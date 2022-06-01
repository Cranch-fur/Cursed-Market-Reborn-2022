using Microsoft.Win32;
using System;
using System.Linq;

namespace Cursed_Market_Reborn
{
    public static class WinReg
    {
        public const string fullRegistryPath = @"HKEY_CURRENT_USER\SOFTWARE\Cursed Market 2022";
        public const string RegistryPath = @"SOFTWARE\Cursed Market 2022";


        public static string GetValue(string DWORD32, string DefaultReturn = null)
        {

            object value = Registry.GetValue(fullRegistryPath, DWORD32, DefaultReturn);
            if (value != null)
                return Convert.ToString(value);
            else return DefaultReturn;
        }

        public static int GetValue_INT32(string DWORD32, int DefaultReturn = 0)
        {
            object value = Registry.GetValue(fullRegistryPath, DWORD32, DefaultReturn);
            if (value != null)
                return Convert.ToInt32(value);
            else return DefaultReturn;
        }

        public static bool SetValue(string DWORD32, string VALUE)
        {
            try
            {
                Registry.SetValue(fullRegistryPath, DWORD32, VALUE); return true;
            }
            catch { return false; }
        }

        public static bool SetValue_INT32(string DWORD32, int VALUE)
        {
            try
            {
                Registry.SetValue(fullRegistryPath, DWORD32, VALUE); return true;
            }
            catch { return false; }
        }

        public static bool DestroyCurrentUserSubKeyTree(string PATH)
        {
            try
            {
                if (Registry.CurrentUser.GetSubKeyNames().Contains(PATH))
                    Registry.CurrentUser.DeleteSubKeyTree(PATH);

                return true;
            }
            catch { return false; }
        }

        public static bool DisableProxy()
        {
            try
            {
                string path = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";
                Registry.SetValue(path, "ProxyEnable", 0);
                Registry.SetValue(path, "ProxyServer", "");
                Registry.SetValue(path, "ProxyOverride", "");
                return true;
            }
            catch (Exception e)
            {
                Messaging.ShowMessage($"Cursed Market Failed To Disable Proxy!\nReason: {e.Message}");
                return false;
            }
        }
    }
}
