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
            }
            finally
            {
                xtr.Close();

            }
            return XmlDoc;
        }

        
    }
}
