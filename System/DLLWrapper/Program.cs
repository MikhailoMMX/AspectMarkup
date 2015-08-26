using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace DLLWrapper
{
    static class Program
    {
        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("AspectCore,"))
            {
                RegistryKey rk = null;
                string Path = "";
                rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("Software\\AspectCore");
                if (rk != null)
                    Path = (string)rk.GetValue("Install_Dir");
                else
                    return null;

                return Assembly.LoadFrom(Path + "\\AspectCore.dll");
            }
            return null;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
