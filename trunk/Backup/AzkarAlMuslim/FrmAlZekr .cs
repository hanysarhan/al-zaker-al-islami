using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Threading;
using System.IO;

namespace AzkarAlMuslim
{
    public partial class FrmAlZekr : Form,IUpdateDisplayTime,IupdateAzkar
    {
        #region varaiables

           int _start = 0;
           private const string ProcessName = "AzkarAlMuslim";
           XmlDocument XmlDoc = null;
           XmlNodeList _allAzkarItems = null;
           XmlDocument _onlineDoc=null;
        int _newAzkar = 0;
       #endregion
        public FrmAlZekr()
        {
            InitializeComponent();
            try

            {
                if (!File.Exists(Application.StartupPath + "\\Azkar.xml"))
                {
                    MessageBox.Show("·„ Ì „ «·⁄ÀÊ— ⁄·Ï „·› «·«–ﬂ«— \r\n  „‰ ›÷·ﬂ «Ã⁄·Â »ÃÊ«— «·„·› «· ‰›Ì–Ï ÊÕ«Ê·  ‘€Ì· «·»—‰«„Ã „—Â √Œ—Ï");
                    Close();
                    Environment.Exit(1210);
                }

                Util.addMainFormListener(this, this);
                XmlDoc = AzkarHandling.LoadXMlFile(Util.getXmlFilePath());
               
                Util.SetCurrentDisplayTime();
                Util.SetCurrentDiffTime();
                SetupInitialValues();
              
               _allAzkarItems = XmlDoc.GetElementsByTagName("Azkar");
                // start timer to start displaying al azkar
               timer1.Enabled = true;
            }

            catch (Exception ex)
            {
                Util.write_to_log(ex.Message +"\n Action : main form constructor and loading xml file");
            }
         }
        private void startUpdating()
        {
            string _onlineAzkar = getOnlineZekr();
            if (_onlineAzkar != null)
            {
                _onlineDoc = new XmlDocument();
                _onlineDoc.LoadXml(_onlineAzkar);

                // start thread responsible for updating current azkar after getting and downloading online azkar .
                updateCurrentAzkarFromOnlineAzkar();
                // update current items 
                _allAzkarItems = XmlDoc.GetElementsByTagName("Azkar");
                // update xml file
                XmlDoc.Save(Util.getXmlFilePath());
                Thread.Sleep(1000);
            }
            else
            {
                MessageBox.Show("⁄›Ê« ·ﬁœ ›‘·  ⁄„·ÌÂ «· ÕœÌÀ „‰ ›÷·ﬂ Õ«Ê· „—Â √Œ—Ï ");
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate
                {
                    frm.Close();
                    frm.Dispose();
                    if (_newAzkar != 0)
                    {
                        MessageBox.Show(" „  ‰“Ì· " + _newAzkar + " –ﬂ— »‰Ã«Õ");
                        _newAzkar = 0;
                    }
                    else
                        MessageBox.Show("·„ Ì „ ≈÷«›Â √–ﬂ«— ÃœÌœÂ »⁄œ");
                }));
            }
            else
            {
                frm.Close();
                frm.Dispose();
                if (_newAzkar != 0)
                {
                    MessageBox.Show(" „  ‰“Ì· " + _newAzkar + " –ﬂ— »‰Ã«Õ");
                    _newAzkar = 0;
                }
                else
                    MessageBox.Show("·„ Ì „ ≈÷«›Â √–ﬂ«— ÃœÌœÂ »⁄œ");
            }
        }
        frmDownloadingAzkar frm;
        private void getOnlineAzkar()
        {
            // check if user updated xml today (as it's alloed only once in aday)
            //DateTime _lastUpdated = Convert.ToDateTime(XmlDoc.GetElementsByTagName("LastUpdated")[0].InnerText);
            //if (!_lastUpdated.ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
            //{
            string returnMessage = "";
                if (!Util.isConnectionAvailable(out returnMessage))
                {
                    Util.write_to_log(returnMessage);
                    MessageBox.Show(returnMessage);
                }
                else
                {
                    
                         frm = new frmDownloadingAzkar();
                        Thread th = new Thread(new ThreadStart(startUpdating));
                        th.Start();
                        frm.ShowDialog();
                  
                    // check by using thread if theres is new azkar then update xml file and update _allAzkarItems 
                    // and notify user with success of updating al azkar
                }
               
           // }
            // **************************//
        }
        // start to update current azkar from online azkar just downloaded
        private void updateCurrentAzkarFromOnlineAzkar()
        {
            
            XmlNode _currentOnlineAzkar= _onlineDoc.GetElementsByTagName("Azkar")[0];
            XmlNode _currentAzkar = XmlDoc.GetElementsByTagName("Azkar")[0];

            foreach (XmlNode OnlineNode in _currentOnlineAzkar.ChildNodes)
            {
                bool found = false;
                foreach (XmlNode currentNode in _currentAzkar.ChildNodes)
                {
                    if (currentNode.Attributes["content"].Value.ToLower() == OnlineNode.Attributes["content"].Value)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    _newAzkar++;
                   XmlDoc.GetElementsByTagName("Azkar")[0].AppendChild(XmlDoc.ImportNode(OnlineNode, true));
                }
            }
           
        }

        private string getOnlineZekr()
        {
            string result = null;
            string url = "http://zaker.linkaty.net/rss.aspx";
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                // handle error
                MessageBox.Show(ex.Message);
                Util.write_to_log(ex.Message);
                return result;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }
            return result;
        }
        private void SetupInitialValues()
        {
         
            timer1.Interval = Util.diffTimeBetwenAzkar;
            toolStripStartDisplaying.Enabled = false;
        }
    
     

        private void btn_Start_Displaying_Azkar_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }
      
        void DisplayAzkar()
        {
           // XmlNodeList xmlNode = XmlDoc.GetElementsByTagName("Azkar");
            // check if we reached to end of xml file -- 
            // if yes reset flag _start=0; to start from begining
            if (_allAzkarItems[0].ChildNodes.Count == _start)
                _start = 0;
            notifyIcon1.ShowBalloonTip(Util.timeToDisplayBallon, _allAzkarItems[0].ChildNodes[_start].Attributes["title"].Value, _allAzkarItems[0].ChildNodes[_start].Attributes["content"].Value, System.Windows.Forms.ToolTipIcon.Info);
             
                 _start++;
        }
       

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
              
                DisplayAzkar();
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                Util.write_to_log(ex.Message);
               
            }
        }

        private void btn_Stop_Displaying_Azkar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

     

        private void toolStripStartDisplaying_Click(object sender, EventArgs e)
        {
            timer1.Start();
            toolStripStopDisplaying.Enabled = true;
            toolStripStartDisplaying.Enabled = false;
        }

        private void toolStripStopDisplaying_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            toolStripStopDisplaying.Enabled = false;
            toolStripStartDisplaying.Enabled = true;
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void FrmAlZekr_Load(object sender, EventArgs e)
        {
            try
            {
                if (Util.checkAnotherProcessRunning(ProcessName))
                {
                    this.Close();
                    Dispose();
                }
                else
                {
                    BeginInvoke(new MethodInvoker(delegate
                  {
                      this.Hide();
                  }));
                }
                Util.RunStartup(true);
            }
            catch (Exception ex)
            {
                 Util.write_to_log(ex.Message);

            }
        }

        private void toolStripAddNewItem_Click(object sender, EventArgs e)
        {
            FrmAddZekr frm = new FrmAddZekr();
            frm.ShowDialog();
        }

        private void toolStridisplayTime_Click(object sender, EventArgs e)
        {
            FrmDisplayTimeZekr frmTime = new FrmDisplayTimeZekr();
            frmTime.ShowDialog();
            
        }



        #region IUpdateDisplayTime Members

        //public void updateDisplayTime()
        //{
        //    timer1.Interval = Util.timeToDisplayBallon;
        //}

        public void updateDiffTime()
        {
            timer1.Interval = Util.diffTimeBetwenAzkar;
        }

        #endregion

        private void toolStripRemoveZekr_Click(object sender, EventArgs e)
        {
            FrmDisplayingZekr frmRmv = new FrmDisplayingZekr();
            frmRmv.ShowDialog();
        }

        #region IupdateAzkar Members

        public void updateAlAzkar()
        {
            _allAzkarItems = XmlDoc.GetElementsByTagName("Azkar");
        }

        #endregion

        private void ToolStripUpdateAzkar_Click(object sender, EventArgs e)
        {
            getOnlineAzkar();
        }

        private void ToolStripAddOnlineAzkar_Click(object sender, EventArgs e)
        {
            frmAbout frmabout = new frmAbout();
            frmabout.ShowDialog();
        }

        private void FrmAlZekr_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }
    }
}