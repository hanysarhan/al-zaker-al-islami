using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AzkarAlMuslim
{
    class AzkarHandling
    {
      public static XmlDocument XmlDoc = null;

       
        public static XmlDocument LoadXMlFile(string FilePath)
        {
            XmlTextReader xtr = null;
            try
            {
                xtr = new XmlTextReader(FilePath);
                xtr.WhitespaceHandling = WhitespaceHandling.None;
                XmlDoc = new XmlDocument();
                XmlDoc.Load(xtr);
                
            }

            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
                Util.write_to_log(ex.Message + "\n Action : can't read the xml file");
            }
            finally
            {
                xtr.Close();

            }
            return XmlDoc;
        }

        
    }
}
