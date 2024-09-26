using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Flowershop_Thesis.Temporary_Forms
{
    public partial class TryCamera : Form
    {
        public TryCamera()
        {
            InitializeComponent();
        }

       

        

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("microsoft.windows.camera:");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            // Set the filter for image files
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            // Set the initial directory to a specific folder (e.g., "C:\\Users\\YourUserName\\Pictures")
            open.InitialDirectory = @"C:\Users\ENZO\OneDrive\Pictures\Camera Roll";

            if (open.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                pictureBox1.Image = new Bitmap(open.FileName);
            }

        }
    }
}
