using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public partial class Form2 : Form
    {
        static bool HasAnythingChanged = false;
        static bool IsSettingsInitialized = false;
        public Form2()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            IsSettingsInitialized = true;
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
                    break;

                case "Legacy":
                    comboBox1.SelectedIndex = 1;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel1.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    break;

                case "DarkMemories":
                    comboBox1.SelectedIndex = 2;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel1.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    break;

                case "SaintsInaRow":
                    comboBox1.SelectedIndex = 3;
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel1.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    break;
            }
        }

        ///////////////////////////////// => WinForms Windows Basics UI
        private void button1_Click(object sender, EventArgs e)
        {
            if (HasAnythingChanged == true)
                Application.Restart();
            else this.Close();
        }
        private async void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false; await Task.Run(() =>
            {
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref mouse);
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsSettingsInitialized == true)
            {
                if (Globals.Program.SelectedThemeIndex != comboBox1.SelectedIndex)
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        default:
                            WinReg.SetValue("SelectedTheme", "Default");
                            break;

                        case 1:
                            WinReg.SetValue("SelectedTheme", "Legacy");
                            HasAnythingChanged = true;
                            break;

                        case 2:
                            WinReg.SetValue("SelectedTheme", "DarkMemories");
                            HasAnythingChanged = true;
                            break;

                        case 3:
                            WinReg.SetValue("SelectedTheme", "SaintsInaRow");
                            HasAnythingChanged = true;
                            break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FiddlerCore.RemoveRootCertificate() == true)
                Messaging.ShowMessage("Fiddler Certificates Successfully Removed From Your PC!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Messaging.ShowMessage("Something Went Wrong When Cursed Market Tried To Delete Fiddler Certificates...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //DialogResult settingsresetdialogue = MessageBox.Show("Are you sure that you want to delete all Cursed Market settings from registry?", Globals.PROGRAM_EXECUTABLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //if (settingsresetdialogue == DialogResult.Yes)
            //{
            //    WinReg.CurrentUser.DeleteSubKeyTree(Globals.REGISTRY_MAIN.Replace("HKEY_CURRENT_USER\\", ""));
            //    Application.Restart();
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WinReg.DisableProxy();
            Application.Restart();
        }
    }
}
