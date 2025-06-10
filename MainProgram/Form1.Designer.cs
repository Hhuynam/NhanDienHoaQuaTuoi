namespace MainProgram
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Các control hiện có
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnRecognize;
        private System.Windows.Forms.Label lblResult;

        // Nút mới để mở Form2 (Webcam)
        private System.Windows.Forms.Button btnWebcam;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnWebcam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(20, 20);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(300, 300);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(350, 20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 30);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Chọn hình";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnRecognize
            // 
            this.btnRecognize.Location = new System.Drawing.Point(350, 70);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(100, 30);
            this.btnRecognize.TabIndex = 2;
            this.btnRecognize.Text = "Nhận diện";
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.btnRecognize_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(350, 120);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(66, 17);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Kết quả:";
            // 
            // btnWebcam - nút mở Form2 (chứa webcam)
            // 
            this.btnWebcam.Location = new System.Drawing.Point(350, 170);
            this.btnWebcam.Name = "btnWebcam";
            this.btnWebcam.Size = new System.Drawing.Size(100, 30);
            this.btnWebcam.TabIndex = 4;
            this.btnWebcam.Text = "Mở Webcam";
            this.btnWebcam.UseVisualStyleBackColor = true;
            this.btnWebcam.Click += new System.EventHandler(this.btnWebcam_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.btnWebcam);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnRecognize);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Ứng dụng nhận diện quả tươi/héo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
