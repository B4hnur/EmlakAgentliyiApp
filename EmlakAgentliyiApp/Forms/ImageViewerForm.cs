using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DashinmazEmlakApp.Forms
{
    /// <summary>
    /// Əmlak şəkillərini nümayiş etdirmək üçün form
    /// </summary>
    public partial class ImageViewerForm : Form
    {
        // Şəkil işləmə ilə bağlı dəyişənlər
        private Image originalImage = null;
        private float zoomFactor = 1.0f;
        private PointF imagePosition = new PointF(0, 0);
        private bool isPanning = false;
        private Point lastMousePosition;
        private string imagePath = string.Empty;

        /// <summary>
        /// Constructor - boş form yaradır
        /// </summary>
        public ImageViewerForm()
        {
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Constructor - şəkil yolunu alan
        /// </summary>
        /// <param name="imagePath">Açılacaq şəkilin yolu</param>
        public ImageViewerForm(string imagePath)
        {
            InitializeComponent();
            SetupForm();
            LoadImage(imagePath);
        }

        /// <summary>
        /// Form elementlərini təyin edir
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomReset = new System.Windows.Forms.Button();
            this.btnZoomFit = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnZoomIn);
            this.panel1.Controls.Add(this.btnZoomOut);
            this.panel1.Controls.Add(this.btnZoomReset);
            this.panel1.Controls.Add(this.btnZoomFit);
            this.panel1.Controls.Add(this.btnRotateLeft);
            this.panel1.Controls.Add(this.btnRotateRight);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(12, 8);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btnZoomIn.TabIndex = 0;
            this.btnZoomIn.Text = "Böyüt";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(93, 8);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btnZoomOut.TabIndex = 1;
            this.btnZoomOut.Text = "Kiçilt";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomReset
            // 
            this.btnZoomReset.Location = new System.Drawing.Point(174, 8);
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.Size = new System.Drawing.Size(75, 23);
            this.btnZoomReset.TabIndex = 2;
            this.btnZoomReset.Text = "Sıfırla";
            this.btnZoomReset.UseVisualStyleBackColor = true;
            this.btnZoomReset.Click += new System.EventHandler(this.btnZoomReset_Click);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.Location = new System.Drawing.Point(255, 8);
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(75, 23);
            this.btnZoomFit.TabIndex = 3;
            this.btnZoomFit.Text = "Uyğunlaşdır";
            this.btnZoomFit.UseVisualStyleBackColor = true;
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Location = new System.Drawing.Point(336, 8);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(75, 23);
            this.btnRotateLeft.TabIndex = 4;
            this.btnRotateLeft.Text = "Sola çevir";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Location = new System.Drawing.Point(417, 8);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(75, 23);
            this.btnRotateRight.TabIndex = 5;
            this.btnRotateRight.Text = "Sağa çevir";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(713, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Bağla";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 389);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 389);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(0, 429);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(800, 21);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Şəkil məlumatı";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblInfo);
            this.Name = "ImageViewerForm";
            this.Text = "Şəkil Görüntüləyici";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Form elementləri
        /// </summary>
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomReset;
        private System.Windows.Forms.Button btnZoomFit;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblInfo;

        /// <summary>
        /// Form-un başlanğıc parametrlərini təyin edir
        /// </summary>
        private void SetupForm()
        {
            // Şəkil paneli üçün event handler-lər əlavə et
            this.pictureBox.MouseWheel += new MouseEventHandler(pictureBox_MouseWheel);
        }

        /// <summary>
        /// Şəkli yükləyən metod
        /// </summary>
        /// <param name="path">Şəkil faylının yolu</param>
        public void LoadImage(string path)
        {
            try
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }

                if (File.Exists(path))
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        originalImage = Image.FromStream(stream);
                    }

                    imagePath = path;
                    zoomFactor = 1.0f;
                    imagePosition = new PointF(0, 0);

                    FileInfo fileInfo = new FileInfo(path);
                    this.Text = $"Şəkil Görüntüləyici - {fileInfo.Name}";
                    lblInfo.Text = $"Fayl: {fileInfo.Name} | Ölçülər: {originalImage.Width}x{originalImage.Height} | Tarix: {fileInfo.LastWriteTime.ToString("dd.MM.yyyy HH:mm")}";

                    // Ekrana uyğunlaşdırma
                    FitToScreen();
                }
                else
                {
                    MessageBox.Show("Şəkil faylı tapılmadı: " + path, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Şəkil yüklənərkən xəta baş verdi: " + ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pictureBox.Invalidate();
        }

        /// <summary>
        /// Şəkli ekrana sığdırır
        /// </summary>
        private void FitToScreen()
        {
            if (originalImage == null) return;

            float imageRatio = (float)originalImage.Width / originalImage.Height;
            float containerRatio = (float)pictureBox.Width / pictureBox.Height;

            if (imageRatio > containerRatio)
            {
                // Şəkil daha enlidir, eni sığdır
                zoomFactor = (float)pictureBox.Width / originalImage.Width;
            }
            else
            {
                // Şəkil daha hündürdür, hündürlüyü sığdır
                zoomFactor = (float)pictureBox.Height / originalImage.Height;
            }

            CenterImage();
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Şəkli mərkəzləşdirən metod
        /// </summary>
        private void CenterImage()
        {
            if (originalImage == null) return;

            float scaledWidth = originalImage.Width * zoomFactor;
            float scaledHeight = originalImage.Height * zoomFactor;

            imagePosition.X = (pictureBox.Width - scaledWidth) / 2;
            imagePosition.Y = (pictureBox.Height - scaledHeight) / 2;
        }

        /// <summary>
        /// PictureBox-da şəklin çəkilməsi
        /// </summary>
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage == null) return;

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            float scaledWidth = originalImage.Width * zoomFactor;
            float scaledHeight = originalImage.Height * zoomFactor;

            e.Graphics.DrawImage(originalImage,
                new RectangleF(imagePosition.X, imagePosition.Y, scaledWidth, scaledHeight));
        }

        #region Button Event Handlers

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            zoomFactor *= 1.25f;
            pictureBox.Invalidate();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            zoomFactor /= 1.25f;
            if (zoomFactor < 0.01f) zoomFactor = 0.01f;
            pictureBox.Invalidate();
        }

        private void btnZoomReset_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.0f;
            CenterImage();
            pictureBox.Invalidate();
        }

        private void btnZoomFit_Click(object sender, EventArgs e)
        {
            FitToScreen();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            originalImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox.Invalidate();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Invalidate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Mouse Event Handlers

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanning = true;
                lastMousePosition = e.Location;
                this.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPanning)
            {
                imagePosition.X += (e.X - lastMousePosition.X);
                imagePosition.Y += (e.Y - lastMousePosition.Y);
                lastMousePosition = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isPanning = false;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            float oldZoom = zoomFactor;

            if (e.Delta > 0)
            {
                zoomFactor *= 1.25f;
            }
            else
            {
                zoomFactor /= 1.25f;
                if (zoomFactor < 0.01f) zoomFactor = 0.01f;
            }

            // Zoom-ı fare pozisiyasına görə ayarla
            PointF mousePos = e.Location;
            PointF oldImagePos = new PointF(
                (mousePos.X - imagePosition.X) / oldZoom,
                (mousePos.Y - imagePosition.Y) / oldZoom);

            imagePosition.X = mousePos.X - oldImagePos.X * zoomFactor;
            imagePosition.Y = mousePos.Y - oldImagePos.Y * zoomFactor;

            pictureBox.Invalidate();
        }

        #endregion

        /// <summary>
        /// Form bağlanarkən resursları azad et
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.Dispose();
            }
            base.OnFormClosed(e);
        }
    }
}