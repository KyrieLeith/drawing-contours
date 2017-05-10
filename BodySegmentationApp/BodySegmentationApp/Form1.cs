using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BodySegmentationApp
{
    public partial class MainForm : Form
    {
        //импорт библиотеки
        [DllImport("CTImageSegmentation.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int CtBodySegmentation(byte* intencity_input, int* regions_output,
            int images_number, int image_height, int image_width, int filter_width = 9, int intencity_threshold = 60);

        private string[] filenames;
        private static byte[][][] ctImages;
        private static int[][][] ctRegions;
        private static int imagesNumber;
        private static int imageHeight;
        private static int imageWidth;
        private static int filterWidth = 3;
        private static int intensityThreshold = 95;
        private static int bodyRegionIndex;

        private static int defaultImageShowIndex = 0;
        

        ColorFactory cFactory;
//конструктор, срабатывающий при создании главной формы
        public MainForm()
        {
            InitializeComponent();
            cFactory = new ColorFactory();
        }

        private void button_browse_Click(object sender, EventArgs e)
        {//метод класса MainForm, который является событием клика кнопки name "button_browse"  
            filenames = null;
            ctImages = null;
            ctRegions = null;
            imagesNumber = 0;
            imageHeight = 0;
            imageWidth = 0;
            bodyRegionIndex = -1;

            pictureBox_original.Image = null;
            pictureBox_segmented.Image = null;
            trackBar.Maximum = 0;
            trackBar.Value = 0;
            toolStripStatusLabel.Text = "Loading and analysis of images";

            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|(*.BMP)|*.BMP";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length > 0)
                {
                    filenames = new string[openFileDialog.FileName.Length];
                    filenames = openFileDialog.FileNames;
                }
                else
                {
                    filenames = null;
                    ctImages = null;
                    ctRegions = null;
                    imagesNumber = 0;
                    imageHeight = 0;
                    imageWidth = 0;
                    bodyRegionIndex = -1;

                    pictureBox_original.Image = null;
                    pictureBox_segmented.Image = null;
                    trackBar.Maximum = 0;
                    trackBar.Value = 0;
                    toolStripStatusLabel.Text = " ";
                    return;
                }
            }
            else
            {
                filenames = null;
                ctImages = null;
                ctRegions = null;
                imagesNumber = 0;
                imageHeight = 0;
                imageWidth = 0;
                bodyRegionIndex = -1;

                pictureBox_original.Image = null;
                pictureBox_segmented.Image = null;
                trackBar.Maximum = 0;
                trackBar.Value = 0;
                toolStripStatusLabel.Text = " ";
                return;
            }

            DateTime begin = DateTime.Now;

            // load images data
            ctImages = ImageProcessing.GetGrayscaleImagesData(filenames, ref imagesNumber, ref imageHeight, ref imageWidth);

            // Выделить память для массива resul
            ctRegions = new int[imagesNumber][][];
            for (int i = 0; i < imagesNumber; i++)
            {
                ctRegions[i] = new int[imageHeight][];
                for (int j = 0; j < imageHeight; j++)
                    ctRegions[i][j] = new int[imageWidth];
            }

            bodyRegionIndex = BodySegmentation();

            DateTime end = DateTime.Now;

            Bitmap firstOriginalImage = ImageProcessing.GetBitmapFrom32Matrix(ctImages[defaultImageShowIndex], imageHeight, imageWidth);
            Bitmap firstRegionsImage = ImageProcessing.GetBitmapFromRegions(ctRegions[defaultImageShowIndex],
                imageHeight, imageWidth, bodyRegionIndex, cFactory);

            trackBar.Maximum = filenames.Length - 1;
            trackBar.Value = defaultImageShowIndex;
            pictureBox_original.Image = firstOriginalImage;
            pictureBox_segmented.Image = firstRegionsImage;
            label_trackBarValue.Text = defaultImageShowIndex.ToString();
            toolStripStatusLabel.Text = string.Format("Complete. {0} images. Elapsed time is {1}", filenames.Length, end - begin);
            getIndices();

            button_draw_contour.Enabled = true;
        }

        private void getIndices()
        {//метод вывода в listbox 1)порядкового номера изображения(imagesNumber)  2)числа регионов (subList.Count)  
            List <List<int>> newList = new List <List<int>>();
            for (int k = 0; k < imagesNumber; k++)
            {
                List<int> subList = new List<int>();
                subList.Add(ctRegions[k][0][0]);
                for (int i = 0; i < imageHeight; i++)
                    for (int j = 0; j < imageWidth; j++)
                    {
                        int inList = 1;
                        for (int p = 0; p < subList.Count; p++)
                            if (subList[p] == ctRegions[k][i][j])
                            {
                                inList = 0;
                                break;
                            }

                        if (inList == 1)
                        {
                            subList.Add(ctRegions[k][i][j]);
                        }
                    }
                listBox1.Items.Add(k + " " + subList.Count);
                newList.Add(subList);
            }
            //return newList;
        }

        unsafe static int BodySegmentation()
        {//метод BodySegmentation сегментации кости 
            // Подготовить данные для использования  функции CtBodySegmentation на языке C
            int imageSize = imageHeight * imageWidth;
            byte[] intencityInput = new byte[imagesNumber * imageSize];
            int[] regionsOutput = new int[imagesNumber * imageSize];

            for (int k = 0; k < imagesNumber; k++)
                for (int i = 0; i < imageHeight; i++)
                    for (int j = 0; j < imageWidth; j++)
                        intencityInput[(k * imageSize) + (i * imageHeight) + j] = ctImages[k][i][j];

            // Использование функции на языке C
            int bodyRegionIndex = -1;
            fixed (byte* intencityInput_ptr = intencityInput)
            {
                fixed(int* regionsOutput_ptr = regionsOutput)
                {
                    bodyRegionIndex = CtBodySegmentation(intencityInput_ptr, regionsOutput_ptr, imagesNumber,
                        imageHeight, imageWidth, filterWidth, intensityThreshold);

                    // Запись результатов, полученных функцией CtBodySegmentation на языке C++ в переменные C #
                    for (int k = 0; k < imagesNumber; k++)
                        for (int i = 0; i < imageHeight; i++)
                            for (int j = 0; j < imageWidth; j++)
                                ctRegions[k][i][j] = regionsOutput_ptr[(k * imageSize) + (i * imageHeight) + j];
                }
            }

            return bodyRegionIndex;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {// метод trackBar_Scroll для полосы прокрутки - выводит номер элемента
            TrackBar bar = (TrackBar)sender;
            if (bar == null)
                return;

            label_trackBarValue.Text = bar.Value.ToString();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {//метод trackBar_ValueChanged обновляет данные при смещении полосы прокрутки(номера и сами изображения)
            TrackBar bar = (TrackBar)sender;
            if (bar == null)
                return;

            int currentIndex = bar.Value;
            Bitmap ctBitmap = ImageProcessing.GetBitmapFrom32Matrix(ctImages[currentIndex], imageHeight, imageWidth);
            Bitmap regionsBitmap = ImageProcessing.GetBitmapFromRegions(ctRegions[currentIndex], imageHeight, imageWidth, 
                bodyRegionIndex, cFactory);

            pictureBox_original.Image = ctBitmap;
            pictureBox_segmented.Image = regionsBitmap;
            label_trackBarValue.Text = currentIndex.ToString();
        }

        private void button_saveImage_Click(object sender, EventArgs e)
        {//метод-обработчик события клика на кнопку name "button_saveImage"сохраняющий одно изображение
            toolStripStatusLabel.Text = "Saving selected image with regions";

            if (pictureBox_segmented.Image == null)
            {
                MessageBox.Show("Нечего сохранять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveFileDialog.Filter = "Bitmap (*.bmp)|*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_segmented.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            toolStripStatusLabel.Text = "Image is saved";
        }

        private void button_saveAllImages_Click(object sender, EventArgs e)
        {//метод-обработчик события клика на кнопку name "button_saveAllImages"сохраняющий все изображения
            toolStripStatusLabel.Text = "Saving images with regions";

            if (pictureBox_segmented.Image == null)
            {
                MessageBox.Show("Нечего сохранять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath != null)
                {
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        string regionsImageName = folderBrowserDialog.SelectedPath + "\\" + (i + 1) + ".bmp";
                        Bitmap regionsBitmap = ImageProcessing.GetBitmapFromRegions(ctRegions[i], imageHeight, imageWidth,
                            bodyRegionIndex, cFactory);
                        regionsBitmap.Save(regionsImageName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
            }

            toolStripStatusLabel.Text = "Images are saved";
        }

        private void button_draw_contour_Click(object sender, EventArgs e)
        {   //функция обводит контур по сегментированным точкам

            //преобразуем bmp изображение в матрицу пикселей RGB
            Bitmap bmp = (Bitmap)pictureBox_segmented.Image;
            byte[,,] imageMatrix = BitmapToByteRgbQ(bmp);

            //составляет список координат всех точек региона
            List<Point> contour = new List<Point>();
            for (int i = 0; i < bmp.Height; ++i)
                for (int j = 0; j < bmp.Width; ++j)
                    if (imageMatrix[0, j, i] == 255 && imageMatrix[1, j, i] == 255 && imageMatrix[2, j, i] == 255)
                        contour.Add(new Point(i,j));

            //найдем центр тяжести многоугольника
            int centerX, centerY, sumX=0, sumY=0;
            for (int i = 0; i < contour.Count; ++i)
            {
                sumX += contour[i].X;
                sumY += contour[i].X;
            }
            centerX = sumX / contour.Count;
            centerY = sumY / contour.Count;

            //отсортируем точки по полярному углу относительно центра тяжести многоугольника
            List<Tuple<double,Point>> polarCoord = new List<Tuple<double, Point>>();
            for (int i = 0; i < contour.Count; ++i)
            {
                double angle = Math.Atan2(contour[i].Y-centerY, contour[i].X - centerX); //вычисляем полярный угол относительно центра тяжести
                polarCoord.Add(new Tuple<double, Point>(angle, contour[i]));
            }
            polarCoord.Sort((x,y) => x.Item1.CompareTo(y.Item1));
            contour.Clear();
            for (int i = 0; i < polarCoord.Count; ++i)
                contour.Add(new Point(polarCoord[i].Item2.X, polarCoord[i].Item2.Y)); 
            contour.Add(new Point(polarCoord[0].Item2.X, polarCoord[0].Item2.Y)); //еще раз добавили первую точку

            //дорисовываем изображение
            SolidBrush brush = new SolidBrush(Color.LightCyan);
            Pen pen1 = new Pen(Color.Green); //используемая кисть
            pen1.Width = 3;
            //Pen pen2 = new Pen(Color.LightBlue); //для сплайна
            //pen2.Width = 3; //для сплайна
            Graphics g = Graphics.FromImage(bmp);                

            for (int i = 0; i < contour.Count-1; ++i)
            {
                //g.DrawRectangle(pen, contour[i].X, contour[i].Y, 1, 1); //просто раскрасить весь регион
                g.DrawLine(pen1, contour[i], contour[i+1]); //аппроксимация линиями
            }

            //g.DrawCurve(pen2, contour.ToArray(), 0.1f); //аппроксимация сплайнами

            pictureBox_segmented.Image = bmp;
        }

        private unsafe static byte[,,] BitmapToByteRgbQ(Bitmap bmp)
        {   //функция осуществляет наиболее быстрое преобразование Bitmap в byte[,,]
            int width = bmp.Width,
                height = bmp.Height;
            byte[,,] res = new byte[3, height, width];
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos;
                fixed (byte* _res = res)
                {
                    byte* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
                    for (int h = 0; h < height; h++)
                    {
                        curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                        for (int w = 0; w < width; w++)
                        {
                            *_b = *(curpos++); ++_b;
                            *_g = *(curpos++); ++_g;
                            *_r = *(curpos++); ++_r;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }

    }
}
