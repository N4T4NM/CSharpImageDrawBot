using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBot.InputUtils
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        public string PresetName { get; set; }
        private void InputForm_Load(object sender, EventArgs e)
        {
            PresetBox.Text = PresetName;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            PresetName = PresetBox.Text;
            DialogResult = DialogResult.OK;
        }

        private void PresetBox_TextChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = PresetBox.Text.Length > 0;
        }
    }
}
