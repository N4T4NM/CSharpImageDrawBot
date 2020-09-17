namespace PictureBot.ImageUtils
{
    partial class ReadingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.WaitFormShow = new System.Windows.Forms.Timer(this.components);
            this.ReadedPixels = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reading image, please wait...";
            // 
            // WaitFormShow
            // 
            this.WaitFormShow.Interval = 500;
            this.WaitFormShow.Tick += new System.EventHandler(this.WaitFormShow_Tick);
            // 
            // ReadedPixels
            // 
            this.ReadedPixels.AutoSize = true;
            this.ReadedPixels.Location = new System.Drawing.Point(12, 34);
            this.ReadedPixels.Name = "ReadedPixels";
            this.ReadedPixels.Size = new System.Drawing.Size(13, 13);
            this.ReadedPixels.TabIndex = 1;
            this.ReadedPixels.Text = "0";
            this.ReadedPixels.Click += new System.EventHandler(this.ReadedPixels_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(42, 61);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ReadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(161, 96);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ReadedPixels);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadingForm";
            this.ShowIcon = false;
            this.Text = "Reading Image...";
            this.Load += new System.EventHandler(this.ReadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer WaitFormShow;
        private System.Windows.Forms.Label ReadedPixels;
        private System.Windows.Forms.Button CancelButton;
    }
}