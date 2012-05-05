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
    public partial class FrmDisplayingZekr : Form
    {
        XmlDocument _XmlDoc = null;
        XmlNode _allAzkar;
        int count = 0;
        public FrmDisplayingZekr()
        {
            InitializeComponent();
            _XmlDoc = AzkarHandling.XmlDoc;
            getCurrentAzkar();
        }
        private void getCurrentAzkar()
        {
            _allAzkar = _XmlDoc.GetElementsByTagName("Azkar")[0];
            count = _allAzkar.ChildNodes.Count;
            lblAzkarCounts.Text = count.ToString();
        }
        private void GetAllAzkar()
        {
            string _title,_content,_date;
            ListViewItem _alZekr = null;
           _date= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            for (int i = 0; i <count; i++)
            {
                try
                {
                    //DateTime _zekrDate;
                    _alZekr = new ListViewItem(i.ToString());
                    _title = _allAzkar.ChildNodes[i].Attributes["title"].Value;
                    _alZekr.SubItems.Add(new ListViewItem.ListViewSubItem(_alZekr, _title, Color.DarkSlateGray, Color.DarkSalmon, new Font("arial", 10)));
                    _content = _allAzkar.ChildNodes[i].Attributes["content"].Value;
                    _alZekr.SubItems.Add(new ListViewItem.ListViewSubItem(_alZekr, _content, Color.DarkSlateGray, Color.DarkSalmon, new Font("arial", 10)));
                    //_date = _allAzkar.ChildNodes[i].Attributes["crdt"].Value;
                    //_alZekr.SubItems.Add(new ListViewItem.ListViewSubItem(_alZekr, (Util.is_datetime(_date, out _zekrDate) ? _zekrDate.ToString() : ""), Color.DarkSlateGray, Color.DarkSalmon, new Font("arial", 10)));
                    lstViewAllAzkar.Items.Add(_alZekr);
                }
                catch (NullReferenceException ex)
                {
                    lstViewAllAzkar.Items.Add(_alZekr);
                    Util.write_to_log(ex.Message + "\n action : while displaying remove azkar form");
                    continue;
                }
                catch (Exception e)
                {
                    Util.write_to_log(e.Message );
                    continue;
                }
            }
        }

        private void FrmRemoveZekr_Load(object sender, EventArgs e)
        {
            GetAllAzkar();
            //Util.SetWorkingSet(750000, 300000);
        }

        private void _cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_cbSelectAll.Checked)
            {
                foreach (ListViewItem Item in lstViewAllAzkar.Items)
                {
                    Item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem Item in lstViewAllAzkar.Items)
                {

                    Item.Checked = false;
                }
            }
        }

        private void lstViewAllAzkar_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                
                _btnDelete.Enabled = true;
              
            }
            else
            {
                bool all_UnChecked = true;
                foreach (ListViewItem l in lstViewAllAzkar.Items)
                {
                    if (l.Checked)
                        all_UnChecked = false;

                }
                if (all_UnChecked)
                {
                    _btnDelete.Enabled = false;
                   
                }
            }
        }

        private void _btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                 RemoveSelectedZekr();
                 Util.updateAzkarItems();
                 Util.write_to_log(" Action :removeing zekr successfully --- Status: Removed successfully");
                 _btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                Util.write_to_log(ex.Message+"\n Action :removeing zekr");
            }
        }
        private void RemoveSelectedZekr()
        {
             
          XmlNode allAzkar= AzkarHandling.XmlDoc.GetElementsByTagName("Azkar")[0];
          int _count = allAzkar.ChildNodes.Count;
          for (int m=0;m<lstViewAllAzkar.Items.Count;m++)
          {
              if (lstViewAllAzkar.Items[m].Checked)
              {
                  for (int i = 0; i < _count; i++)
                  {

                      if (allAzkar.ChildNodes[i].Attributes["content"].Value.ToLower() == lstViewAllAzkar.Items[m].SubItems[2].Text.ToString().ToLower())
                      {
                          allAzkar.RemoveChild(allAzkar.ChildNodes[i]);
                          AzkarHandling.XmlDoc.Save(Util.getXmlFilePath());
                          lstViewAllAzkar.Items.Remove(lstViewAllAzkar.Items[m]);
                          count--;
                          m--;
                          lblAzkarCounts.Text = count.ToString();
                          break;
                      }
                  }
              }
          }
        }

        private void btnAddNewZekr_Click(object sender, EventArgs e)
        {
            FrmAddZekr frm = new FrmAddZekr(this);
            frm.ShowDialog();
        }
        public void updateCurrentListoOfZekr()
        {
            lstViewAllAzkar.Items.Clear();
            getCurrentAzkar();
            GetAllAzkar();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            this.Dispose();
        }

        private void FrmDisplayingZekr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
        }
    }
}