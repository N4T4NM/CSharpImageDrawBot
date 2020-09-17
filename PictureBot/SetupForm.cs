using PictureBot.InputUtils;
using PictureBot.Utils;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PictureBot
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        int currentConfig = 0;
        bool acceptHook = false;

        private void SetupForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Press \"SUBTRACT\" to start.");
            KeyHook.AddHookToCurrentProcess();
            KeyHook.onKeyHooked += NextConfig;
            acceptHook = true;

            this.Hide();
        }

        private void NextConfig(Keys key)
        {
            if (acceptHook && key == Keys.Subtract && currentConfig == 0)
            {
                ConfigureCanvas();
            }
            else if (acceptHook && key == Keys.Subtract)
            {
                ConfigureRGB();
            }
        }

        Point RBox;
        Point GBox;
        Point BBox;
        private void ConfigureRGB()
        {
            acceptHook = false;
            PrntBox.Image = PrintScreen();
            this.Show();
            MessageBox.Show("Select R, G and B text boxes");
            CanSelectCanvas = true;
        }

        private bool CanSelectCanvas = false;

        Point canvasStart;

        private void ConfigureCanvas()
        {
            acceptHook = false;
            PrntBox.Image = PrintScreen();
            this.Show();
            MessageBox.Show("Click on CANVAS borders.");
            CanSelectCanvas = true;
        }

        Point penLocation;
        private void ConfigurePen()
        {
            MessageBox.Show("Select pen location");
            CanSelectCanvas = true;
        }

        Point colorLocation;
        private void ConfigureColorSelector()
        {
            MessageBox.Show("Select the color selection button");
            CanSelectCanvas = true;
        }

        private void SaveAllData()
        {
            var inp = new InputForm();
            string TargetWindow = "";
            this.Hide();
            if(inp.ShowDialog() == DialogResult.OK)
            {
                TargetWindow = inp.PresetName;
            } else
            {
                MessageBox.Show("Error on save !");
                this.Close();
                return;
            }
            string presetCanvas = string.Format("{0},{1}", canvasStart.X, canvasStart.Y);
            string presetPen = string.Format("{0},{1}", penLocation.X, penLocation.Y);
            string presetColor = string.Format("{0},{1}",colorLocation.X, colorLocation.Y);

            string presetR = string.Format("{0},{1}", RBox.X, RBox.Y);
            string presetG = string.Format("{0},{1}", GBox.X, GBox.Y);
            string presetB = string.Format("{0},{1}", BBox.X, BBox.Y);

            string data = string.Format("{0};{1};{2};{3};{4};{5};{6}", TargetWindow, presetCanvas, presetPen, presetColor, presetR, presetG, presetB);
            if (Properties.Settings.Default.UserPresets == null) Properties.Settings.Default.UserPresets = new StringCollection();
            Properties.Settings.Default.UserPresets.Add(data);
            Properties.Settings.Default.Save();

            MessageBox.Show("Saved data");
            this.Close();
        }

        private void PrntBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && CanSelectCanvas)
            {
                switch (currentConfig)
                {
                    case 0:
                        if (canvasStart.IsEmpty)
                        {
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Red), new Rectangle(MousePosition, new Size(5, 5)));
                            canvasStart = MousePosition;
                            CanSelectCanvas = false;
                            currentConfig = 1;
                            ConfigurePen();
                        }
                        break;
                    case 1:
                        if (penLocation.IsEmpty)
                        {
                            penLocation = MousePosition;
                            CanSelectCanvas = false;
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkOrange), new Rectangle(MousePosition, new Size(5, 5)));
                            currentConfig = 2;
                            ConfigureColorSelector();
                        }
                        break;
                    case 2:
                        if (colorLocation.IsEmpty)
                        {
                            colorLocation = MousePosition;
                            CanSelectCanvas = false;
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Green), new Rectangle(MousePosition, new Size(5, 5)));
                            currentConfig = 3;
                            SelectColorsLocation();
                        }
                        break;
                    case 3:
                        if (RBox.IsEmpty)
                        {
                            RBox = MousePosition;
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Red), new Rectangle(MousePosition, new Size(5, 5)));
                            return;
                        }
                        if (GBox.IsEmpty)
                        {
                            GBox = MousePosition;
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Green), new Rectangle(MousePosition, new Size(5, 5)));
                            return;
                        }
                        if (BBox.IsEmpty)
                        {
                            BBox = MousePosition;
                            PrntBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Blue), new Rectangle(MousePosition, new Size(5, 5)));
                            CanSelectCanvas = false;
                            SaveAllData();
                        }
                        break;
                }
            }
        }

        private void SelectColorsLocation()
        {
            this.Hide();
            MessageBox.Show("Open color selector and press \"SUBTRACT\" when ready");
            acceptHook = true;
        }

        private Image PrintScreen()
        {
            Bitmap print = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graph = Graphics.FromImage(print);
            graph.CopyFromScreen(0, 0, 0, 0, print.Size);
            return (Image)print;
        }

        private void PrntBox_Click(object sender, EventArgs e)
        {

        }
    }
}
