﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;


namespace ClientPatcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string Path = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\Open Meridian");
            if (!Directory.Exists(Path)) //if we're not installed yet, we need admin this run
            {
                MessageBox.Show("In order to set up your computer for the Meridian 59 Patcher, you will need to allow it to run as an administrator once.");
                if (IsAdministrator() == false) 
                {
                    // Restart program and run as admin
                    var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    startInfo.Verb = "runas";
                    System.Diagnostics.Process.Start(startInfo);
                    Application.Exit();
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientPatchForm());
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }


    }
}