using PictureBot.ImageUtils;
using PictureBot.InputUtils;
using PictureBot.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PictureBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void configButton_click(object sender, EventArgs e)
        {
            
        }

        private void PaseButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                var img = Clipboard.GetImage();
                TargetImageBox.Tag = img;
                ImageRGBType_SelectedIndexChanged(null, new EventArgs());
                checkAllIntegers(null, new EventArgs());
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowTray.Visible = false;
            ImageRGBType.SelectedIndex = 1;
            WidthBox.TextChanged += checkAllIntegers;
            HeightBox.TextChanged += checkAllIntegers;
            VJumpBox.TextChanged += checkAllIntegers;
            HJumpBox.TextChanged += checkAllIntegers;
            ColorDiffBox.TextChanged += checkAllIntegers;
            ReloadPresetsList();
        }

        private void checkAllIntegers(object sender, EventArgs e)
        {
            TextBox[] boxes = { WidthBox, HeightBox, VJumpBox, HJumpBox, ColorDiffBox, ThreadSleepBox };

            bool AllOk = true;
            foreach (TextBox box in boxes)
            {
                bool IsAnInteger = false;
                int CurrentValue = 0;
                IsAnInteger = int.TryParse(box.Text, out CurrentValue);

                if (!IsAnInteger || CurrentValue < 1 || TargetImageBox.Image == null)
                {
                    AllOk = false;
                    break;
                }
            }
            StartButton.Enabled = AllOk;
        }

        private void ReceiveKeyHook(Keys key)
        {
            if (key == Keys.Subtract && !ImageDraw.IsRunning)
            {
                ImageDraw.onDrawEnd += ImageDrawEnd;
                ImageDraw.StartDrawing(imageData, int.Parse(VJumpBox.Text), int.Parse(ThreadSleepBox.Text));
            }
            if (key == Keys.Multiply && !this.Visible && ImageDraw.IsRunning)
            {
                ImageDraw.IsPaused = !ImageDraw.IsPaused;
            }
            if (key == Keys.Divide && !this.Visible)
            {
                this.Show();
                ImageDraw.StopDrawing();
                ImageDraw.onDrawEnd -= ImageDrawEnd;
                KeyHook.onKeyHooked -= ReceiveKeyHook;
                KeyHook.RemoveHookFromCurrentProcess();
            }
        }

        private void ImageDrawEnd(bool Success)
        {
            Invoke(new Action(() =>
            {
                this.Show();
                if (Success)
                {
                    WindowTray.ShowBalloonTip(2000, "Draw ended", "Success !", ToolTipIcon.Info);
                }
                else WindowTray.ShowBalloonTip(2000, "Draw ended", "Interrupted !", ToolTipIcon.Warning);
            }));
            ImageDraw.onDrawEnd -= ImageDrawEnd;
            KeyHook.onKeyHooked -= ReceiveKeyHook;
            KeyHook.RemoveHookFromCurrentProcess();
        }

        private void ImageRGBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TargetImageBox.Tag != null)
            {
                switch (ImageRGBType.SelectedIndex)
                {
                    case 0:
                        TargetImageBox.Image = ImageConvert.GetGrayImage((Image)TargetImageBox.Tag);
                        break;
                    case 1:
                        TargetImageBox.Image = ImageConvert.Get16Bit((Image)TargetImageBox.Tag);
                        break;
                    case 2:
                        TargetImageBox.Image = ImageConvert.Get8Bit((Image)TargetImageBox.Tag);
                        break;
                }
            }
        }

        private Dictionary<string, List<int[]>> imageData;
        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowTray.Visible = true;
            var rd = new ReadingForm();
            rd.TargetImage = new Bitmap(TargetImageBox.Image, int.Parse(WidthBox.Text), int.Parse(HeightBox.Text));
            rd.NoWhite = IgnoreWhiteBox.Checked;
            rd.JumpV = int.Parse(VJumpBox.Text);
            rd.JumpH = int.Parse(HJumpBox.Text);
            rd.ColorDiff = int.Parse(ColorDiffBox.Text);

            if (rd.ShowDialog() == DialogResult.OK)
            {
                imageData = rd.ImageData;
                WindowTray.ShowBalloonTip(5000, "Readed !", "SUBTRACT: Start\nMULTIPLY: Pause\nDIVIDE: Stop", ToolTipIcon.Info);
                KeyHook.AddHookToCurrentProcess();
                KeyHook.onKeyHooked += ReceiveKeyHook;
            }
            else
            {
                this.Show();
            }
        }

        private void SelectedPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = SelectedPresets.SelectedIndex;
            if (selectedIndex < 0) return;
            if(selectedIndex == 0)
            {
                SelectedPresets.SelectedIndex = -1;
                KeyHook.onKeyHooked -= ReceiveKeyHook;
                KeyHook.RemoveHookFromCurrentProcess();
                var stp = new SetupForm() { WindowState = FormWindowState.Maximized };
                stp.FormClosed += frmC;
                stp.ShowDialog();
                return;
            }
            selectedIndex--;
            if (Properties.Settings.Default.UserPresets == null) return;

            string data = Properties.Settings.Default.UserPresets[selectedIndex];
            string[] args = data.Split(';');

            SetSettingsAsPreset(args[1].Split(','), "CanvasStart");
            SetSettingsAsPreset(args[2].Split(','), "PenLocation");
            SetSettingsAsPreset(args[3].Split(','), "ColorLocation");
            SetSettingsAsPreset(args[4].Split(','), "ColorR");
            SetSettingsAsPreset(args[5].Split(','), "ColorG");
            SetSettingsAsPreset(args[6].Split(','), "ColorB");
        }

        private void frmC(object sender, FormClosedEventArgs e)
        {
            ReloadPresetsList();
        }

        private void SetSettingsAsPreset(string[] Preset, string Target)
        {
            Properties.Settings.Default[Target] = (Object)new Point(int.Parse(Preset[0]), int.Parse(Preset[1]));
        }

        public void ReloadPresetsList()
        {
            SelectedPresets.Items.Clear();
            SelectedPresets.Items.Add("--New Preset--");
            if (Properties.Settings.Default.UserPresets != null)
            {
                for(int i=0; i < Properties.Settings.Default.UserPresets.Count; i++)
                {
                    string data = Properties.Settings.Default.UserPresets[i];
                    string[] args = data.Split(';');
                    SelectedPresets.Items.Add(args[0]);
                }
                try { SelectedPresets.SelectedIndex = 1; } catch { }
            }
        }

        private void RemoveSelectedPreset_Click(object sender, EventArgs e)
        {
            if(SelectedPresets.SelectedIndex > 0)
            {
                Properties.Settings.Default.UserPresets.RemoveAt(SelectedPresets.SelectedIndex-1);
                Properties.Settings.Default.Save();
                ReloadPresetsList();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            ReloadPresetsList();
        }

        private void DrawLinesBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowTray.Visible = false;
            WindowTray.Dispose();
        }

        private void WidthBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
