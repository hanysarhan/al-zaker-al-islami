using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AzkarAlMuslim
{
    public partial class frmDownloadingAzkar : Form
    {
        public frmDownloadingAzkar()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label11.Visible == true && label12.Visible == true && label13.Visible == true && label14.Visible == true && label16.Visible == true)
            {
                label13.Visible = true;
                label11.Visible = false;
                label14.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
            }
            else if (label13.Visible == true && label11.Visible == false)
            {
                label11.Visible = true;
                label12.Visible = false;
                label13.Visible = true;
                label14.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
            }
            else if (label13.Visible == true && label12.Visible == false)
            {
                label12.Visible = true;
                label13.Visible = false;
                label14.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
            }
            else if (label14.Visible == true && label13.Visible == false)
            {
                label12.Visible = true;
                label11.Visible = true;
                label13.Visible = true;
                label14.Visible = false;
                label16.Visible = true;
                label17.Visible = true;
            }
            else if (label16.Visible == true && label14.Visible == false)
            {
                label12.Visible = true;
                label11.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label16.Visible = false;
                label17.Visible = true;
            }
            else if (label17.Visible == true && label16.Visible == false)
            {
                label12.Visible = true;
                label11.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label16.Visible = true;
                label17.Visible = false;
            }
        }

        private void frmDownloadingAzkar_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }

    }
}