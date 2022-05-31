using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public partial class Overlay : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        
        
        public Overlay()
        {
            InitializeComponent();
            InitializeSettings();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            label1.Location = new Point(this.Width / 128, this.Height / 128);
        }
        

        private void Overlay_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.ShowInTaskbar = false;
            this.TransparencyKey = Color.DarkSlateGray;
            this.TopMost = true;
        
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }
        private void InitializeSettings()
        {
            switch (Globals.Program.SelectedTheme)
            {
                default:
                    label1.ForeColor = Color.Black;
                    label1.BackColor = Color.WhiteSmoke;
                    break;

                case "Legacy":
                    label1.ForeColor = Color.White;
                    label1.BackColor = Color.FromArgb(255, 46, 51, 73);
                    break;
        
                case "DarkMemories":
                    label1.ForeColor = Color.White;
                    label1.BackColor = Color.FromArgb(255, 44, 47, 51);
                    break;
        
                case "SaintsInaRow":
                    label1.ForeColor = Color.FromArgb(255, 146, 71, 214);
                    label1.BackColor = Color.FromArgb(255, 37, 13, 57);
                    break;
            }
        }
        
        private void Overlay_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = true;

        public async void UpdateQueueStatus(bool wasMatchFound, int queuePosition = 0)
        {
            if (Globals_Cache._OVERLAY.Visible)
            {
                await Task.Run(() =>
                {
                    if (wasMatchFound == true)
                    {
                        Globals_Cache._OVERLAY.Invoke(new Action(() =>
                        {
                            label1.Text = "MATCH FOUND";
                            SoundPlayer sPlayer = new SoundPlayer(Properties.Resources.ES_Gong_Hit);
                            sPlayer.Play();
                        }));
                    }
                    else
                    {
                        if (queuePosition == -1)
                        {
                            Thread.Sleep(8000);
                            Globals_Cache._OVERLAY.Invoke(new Action(() =>
                            {
                                label1.Text = string.Empty;
                            }));
                        }
                        else if (queuePosition == -2)
                        {
                            Globals_Cache._OVERLAY.Invoke(new Action(() =>
                            {
                                label1.Text = string.Empty;
                            }));
                        }
                        else
                            Globals_Cache._OVERLAY.Invoke(new Action(() =>
                            {
                                label1.Text = Convert.ToString(queuePosition);
                            }));
                    }
                });
            }
        }
        
        private async void TRACK_QUEUE()
        {
            //    === LENGTH TABLE ===
            //        4 = NONE
            //        6 = QUEUED
            //        7 = MATCHED
        
            //try
            //{
            //    await Task.Run(() =>
            //    {
            //        while (true)
            //        {
            //            if (Globals.FIDDLERCORE_VALUE_QUEUEPOSITION == "NONE" || (label1.Text.Length == 7 && Globals.FIDDLERCORE_VALUE_QUEUEPOSITION.Length == 7))
            //                label1.Invoke(new Action(() => { label1.Text = String.Empty; }));
            //            else
            //            {
            //                if (IsMatchFound == true)
            //                    label1.Invoke(new Action(() => { label1.Text = String.Empty; }));
            //                else
            //                {
            //                    label1.Invoke(new Action(() => { label1.Text = Globals.FIDDLERCORE_VALUE_QUEUEPOSITION; }));
            //                    if (Globals.FIDDLERCORE_VALUE_QUEUEPOSITION.Length == 7)
            //                    {
            //                        IsMatchFound = true;
            //                        Thread.Sleep(9000);
            //                    }
            //                }
            //            }
            //            Thread.Sleep(1000);
            //        }
            //
            //    });
            //}
            //catch { TRACK_QUEUE(); }
        }
    }
}
