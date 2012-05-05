using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AzkarAlMuslim
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://zaker.linkaty.net");
            this.Close();
            this.Dispose();
        }

        private void frmAbout_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }
    }
}