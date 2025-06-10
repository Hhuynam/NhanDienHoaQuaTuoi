namespace MainProgram
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxWebcam;
        private System.Windows.Forms.Button btnCapture;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pictureBoxWebcam = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWebcam)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxWebcam
            // 
            this.pictureBoxWebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxWebcam.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxWebcam.Name = "pictureBoxWebcam";
            this.pictureBoxWebcam.Size = new System.Drawing.Size(640, 480);
            this.pictureBoxWebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxWebcam.TabIndex = 0;
            this.pictureBoxWebcam.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(20, 520);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(100, 30);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Chụp ảnh";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(700, 570);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.pictureBoxWebcam);
            this.Name = "Form2";
            this.Text = "Webcam";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWebcam)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
