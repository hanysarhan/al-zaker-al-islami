using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AzkarAlMuslim
{
    public partial class FrmAddZekr : Form
    {
        FrmDisplayingZekr _frmRmvZekr = null;
        public FrmAddZekr()
        {
            InitializeComponent();
        }
        public FrmAddZekr(FrmDisplayingZekr frm)
        {
            InitializeComponent();
            _frmRmvZekr = frm;
        }
      
        private void AddNewZekr()
        {
            // get xmlNode using xpath instead of using getElementByID() that works only with xml file that has DTD 
       
            XmlElement newZekr = AzkarHandling.XmlDoc.CreateElement("Zekr");
             newZekr.SetAttribute("title", cmbAzkarTypes.Text);
            newZekr.SetAttribute("content", txtZekrContent.Text.Trim());
             XmlNode currentZekrNode=AzkarHandling.XmlDoc.GetElementsByTagName("Azkar")[0];
              currentZekrNode.AppendChild(newZekr);
             AzkarHandling.XmlDoc.Save(Util.getXmlFilePath());
             resetControls();
        }
        private void resetControls()
        {
            cmbAzkarTypes.SelectedIndex = -1;
            txtZekrContent.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            this.Dispose();
        }

        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    AddNewZekr();
                    Util.updateAzkarItems();
                    _frmRmvZekr.updateCurrentListoOfZekr();
                    Util.write_to_log(" Action: Adding New Zekr --- Status: Added Successfully");
                }
            }
            catch (Exception ex)
            {
                  Util.write_to_log(ex.Message+"\n Action: Adding New Zekr");
            }
        }
        private bool IsValid()
        {
            
            if (cmbAzkarTypes.SelectedIndex < 0)
            {
                MessageBox.Show("ãä ÝÖáß ÅÎÊÇÑ ÇáÝÆå");
                return false;
            }
            if (txtZekrContent.Text.Trim().Length <= 0)
            {
                MessageBox.Show("ãä ÝÖáß ÃÏÎá ãÍÊæì ÇáÐßÑ");
                return false;
            }
            return true;
        }

        private void FrmAddZekr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
        }
    }
}