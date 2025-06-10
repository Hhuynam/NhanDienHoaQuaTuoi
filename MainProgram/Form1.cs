using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Accord.MachineLearning;

namespace MainProgram
{
    // Lớp biểu diễn một mẫu (instance) của dữ liệu
    public class ImageInstance
    {
        public double[] Features { get; set; } = Array.Empty<double>();
        // Nhãn được lấy trực tiếp từ tên subfolder, ví dụ: "fresh_apple"
        public string Label { get; set; } = string.Empty;
    }

    public partial class Form1 : Form
    {
        // Mô hình KNN sau khi huấn luyện
        private KNearestNeighbors knnModel;
        // Ánh xạ từ số (nhãn dự đoán KNN) sang nhãn dạng chuỗi
        private Dictionary<int, string> labelMapping;

        public Form1()
        {
            InitializeComponent();
            // Huấn luyện mô hình ngay khi form khởi tạo
            TrainKnnModel();
        }

        // Sự kiện của nút "Chọn hình"
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(dialog.FileName);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi khi mở ảnh: " + ex.Message);
                    }
                }
            }
        }

        // Sự kiện của nút "Nhận diện"
        private void btnRecognize_Click(object sender, EventArgs e)
        {
            if(pictureBox.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(knnModel == null)
            {
                MessageBox.Show("Mô hình chưa được huấn luyện.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string result = RecognizeImage(pictureBox.Image);
            lblResult.Text = "Kết quả: " + result;
        }

        // Sự kiện nút "Mở Webcam" - mở Form2 chứa webcam
        private void btnWebcam_Click(object sender, EventArgs e)
        {
            // Tạo một instance của Form2 và hiển thị dưới dạng dialog (modal)
            using (Form2 frmWebcam = new Form2())
            {
                frmWebcam.ShowDialog();
            }
        }

        private void TrainKnnModel()
        {
            // Đường dẫn dataset (chứa các subfolder: fresh_apple, rotten_apple, …)
            string datasetPath = @"D:\Project\NhanDienHoaQuaTuoi\dataset2";
            List<ImageInstance> instances = LoadDataset(datasetPath);

            if(instances == null || instances.Count == 0)
            {
                MessageBox.Show("Không tìm thấy ảnh huấn luyện trong dataset.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int n = instances.Count;
            double[][] features = new double[n][];
            int[] labels = new int[n];

            Dictionary<string, int> labelToInt = new Dictionary<string, int>();
            int nextLabel = 0;
            for(int i = 0; i < n; i++)
            {
                features[i] = instances[i].Features;
                string lbl = instances[i].Label;
                if(!labelToInt.ContainsKey(lbl))
                {
                    labelToInt[lbl] = nextLabel++;
                }
                labels[i] = labelToInt[lbl];
            }

            // Khởi tạo mô hình KNN với k = 3
            knnModel = new KNearestNeighbors(3, features, labels);

            // Tạo ánh xạ ngược từ số sang nhãn
            labelMapping = new Dictionary<int, string>();
            foreach(var kv in labelToInt)
            {
                labelMapping[kv.Value] = kv.Key;
            }
        }

        private string RecognizeImage(Image image)
        {
            double[] featureVector = ExtractFeaturesFromImage(image);
            int predicted = knnModel.Compute(featureVector);
            return labelMapping.ContainsKey(predicted) ? labelMapping[predicted] : "Không xác định";
        }

        private List<ImageInstance> LoadDataset(string datasetPath)
        {
            List<ImageInstance> instances = new List<ImageInstance>();
            if(!System.IO.Directory.Exists(datasetPath))
            {
                MessageBox.Show($"Đường dẫn dataset không tồn tại: {datasetPath}");
                return instances;
            }
            string[] subfolders = System.IO.Directory.GetDirectories(datasetPath);
            foreach(string folder in subfolders)
            {
                string label = System.IO.Path.GetFileName(folder).ToLower();
                string[] files = System.IO.Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly);
                foreach(string file in files)
                {
                    if(!IsImageFile(file)) continue;
                    double[] featureVector = ExtractFeatures(file);
                    if(featureVector != null && featureVector.Length > 0)
                    {
                        instances.Add(new ImageInstance { Features = featureVector, Label = label });
                    }
                }
            }
            return instances;
        }

        private bool IsImageFile(string file)
        {
            string ext = System.IO.Path.GetExtension(file).ToLower();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp";
        }

        private double[] ExtractFeatures(string imagePath)
        {
            try
            {
                using(Bitmap bmp = new Bitmap(imagePath))
                {
                    Size targetSize = new Size(32, 32);
                    using(Bitmap resized = new Bitmap(bmp, targetSize))
                    {
                        List<double> featureList = new List<double>();
                        for(int y = 0; y < resized.Height; y++)
                        {
                            for(int x = 0; x < resized.Width; x++)
                            {
                                Color pixel = resized.GetPixel(x, y);
                                featureList.Add(pixel.R / 255.0);
                                featureList.Add(pixel.G / 255.0);
                                featureList.Add(pixel.B / 255.0);
                            }
                        }
                        return featureList.ToArray();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi xử lý ảnh [{imagePath}]: {ex.Message}");
                return Array.Empty<double>();
            }
        }

        private double[] ExtractFeaturesFromImage(Image image)
        {
            try
            {
                Bitmap bmp = new Bitmap(image);
                Size targetSize = new Size(32, 32);
                using(Bitmap resized = new Bitmap(bmp, targetSize))
                {
                    List<double> featureList = new List<double>();
                    for(int y = 0; y < resized.Height; y++)
                    {
                        for(int x = 0; x < resized.Width; x++)
                        {
                            Color pixel = resized.GetPixel(x, y);
                            featureList.Add(pixel.R / 255.0);
                            featureList.Add(pixel.G / 255.0);
                            featureList.Add(pixel.B / 255.0);
                        }
                    }
                    return featureList.ToArray();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý ảnh từ đối tượng: {ex.Message}");
                return Array.Empty<double>();
            }
        }
    }
}
