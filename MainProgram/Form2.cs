using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;

namespace MainProgram
{
    public partial class Form2 : Form
    {
        private VideoCapture capture;      // Dùng để mở webcam
        private Thread cameraThread;       // Luồng lấy frame từ webcam
        private bool isCameraRunning = false;

        public Form2()
        {
            InitializeComponent();
            StartCamera();
        }

        // Khởi tạo webcam và bắt đầu luồng lấy frame
        private void StartCamera()
        {
            capture = new VideoCapture(0); // 0 để mở webcam mặc định
            if (!capture.IsOpened())
            {
                MessageBox.Show("Không mở được webcam!");
                return;
            }
            isCameraRunning = true;
            cameraThread = new Thread(new ThreadStart(CaptureCameraCallback));
            cameraThread.IsBackground = true;
            cameraThread.Start();
        }

        // Luồng liên tục lấy frame từ webcam
        private void CaptureCameraCallback()
        {
            using (Mat frame = new Mat())
            {
                while (isCameraRunning)
                {
                    try
                    {
                        capture.Read(frame);
                        if (!frame.Empty())
                        {
                            // Chuyển đổi Mat sang Bitmap sử dụng hàm tự viết
                            Bitmap bmp = MatToBitmap(frame);
                            if (bmp == null)
                                continue;

                            // Cập nhật PictureBox trên luồng UI
                            if (pictureBoxWebcam.InvokeRequired)
                            {
                                pictureBoxWebcam.BeginInvoke(new Action(() =>
                                {
                                    if (pictureBoxWebcam.Image != null)
                                        pictureBoxWebcam.Image.Dispose();
                                    pictureBoxWebcam.Image = bmp;
                                }));
                            }
                            else
                            {
                                if (pictureBoxWebcam.Image != null)
                                    pictureBoxWebcam.Image.Dispose();
                                pictureBoxWebcam.Image = bmp;
                            }
                        }
                        Thread.Sleep(30); // ~33 fps
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Camera capture error: " + ex.Message);
                    }
                }
            }
        }

        // Hàm tự chuyển đổi Mat thành Bitmap
        private Bitmap MatToBitmap(Mat mat)
        {
            try
            {
                // Sử dụng định dạng BMP để chuyển đổi
                byte[] imageData;
                Cv2.ImEncode(".bmp", mat, out imageData, new int[0]);
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    // Tạo Bitmap từ MemoryStream
                    Bitmap bmp = new Bitmap(ms);
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MatToBitmap error: " + ex.Message);
                return null;
            }
        }

        // Sự kiện nút "Chụp ảnh" để lưu khung hình hiện tại từ webcam
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (pictureBoxWebcam.Image != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "PNG Image|*.png";
                    dialog.FileName = $"webcam_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Sử dụng clone của ảnh:
                            using (Bitmap bmpClone = new Bitmap((Bitmap)pictureBoxWebcam.Image))
                            {
                                bmpClone.Save(dialog.FileName, ImageFormat.Png);
                            }
                            MessageBox.Show("Ảnh đã được lưu thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có ảnh từ webcam để chụp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Dừng webcam và giải phóng tài nguyên khi form đóng
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isCameraRunning = false;
            if (cameraThread != null && cameraThread.IsAlive)
            {
                cameraThread.Join();
            }
            if (capture != null && capture.IsOpened())
            {
                capture.Release();
            }
            base.OnFormClosing(e);
        }
    }
}
