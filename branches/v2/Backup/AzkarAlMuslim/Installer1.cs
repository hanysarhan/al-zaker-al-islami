using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using Microsoft.Win32;

namespace AzkarAlMuslim
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }
      
     
        protected override void OnAfterUninstall(System.Collections.IDictionary savedState)
        { 
            removeRegistryValue();
            base.OnAfterUninstall(savedState);
        }
        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {  
            removeRegistryValue();
            base.OnBeforeInstall(savedState);
        }
   
        public static void removeRegistryValue()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
                //string _currentValue= key.GetValue("AzkarAlMuslim").ToString();
                string[] _values = key.GetValueNames();
                for (int i = 0; i < _values.Length; i++)
                {
                    if (_values[i].ToLower() == "AzkarAlMuslim".ToLower())
                    {
                        key.DeleteValue("AzkarAlMuslim", false);
                    }
                }
            }

         
        }
    }
}