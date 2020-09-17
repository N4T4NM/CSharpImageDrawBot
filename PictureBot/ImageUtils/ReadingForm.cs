using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PictureBot.ImageUtils
{
    public partial class ReadingForm : Form
    {
        public ReadingForm()
        {
            InitializeComponent();
        }

        public Dictionary<string, List<int[]>> ImageData { get; private set; }
        public Image TargetImage { get; set; }

        public int JumpV { get; set; }
        public int JumpH { get; set; }
        public int ColorDiff { get; set; }
        public bool NoWhite { get; set; }

        ImageRead imgR;
        private void ReadingForm_Load(object sender, EventArgs e)
        {
            WaitFormShow.Start();
            ReadedPixels.Text = string.Format("Pixels to read: {0}", (TargetImage.Width * TargetImage.Height));
        }

        public void StartReading()
        {
            imgR = new ImageRead();
            imgR.onImageReaded += ImageReaded;
            imgR.ReadFullImage(TargetImage, JumpH, JumpV, ColorDiff, NoWhite);
        }

        private void ImageReaded(Dictionary<string, List<int[]>> ImageMap)
        {
            ImageData = ImageMap;
            this.DialogResult = DialogResult.OK;
        }

        private void WaitFormShow_Tick(object sender, EventArgs e)
        {
            WaitFormShow.Stop();
            StartReading();
        }


        private void ReadedPixels_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            imgR.StopReading();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
