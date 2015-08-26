using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace AspectCore.Helpers
{
    internal class RegistryHelper
    {
        public static void SaveToRegistry(string key, object value, string Subkey = AspectCore.Strings.RegistryRoot)
        {
            RegistryKey rk = null;
            try
            {
                //!Aspect tmp
                rk = Registry.CurrentUser.CreateSubKey(Subkey);
                if (rk == null)
                    return;
                rk.SetValue(key, value);
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }
        public static object ReadFromRegistry(string key, string subkey = Strings.RegistryRoot)
        {
            RegistryKey rk = null;
            object result = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(subkey);
                if (rk != null)
                    result = rk.GetValue(key);
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
            return result;
        }
    }
}
