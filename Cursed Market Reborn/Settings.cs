using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public partial class Settings : Form
    {
        static bool HasAnythingChanged = false;
        public Settings()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            ObtainLimitedPlatforms();

            if (Globals_Session.Creator.Discord != null)
                pictureBox5.Visible = true;
        }
        private void InitializeSettings()
        {
            switch (Globals.Program.SelectedTheme)
            {
                default:
                    comboBox1.SelectedIndex = 0;
                    this.BackColor = Color.White;
                    panel1.BackColor = SystemColors.Control;
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Black;
                    label3.ForeColor = Color.Black;
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    pictureBox5.Image = Properties.Resources.ICON_SMALL_DISCORD_BLACK;
                    break;

                case "Legacy":
                    comboBox1.SelectedIndex = 1;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel1.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    label6.ForeColor = Color.White;
                    pictureBox5.Image = Properties.Resources.ICON_SMALL_DISCORD_WHITE;
                    break;

                case "DarkMemories":
                    comboBox1.SelectedIndex = 2;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel1.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    label6.ForeColor = Color.White;
                    pictureBox5.Image = Properties.Resources.ICON_SMALL_DISCORD_WHITE;
                    break;

                case "SaintsInaRow":
                    comboBox1.SelectedIndex = 3;
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel1.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    label6.ForeColor = Color.White;
                    pictureBox5.Image = Properties.Resources.ICON_SMALL_DISCORD_WHITE;
                    break;
            }


            switch (Globals.Program.SelectedQueueNotifySound)
            {
                default:
                    comboBox3.SelectedIndex = 0;
                    break;

                case "ES_Gong":
                    comboBox3.SelectedIndex = 1;
                    break;

                case "ES_Xylophone":
                    comboBox3.SelectedIndex = 2;
                    break;

                case "ES_Applause":
                    comboBox3.SelectedIndex = 3;
                    break;

                case "ES_Nice":
                    comboBox3.SelectedIndex = 4;
                    break;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (HasAnythingChanged == true)
            {
                var gameProcess = Globals.GetIsGameRunning();
                if (gameProcess.Item1 == true)
                    gameProcess.Item2.Kill();

                Application.Restart();
            }
            else this.Close();
        }
        private async void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;

            await Task.Run(() =>
            {
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                    WndProc(ref mouse);
                }));
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != Globals.Program.SelectedThemeIndex)
            {
                HasAnythingChanged = true;
                switch (comboBox1.SelectedIndex)
                {
                    default:
                        WinReg.SetValue("SelectedTheme", "Default");
                        break;

                    case 1:
                        WinReg.SetValue("SelectedTheme", "Legacy");
                        break;

                    case 2:
                        WinReg.SetValue("SelectedTheme", "DarkMemories");
                        break;

                    case 3:
                        WinReg.SetValue("SelectedTheme", "SaintsInaRow");
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FiddlerCore.DestroyRootCertificates() == true)
                Messaging.ShowMessage("Fiddler Certificates Successfully Removed From Your PC!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Messaging.ShowMessage("Something Went Wrong When Cursed Market Tried To Delete Fiddler Certificates...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = Messaging.ShowDialog("This Action Will Delete Every Single Cursed Market Setting, Including Certificate.\n\nAre You Sure You Want To Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (FiddlerCore.GetIsRunning() == true)
                    FiddlerCore.Stop();

                if (WinReg.DestroyCurrentUserSubKeyTree(WinReg.RegistryPath))
                {
                    if (File.Exists(FiddlerCore.rootCertificatePath))
                        File.Delete(FiddlerCore.rootCertificatePath);
                }

                Application.Restart();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WinReg.DisableProxy();

            SoundPlayer sPlayer = new SoundPlayer(Properties.Resources.ES_Gong);
            sPlayer.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Globals_Session.userId   ?? "NONE";
            textBox2.Text = Globals_Session.UID      ?? "NONE";
        }

        private void ObtainLimitedPlatforms()
        {
            comboBox2.Items.Clear();

            foreach (Globals_Session.E_GamePlatform limitedPlatform in Globals_Session.LimitedPlatforms)
                comboBox2.Items.Add(limitedPlatform);

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                List<Globals_Session.E_GamePlatform> platforms = new List<Globals_Session.E_GamePlatform>(Globals_Session.LimitedPlatforms);
                platforms.RemoveAt(comboBox2.SelectedIndex);

                Globals_Session.LimitedPlatforms = platforms.ToArray();
                ObtainLimitedPlatforms();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                default:
                    WinReg.SetValue("SelectedQueueNotifySound", "None");
                    Globals.Program.SelectedQueueNotifySound = "None";
                    break;

                case 1:
                    WinReg.SetValue("SelectedQueueNotifySound", "ES_Gong");
                    Globals.Program.SelectedQueueNotifySound = "ES_Gong";
                    break;

                case 2:
                    WinReg.SetValue("SelectedQueueNotifySound", "ES_Xylophone");
                    Globals.Program.SelectedQueueNotifySound = "ES_Xylophone";
                    break;

                case 3:
                    WinReg.SetValue("SelectedQueueNotifySound", "ES_Applause");
                    Globals.Program.SelectedQueueNotifySound = "ES_Applause";
                    break;

                case 4:
                    WinReg.SetValue("SelectedQueueNotifySound", "ES_Nice");
                    Globals.Program.SelectedQueueNotifySound = "ES_Nice";
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e) => Globals.PlayQueueNotifySound(Globals.Program.SelectedQueueNotifySound);

        private void pictureBox5_Click(object sender, EventArgs e) => Process.Start(Globals_Session.Creator.Discord);
    }
}
