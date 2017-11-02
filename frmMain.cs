using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myCountdown
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);

            //monthCalendar1.SelectionRange = new SelectionRange(DateTime.Now, new DateTime(2017, 11, 15));
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.SetDate(new DateTime(2017, 11, 15));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
