using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Event_ApplicationException);
            Application.Run(new Form1());
        }

        static void Event_ApplicationException(object sender, ThreadExceptionEventArgs e)
        {
            DialogResult result = MessageBox.Show("Cursed Market Experienced Critical Error!\nDo you Prefer to see Detailed Log?\n\n\"Cancel\" - Don't log this crash", "Cursed Market Critical Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            
            string dataFolder = Globals.SelfDataFolder ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Random rnd = new Random();
            string dateTime = Globals.GetCurrentDateTime() ?? $"[{rnd.Next(1, 1000000)}]";
            string path = $"{dataFolder}\\{dateTime} Cursed Market Crash.txt";

            try
            {
                switch (result)
                {
                    case DialogResult.Yes:
                        System.IO.File.WriteAllText(path, e.Exception.ToString());
                        Process.Start(path);
                        break;

                    case DialogResult.No:
                        Messaging.ShowMessage(e.Exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;

                    default:
                        break;
                }
            }
            catch { MessageBox.Show(e.Exception.ToString()); }
        }
    }
}
