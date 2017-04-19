using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBoxTest
{
    public partial class Form1 : Form
    {
        public bool isLoading { get; set; }
        public Form1()
        {
            InitializeComponent();
            isLoading = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            isLoading = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox3.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox3.Checked = true;
        }

        //private void checkBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!isLoading)
        //    {
        //        if (checkBox1.Checked)
        //        {
        //            checkBox2.Checked = false;
        //            checkBox3.Checked = false;
        //        }
        //        else if (checkBox2.Checked)
        //        {
        //            checkBox1.Checked = true;
        //            checkBox3.Checked = false;
        //        }
        //        else if (checkBox3.Checked)
        //        {
        //            checkBox1.Checked = true;
        //            checkBox2.Checked = true;
        //        }
        //    }
        //}
    }
}
