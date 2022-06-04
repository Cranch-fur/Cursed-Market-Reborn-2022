using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public partial class MatchInformer : Form
    {
        private static bool IsKillerOnSteam = false;
        private static string KillerSteamProfile = null;
        public static string FiddlerCoreObtainedMatchInfo = null;
        
        
        public MatchInformer()
        {
            InitializeComponent();
            InitializeSettings();
        }
        
        private void MatchInformer_Load(object sender, EventArgs e)
        {
        }
        private void InitializeSettings()
        {
            switch (Globals.Program.SelectedTheme)
            {
                default:
                    pictureBox1.Visible = true;
                    this.BackColor = Color.White;
                    panel1.BackColor = SystemColors.Control;
                    pictureBox2.BackColor = SystemColors.Control;
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Gainsboro;
                    label3.ForeColor = Color.Black;
                    label4.ForeColor = Color.DimGray;
                    label5.ForeColor = Color.DimGray;
                    label6.ForeColor = Color.DimGray;
                    label7.ForeColor = Color.DimGray;
                    label8.ForeColor = Color.Gainsboro;
                    break;

                case "Legacy":
                    pictureBox1.Visible = false;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel1.BackColor = Color.FromArgb(255, 24, 30, 54);
                    pictureBox2.BackColor = SystemColors.ControlDark;
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.DimGray;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.Gainsboro;
                    label5.ForeColor = Color.Gainsboro;
                    label6.ForeColor = Color.Gainsboro;
                    label7.ForeColor = Color.Gainsboro;
                    label8.ForeColor = Color.DimGray;
        
                    break;
        
                case "DarkMemories":
                    pictureBox1.Visible = false;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel1.BackColor = Color.FromArgb(255, 35, 39, 42);
                    pictureBox2.BackColor = SystemColors.ControlDark;
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.DimGray;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.Gainsboro;
                    label5.ForeColor = Color.Gainsboro;
                    label6.ForeColor = Color.Gainsboro;
                    label7.ForeColor = Color.Gainsboro;
                    label8.ForeColor = Color.DimGray;
                    break;
        
                case "SaintsInaRow":
                    pictureBox1.Visible = false;
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel1.BackColor = Color.FromArgb(255, 55, 20, 86);
                    pictureBox2.BackColor = SystemColors.ControlDark;
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.DimGray;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.Gainsboro;
                    label5.ForeColor = Color.Gainsboro;
                    label6.ForeColor = Color.Gainsboro;
                    label7.ForeColor = Color.Gainsboro;
                    label8.ForeColor = Color.DimGray;
                    break;
            }
        }
        
        private void MatchInformer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        
        private async void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false; await Task.Run(() =>
            {
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref mouse);
            });
        }
        
        
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            //if (Globals.FIDDLERCORE_VALUE_CURRENTMATCHID != null)
            //    MatchInformer_GetAndShowMatchInfo();
            //else
            //{
            //    label3.Text = "KILLER NOT OBTAINED";
            //    pictureBox2.Image = Properties.Resources.SL_Unknown;
            //    label4.Text = "";
            //    label5.Text = "";
            //    label6.Text = "";
            //    label7.Text = "";
            //}
        }
        
        private void label4_Click(object sender, EventArgs e)
        {
            if (IsKillerOnSteam == true)
                Process.Start(KillerSteamProfile);
        }
    }
}
