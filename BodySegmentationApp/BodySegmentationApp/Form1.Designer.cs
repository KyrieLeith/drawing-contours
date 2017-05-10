namespace BodySegmentationApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_browse = new System.Windows.Forms.Button();
            this.pictureBox_original = new System.Windows.Forms.PictureBox();
            this.pictureBox_segmented = new System.Windows.Forms.PictureBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label_trackBarValue = new System.Windows.Forms.Label();
            this.button_saveImage = new System.Windows.Forms.Button();
            this.button_saveAllImages = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button_draw_contour = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_segmented)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(446, 15);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(134, 32);
            this.button_browse.TabIndex = 1;
            this.button_browse.Text = "Open Images";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // pictureBox_original
            // 
            this.pictureBox_original.Location = new System.Drawing.Point(446, 68);
            this.pictureBox_original.Name = "pictureBox_original";
            this.pictureBox_original.Size = new System.Drawing.Size(381, 394);
            this.pictureBox_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_original.TabIndex = 2;
            this.pictureBox_original.TabStop = false;
            // 
            // pictureBox_segmented
            // 
            this.pictureBox_segmented.Location = new System.Drawing.Point(30, 68);
            this.pictureBox_segmented.Name = "pictureBox_segmented";
            this.pictureBox_segmented.Size = new System.Drawing.Size(381, 394);
            this.pictureBox_segmented.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_segmented.TabIndex = 3;
            this.pictureBox_segmented.TabStop = false;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(14, 637);
            this.trackBar.Maximum = 0;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(1172, 69);
            this.trackBar.TabIndex = 4;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 767);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 15, 0);
            this.statusStrip.Size = new System.Drawing.Size(1198, 30);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(17, 25);
            this.toolStripStatusLabel.Text = " ";
            // 
            // label_trackBarValue
            // 
            this.label_trackBarValue.AutoSize = true;
            this.label_trackBarValue.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_trackBarValue.Location = new System.Drawing.Point(574, 725);
            this.label_trackBarValue.Name = "label_trackBarValue";
            this.label_trackBarValue.Size = new System.Drawing.Size(17, 25);
            this.label_trackBarValue.TabIndex = 6;
            this.label_trackBarValue.Text = " ";
            // 
            // button_saveImage
            // 
            this.button_saveImage.Location = new System.Drawing.Point(836, 717);
            this.button_saveImage.Name = "button_saveImage";
            this.button_saveImage.Size = new System.Drawing.Size(130, 40);
            this.button_saveImage.TabIndex = 7;
            this.button_saveImage.Text = "Save Image";
            this.button_saveImage.UseVisualStyleBackColor = true;
            this.button_saveImage.Click += new System.EventHandler(this.button_saveImage_Click);
            // 
            // button_saveAllImages
            // 
            this.button_saveAllImages.Location = new System.Drawing.Point(974, 717);
            this.button_saveAllImages.Name = "button_saveAllImages";
            this.button_saveAllImages.Size = new System.Drawing.Size(158, 40);
            this.button_saveAllImages.TabIndex = 8;
            this.button_saveAllImages.Text = "Save All Images";
            this.button_saveAllImages.UseVisualStyleBackColor = true;
            this.button_saveAllImages.Click += new System.EventHandler(this.button_saveAllImages_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Выберите КТ-снимки";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.Title = "Сохранение текущего сегментированного изображения";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Выберите директорию для сохранения сегментированных изображений";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(862, 68);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(278, 384);
            this.listBox1.TabIndex = 9;
            // 
            // button_draw_contour
            // 
            this.button_draw_contour.Enabled = false;
            this.button_draw_contour.Location = new System.Drawing.Point(693, 15);
            this.button_draw_contour.Name = "button_draw_contour";
            this.button_draw_contour.Size = new System.Drawing.Size(134, 32);
            this.button_draw_contour.TabIndex = 10;
            this.button_draw_contour.Text = "Draw contour";
            this.button_draw_contour.UseVisualStyleBackColor = true;
            this.button_draw_contour.Click += new System.EventHandler(this.button_draw_contour_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 797);
            this.Controls.Add(this.button_draw_contour);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button_saveAllImages);
            this.Controls.Add(this.button_saveImage);
            this.Controls.Add(this.label_trackBarValue);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.pictureBox_segmented);
            this.Controls.Add(this.pictureBox_original);
            this.Controls.Add(this.button_browse);
            this.Name = "MainForm";
            this.Text = "Body Segmentation";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_segmented)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.PictureBox pictureBox_original;
        private System.Windows.Forms.PictureBox pictureBox_segmented;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label label_trackBarValue;
        private System.Windows.Forms.Button button_saveImage;
        private System.Windows.Forms.Button button_saveAllImages;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_draw_contour;
    }
}

