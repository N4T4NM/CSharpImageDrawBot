namespace PictureBot
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PaseButton = new System.Windows.Forms.Button();
            this.TargetImageBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ImageRGBType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.WidthBox = new System.Windows.Forms.TextBox();
            this.HeightBox = new System.Windows.Forms.TextBox();
            this.HJumpBox = new System.Windows.Forms.TextBox();
            this.VJumpBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.IgnoreWhiteBox = new System.Windows.Forms.CheckBox();
            this.ColorDiffBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SelectedPresets = new System.Windows.Forms.ComboBox();
            this.RemoveSelectedPreset = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.WindowTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ThreadSleepBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TargetImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PaseButton
            // 
            this.PaseButton.Location = new System.Drawing.Point(537, 12);
            this.PaseButton.Name = "PaseButton";
            this.PaseButton.Size = new System.Drawing.Size(75, 27);
            this.PaseButton.TabIndex = 1;
            this.PaseButton.Text = "Paste Image";
            this.PaseButton.UseVisualStyleBackColor = true;
            this.PaseButton.Click += new System.EventHandler(this.PaseButton_Click);
            // 
            // TargetImageBox
            // 
            this.TargetImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TargetImageBox.Location = new System.Drawing.Point(12, 45);
            this.TargetImageBox.Name = "TargetImageBox";
            this.TargetImageBox.Size = new System.Drawing.Size(600, 300);
            this.TargetImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TargetImageBox.TabIndex = 2;
            this.TargetImageBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "RGB";
            // 
            // ImageRGBType
            // 
            this.ImageRGBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ImageRGBType.FormattingEnabled = true;
            this.ImageRGBType.Items.AddRange(new object[] {
            "Gray Scale",
            "16 Bit",
            "8 Bit"});
            this.ImageRGBType.Location = new System.Drawing.Point(15, 364);
            this.ImageRGBType.Name = "ImageRGBType";
            this.ImageRGBType.Size = new System.Drawing.Size(94, 21);
            this.ImageRGBType.TabIndex = 4;
            this.ImageRGBType.SelectedIndexChanged += new System.EventHandler(this.ImageRGBType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(149, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(217, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Height";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(300, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pixel Jump Horizontal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(423, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Pixel Jump Vertical";
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(152, 365);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(49, 20);
            this.WidthBox.TabIndex = 9;
            this.WidthBox.Text = "200";
            this.WidthBox.TextChanged += new System.EventHandler(this.WidthBox_TextChanged);
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(220, 365);
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(49, 20);
            this.HeightBox.TabIndex = 10;
            this.HeightBox.Text = "200";
            // 
            // HJumpBox
            // 
            this.HJumpBox.Location = new System.Drawing.Point(322, 365);
            this.HJumpBox.Name = "HJumpBox";
            this.HJumpBox.Size = new System.Drawing.Size(67, 20);
            this.HJumpBox.TabIndex = 11;
            this.HJumpBox.Text = "2";
            // 
            // VJumpBox
            // 
            this.VJumpBox.Location = new System.Drawing.Point(436, 365);
            this.VJumpBox.Name = "VJumpBox";
            this.VJumpBox.Size = new System.Drawing.Size(67, 20);
            this.VJumpBox.TabIndex = 12;
            this.VJumpBox.Text = "2";
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(537, 363);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 13;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // IgnoreWhiteBox
            // 
            this.IgnoreWhiteBox.AutoSize = true;
            this.IgnoreWhiteBox.Checked = true;
            this.IgnoreWhiteBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IgnoreWhiteBox.ForeColor = System.Drawing.Color.White;
            this.IgnoreWhiteBox.Location = new System.Drawing.Point(12, 401);
            this.IgnoreWhiteBox.Name = "IgnoreWhiteBox";
            this.IgnoreWhiteBox.Size = new System.Drawing.Size(87, 17);
            this.IgnoreWhiteBox.TabIndex = 14;
            this.IgnoreWhiteBox.Text = "Ignore White";
            this.IgnoreWhiteBox.UseVisualStyleBackColor = true;
            // 
            // ColorDiffBox
            // 
            this.ColorDiffBox.Location = new System.Drawing.Point(509, 400);
            this.ColorDiffBox.Name = "ColorDiffBox";
            this.ColorDiffBox.Size = new System.Drawing.Size(96, 20);
            this.ColorDiffBox.TabIndex = 16;
            this.ColorDiffBox.Text = "80";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(374, 403);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Minimium Color Difference";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Presets";
            // 
            // SelectedPresets
            // 
            this.SelectedPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedPresets.FormattingEnabled = true;
            this.SelectedPresets.Location = new System.Drawing.Point(60, 16);
            this.SelectedPresets.Name = "SelectedPresets";
            this.SelectedPresets.Size = new System.Drawing.Size(148, 21);
            this.SelectedPresets.TabIndex = 18;
            this.SelectedPresets.SelectedIndexChanged += new System.EventHandler(this.SelectedPresets_SelectedIndexChanged);
            // 
            // RemoveSelectedPreset
            // 
            this.RemoveSelectedPreset.Location = new System.Drawing.Point(214, 12);
            this.RemoveSelectedPreset.Name = "RemoveSelectedPreset";
            this.RemoveSelectedPreset.Size = new System.Drawing.Size(100, 27);
            this.RemoveSelectedPreset.TabIndex = 19;
            this.RemoveSelectedPreset.Text = "Remove Selected";
            this.RemoveSelectedPreset.UseVisualStyleBackColor = true;
            this.RemoveSelectedPreset.Click += new System.EventHandler(this.RemoveSelectedPreset_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(322, 12);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(93, 27);
            this.UpdateButton.TabIndex = 20;
            this.UpdateButton.Text = "Update Presets";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // WindowTray
            // 
            this.WindowTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.WindowTray.Icon = ((System.Drawing.Icon)(resources.GetObject("WindowTray.Icon")));
            this.WindowTray.Text = "PictureBot";
            // 
            // ThreadSleepBox
            // 
            this.ThreadSleepBox.Location = new System.Drawing.Point(152, 405);
            this.ThreadSleepBox.Name = "ThreadSleepBox";
            this.ThreadSleepBox.Size = new System.Drawing.Size(56, 20);
            this.ThreadSleepBox.TabIndex = 22;
            this.ThreadSleepBox.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(149, 388);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Thread sleep";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 430);
            this.Controls.Add(this.ThreadSleepBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.RemoveSelectedPreset);
            this.Controls.Add(this.SelectedPresets);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ColorDiffBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.IgnoreWhiteBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.VJumpBox);
            this.Controls.Add(this.HJumpBox);
            this.Controls.Add(this.HeightBox);
            this.Controls.Add(this.WidthBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImageRGBType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TargetImageBox);
            this.Controls.Add(this.PaseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Draw Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TargetImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button PaseButton;
        private System.Windows.Forms.PictureBox TargetImageBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ImageRGBType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox WidthBox;
        private System.Windows.Forms.TextBox HeightBox;
        private System.Windows.Forms.TextBox HJumpBox;
        private System.Windows.Forms.TextBox VJumpBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.CheckBox IgnoreWhiteBox;
        private System.Windows.Forms.TextBox ColorDiffBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox SelectedPresets;
        private System.Windows.Forms.Button RemoveSelectedPreset;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.NotifyIcon WindowTray;
        private System.Windows.Forms.TextBox ThreadSleepBox;
        private System.Windows.Forms.Label label8;
    }
}

