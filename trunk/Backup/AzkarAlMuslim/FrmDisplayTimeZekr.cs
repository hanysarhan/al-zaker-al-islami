using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AzkarAlMuslim
{
    public partial class FrmDisplayTimeZekr : Form
    {
        int _currentTime = 0;
        public FrmDisplayTimeZekr()
        {
            InitializeComponent();
            _currentTime = Util.timeToDisplayBallon;
            NumDisTime.Value = Util.timeToDisplayBallon/1000;
            NumDiffTime.Value = Util.diffTimeBetwenAzkar/(60*1000);
        }

     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                //if (isValid())
                //{
                    Util.updateDisplayTime(int.Parse(NumDisTime.Value.ToString()));
                    Util.updateDiffTime(int.Parse(NumDiffTime.Value.ToString()));
                   MessageBox.Show(" „  ⁄œÌ· Êﬁ  «·–ﬂ— »‰Ã«Õ");
                    Close();
                //}
                //else
                //{
                //    MessageBox.Show("„‰ ›÷·ﬂ √œŒ· ⁄œœ ’ÕÌÕ");
                //}
            }
            catch (Exception ex)
            {

                Util.write_to_log(ex.Message +"\n Acion: updating Displaying Time");
            }
        }
        private bool isValid()
        {
            return true;
           // return Util.is_int(tbxTime.Text.Trim());
        }

        private void FrmDisplayTimeZekr_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }
    }
}