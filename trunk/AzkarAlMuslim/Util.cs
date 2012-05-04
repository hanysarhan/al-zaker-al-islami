using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using System.Security;
using System.Net.NetworkInformation;
using System.IO;

namespace AzkarAlMuslim
{
    class Util
    {
        public static int timeToDisplayBallon = 0;
        public static int diffTimeBetwenAzkar = 0;
       static IUpdateDisplayTime _mainForm = null;
        static IupdateAzkar _mainFormAzkar = null;
        public static void addMainFormListener(IUpdateDisplayTime ZekrTime,IupdateAzkar updateAzkar)
        {
            _mainForm = ZekrTime;
            _mainFormAzkar = updateAzkar;
        }

        public static void SetWorkingSet(int lnMaxSize, int lnMinSize)
        {
            System.Diagnostics.Process loProcess = System.Diagnostics.Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = (IntPtr)lnMaxSize;
            loProcess.MinWorkingSet = (IntPtr)lnMinSize;
            //long lnValue = loProcess.WorkingSet; // see what the actual value
        }

       // [DllImport("kernel32.dll", SetLastError = true)]
       //public static extern int SetProcessWorkingSetSize(int hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);

        public static void updateAzkarItems()
        {
            _mainFormAzkar.updateAlAzkar();
        }
        public static void updateDisplayTime(int newTime)
        {
            XmlNodeList xmlNode = AzkarHandling.XmlDoc.GetElementsByTagName("DisplayTime");
            xmlNode[0].InnerText = newTime.ToString();
            AzkarHandling.XmlDoc.Save(getXmlFilePath());
            timeToDisplayBallon = newTime*1000;
           // _mainForm.updateTime();

        }
        public static void updateDiffTime(int newTime)
        {
            XmlNodeList xmlNode = AzkarHandling.XmlDoc.GetElementsByTagName("DiffTime");
            xmlNode[0].InnerText = newTime.ToString();
            AzkarHandling.XmlDoc.Save(getXmlFilePath());
            diffTimeBetwenAzkar = newTime*60*1000;
            _mainForm.updateDiffTime();

        }
        public static void SetCurrentDisplayTime()
        {
            XmlNodeList xmlNode = AzkarHandling.XmlDoc.GetElementsByTagName("DisplayTime");
            timeToDisplayBallon = int.Parse(xmlNode[0].InnerText)*1000;
        }
        public static void SetCurrentDiffTime()
        {
            XmlNodeList xmlNode = AzkarHandling.XmlDoc.GetElementsByTagName("DiffTime");
            diffTimeBetwenAzkar = int.Parse(xmlNode[0].InnerText)*60*1000;
        }
        public static void write_to_log(string s)
        {
              string path = get_log_file_path();
            
                System.IO.StreamWriter w = System.IO.File.AppendText(path);
                w.WriteLine( s+ " "+DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                w.Close();
            
        }
        public static bool is_datetime(string maybe_datetime,out DateTime d)
        {
           

            try
            {
                if (DateTime.TryParse(maybe_datetime, out d))
                {
                    d = DateTime.Parse(maybe_datetime, get_culture_info());
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                d = new DateTime();
                return false;
            }
        }
        public static System.Globalization.CultureInfo get_culture_info()
        {
            // Create a basic culture object to provide also all input parsing
            return new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
        public static string get_log_file_path()
        {
            XmlNode logPath = AzkarHandling.XmlDoc.GetElementsByTagName("path")[0];
            return logPath.InnerText;
        }
        public static string getXmlFilePath()
        {
            
            return  Application.StartupPath+"\\Azkar.xml";
        }
        public static bool is_int(string maybe_int)
        {
            
                int number;
           bool result= Int32.TryParse(maybe_int, out number);
           return result;
           
        }
        public  static void RunStartup(Boolean RunOnStartup)
        {
            //RegistryKey key 1= Registry.CurrentUser.("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
              //string _currentValue= key.GetValue("AzkarAlMuslim").ToString();
                string[] _values = key.GetValueNames();
                for (int i = 0; i < _values.Length; i++)
                {
                    if (_values[i].ToLower() == "AzkarAlMuslim".ToLower() )
                    {
                        return;
                    }
                }
            }

            if (RunOnStartup == true)
            {
                key.SetValue("AzkarAlMuslim", Application.ExecutablePath.ToString());
            }
            else
            {
                key.DeleteValue("AzkarAlMuslim", false);
            }

        }

        public static bool checkAnotherProcessRunning(string ProcessName)
        {

            // check if this process loaded in taskbar tray
            Process[] process = Process.GetProcessesByName(ProcessName);
            if (process.Length > 1)
            {
                  return true;
            }
            else
                return false;

        }
        /// <summary>
        /// Method used to check for internet connectivity by piging
        /// varoaus websites and looking for the response.
        /// </summary>
        /// <returns>True if a ping succeeded, False if otherwise.</returns>
        /// <remarks></remarks>
        public static bool isConnectionAvailable(out string _returnMessage)
        {
            //build a list of sites to ping, you can use your own
            string[] sitesList = { "www.google.com", "www.alzaker.net" };
            //create an instance of the System.Net.NetworkInformation Namespace
            Ping ping = new Ping();
            //Create an instance of the PingReply object from the same Namespace
            PingReply reply;
            bool _success = false;
            //int variable to hold # of pings not successful
            int notReturned = 0;
            try
            {
                //start a loop that is the lentgh of th string array we
                //created above
                for (int i = 0; i <sitesList.Length; i++)
                {
                    //use the Send Method of the Ping object to send the
                    //Ping request
                    reply = ping.Send(sitesList[i], 1000);
                    //now we check the status, looking for,
                    //of course a Success status
                    if (reply.Status != IPStatus.Success)
                    {
                        //now valid ping so increment
                        notReturned += 1;
                    }
                  
                }
                //check to see if any pings came back
                if (notReturned == sitesList.Length)
                {
                    _success = false;
                    _returnMessage = "ÚÝæÇ ¡ ÚäÏ ÊÍÏíË ÇáÇÐßÇÑ ¡ íÌÈ Çä Êßæä ãÊÕáÇ ÈÇáÇäÊÑäÊ ¡ áßí íÊã ÇáÇÊÕÇá ÈãæÞÚ ÇáÐÇßÑ æÊäÒíá ÇÎÑ ÇáÇÐßÇÑ ÇáãÖÇÝÉ";
                   return _success;
                }
                else
                {
                    _returnMessage = "Êã ÇáÇÊÕÇá ÈÇáÇäÊÑäÊ ÈäÌÇÍ ÇáÇä íãßäß ÊÍÏíË ÇáÇÐßÇÑ æÇÖÇÝÉ ÇÐßÇÑ ÌÏíÏå íæãíÇ ãä Úáì ãæÞÚ ÇáÐÇßÑ ÇáÇÓáÇãí";
                    _success = true;
                }
            }
          
            catch (Exception ex)
            {
                _success = false;
                _returnMessage ="áã íÊã ÇáÇÊÕÇá ÈÇáÇäÊÑäÊ äÊíÌå áÍÏæË ãÔßáå ãä ÝÖáß ÊÃßÏ ãä ÇÊÕÇáß ÈÇáÇäÊÑäÊ";
            }
            return _success;
        }


    }
}
