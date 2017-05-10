using System.Drawing;
using System.Drawing.Imaging;

namespace BodySegmentationApp
{
    public class ImageProcessing
    {
        private static byte[][] GetGrayscaleImageData(Bitmap image) // метод GetGrayscaleImageData преобразует входное изображение в массив байтов в градациях серого
        {//byte[][] - то, что получаем на выходе
         //(Bitmap image) - то, что получаем на входе, Bitmap- класс, а image - название объекта класса
            int imageHeight = image.Height;
            int imageWidth = image.Width;
            byte[][] imageData = new byte[imageHeight][]; // изображение представленное в виде двумерного массива байтов
            for (int i = 0; i < imageHeight; i++)
                imageData[i] = new byte[imageWidth];

            // преобразование входного изображения в массив байтов, bitmapData - это временная переменная для хранения байтов
            BitmapData bitmapData = image.LockBits(new Rectangle(new Point(0, 0),
               image.Size), ImageLockMode.ReadOnly, image.PixelFormat);
            unsafe
            {
                int byteCounts = 3;
                for (int i = 0; i < imageHeight; i++)
                {
                    byte* OriginalRowPtr = (byte*)bitmapData.Scan0 +
                        i * bitmapData.Stride;
                    for (int j = 0; j < imageWidth; j++)
                    {
                        int ColorPosition = j * byteCounts;
                        imageData[i][j] = OriginalRowPtr[ColorPosition]; // blue color
                    }
                }
            }
            image.UnlockBits(bitmapData);

            return imageData;
        }

        public static byte[][][] GetGrayscaleImagesData(string[] filenames, 
            ref int imagesNumber, ref int imageHeight, ref int imageWidth)
        {
            // метод GetGrayscaleImagESData преобразует набор входных изображений в трёхмерный массив байтов в градациях серого
            // Загрузить первый растровый рисунок и его параметры
            Bitmap firstBitmap = new Bitmap(filenames[0]);
            imagesNumber = filenames.Length;
            imageHeight = firstBitmap.Height;
            imageWidth = firstBitmap.Width;

            byte[][][] imagesData = new byte[imagesNumber][][];
            for (int i = 0; i < imagesNumber; i++)
            {
                imagesData[i] = new byte[imageHeight][];
                for (int j = 0; j < imageHeight; j++)
                    imagesData[i][j] = new byte[imageWidth];
            }
            imagesData[0] = GetGrayscaleImageData(firstBitmap);

            // Загрузить остальные изображения
            for (int i = 1; i < filenames.Length; i++)
                imagesData[i] = GetGrayscaleImageData(new Bitmap(filenames[i]));

            return imagesData;
        }

        public static Bitmap GetBitmapFrom32Matrix(byte[][] matrix, int height, int width)
        {// преобразует матрицу байтов в изображение(обратный метод для метода GetGrayscaleImageData)
            //resultBitmap - объявляем переменную для хранения результирующего изображения в окне слева
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            unsafe
            {// преобразование
                byte BytesCount = 3;
                BitmapData UpdatingData = resultBitmap.LockBits(new Rectangle(
                    new Point(0, 0), resultBitmap.Size), ImageLockMode.WriteOnly, resultBitmap.PixelFormat);

                for (int i = 0; i < height; i++)
                {
                    byte* BitmapRowPtr = (byte*)UpdatingData.Scan0 + i * UpdatingData.Stride;
                    for (int j = 0; j < width; j++)
                    {
                        int ColorPosition = j * BytesCount;
                        BitmapRowPtr[ColorPosition] = matrix[i][j];
                        BitmapRowPtr[ColorPosition + 1] = matrix[i][j];
                        BitmapRowPtr[ColorPosition + 2] = matrix[i][j];
                    }
                }

                resultBitmap.UnlockBits(UpdatingData);
            }
            //возврат значения
            return resultBitmap;
        }

        public static Bitmap GetBitmapFromRegions(int[][] regionsMatrix, int height, int width, int bodyRegionIndex, ColorFactory cFactory)
        {//метод GetBitmapFromRegions получает растровое изображение из регионов, где resultBitmap - переменная для хранения растрового изображения
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte BytesCount = 3;
                BitmapData UpdatingData = resultBitmap.LockBits(new Rectangle(
                    new Point(0, 0), resultBitmap.Size), ImageLockMode.WriteOnly, resultBitmap.PixelFormat);

                int segmentIndex;
                for (int i = 0; i < height; i++)
                {
                    byte* BitmapRowPtr = (byte*)UpdatingData.Scan0 + i * UpdatingData.Stride;
                    for (int j = 0; j < width; j++)
                    {
                        int ColorPosition = j * BytesCount;

                        segmentIndex = regionsMatrix[i][j];

                        if (segmentIndex == -1) // Области, отсекаемые как результат фильтрации
                        {
                            BitmapRowPtr[ColorPosition] = 0;
                            BitmapRowPtr[ColorPosition + 1] = 0;
                            BitmapRowPtr[ColorPosition + 2] = 200;
                            continue;
                        }

                        if (segmentIndex == bodyRegionIndex)
                        {
                            BitmapRowPtr[ColorPosition] = 255;
                            BitmapRowPtr[ColorPosition + 1] = 255;
                            BitmapRowPtr[ColorPosition + 2] = 255;
                        }
                        else
                        {
                            BitmapRowPtr[ColorPosition] = 0;
                            BitmapRowPtr[ColorPosition + 1] = 0;
                            BitmapRowPtr[ColorPosition + 2] = 0;

                            //if (segmentIndex < cFactory.MyColors.Length)
                            //{
                            //    Color color = cFactory.GetColorByIndex(segmentIndex);
                            //    BitmapRowPtr[ColorPosition] = color.B; ;
                            //    BitmapRowPtr[ColorPosition + 1] = color.G;
                            //    BitmapRowPtr[ColorPosition + 2] = color.R;
                            //}
                            //else
                            //{
                            //    BitmapRowPtr[ColorPosition] = 0;
                            //    BitmapRowPtr[ColorPosition + 1] = 0;
                            //    BitmapRowPtr[ColorPosition + 2] = 0;
                            //}
                        }
                    }
                }

                resultBitmap.UnlockBits(UpdatingData);
            }

            return resultBitmap;
        }
    }
}
