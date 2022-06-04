//    _____ _    _ _____   _____ ______ _____       __  __          _____  _  ________ _______ 
//   / ____| |  | |  __ \ / ____|  ____|  __ \     |  \/  |   /\   |  __ \| |/ /  ____|__   __|
//  | |    | |  | | |__) | (___ | |__  | |  | |    | \  / |  /  \  | |__) | ' /| |__     | |   
//  | |    | |  | |  _  / \___ \|  __| | |  | |    | |\/| | / /\ \ |  _  /|  < |  __|    | |   
//  | |____| |__| | | \ \ ____) | |____| |__| |    | |  | |/ ____ \| | \ \| . \| |____   | |   
//   \_____|\____/|_|  \_\_____/|______|_____/     |_|  |_/_/    \_\_|  \_\_|\_\______|  |_|   
//
//                             ██████╗  ██████╗ ██████╗ ██████╗ 
//                             ╚════██╗██╔═████╗╚════██╗╚════██╗
//                              █████╔╝██║██╔██║ █████╔╝ █████╔╝
//                             ██╔═══╝ ████╔╝██║██╔═══╝ ██╔═══╝ 
//                             ███████╗╚██████╔╝███████╗███████╗
//                             ╚══════╝ ╚═════╝ ╚══════╝╚══════╝
// 


using CranchyLib.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market_Reborn
{
    public partial class Form1 : Form
    {
        NotifyIcon trayIcon = new NotifyIcon();
        static bool IsProgramInitialized = false;
        static bool IsMarketFileInitialized = false;
        static bool IsSeasonManagerInitialized = false;


        public Form1()
        {
            InitializeComponent();
            InitializeSettings();
            Globals.EnsureSelfDataFolderExists();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            Globals_Cache._MAIN = this;

            await Task.Run(() =>
            {
                PerformVersionCheck();
            });

            Globals_Cache._MAIN.Invoke(new Action(() =>
            {
                button3.Text = "START";
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
                checkBox7.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                textBox1.Visible = true;
                button5.Visible = true;

                if (IsMarketFileInitialized == true)
                    checkBox5.Enabled = true;

                button6.Visible = true;
                comboBox1.Visible = true;

                IsProgramInitialized = true;
            }));

        }
        protected private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { FiddlerCore.Stop(); }
            catch { WinReg.DisableProxy(); }
        }
        private void Event_TrayMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                trayIcon.Visible = false;
            }
        }
        private void InitializeSettings()
        {
            label11.Text = $"{Globals.OfflineVersion[0]}.{Globals.OfflineVersion[1]}.{Globals.OfflineVersion[2]}.{Globals.OfflineVersion[3]}";

            comboBox2.SelectedItem = Globals.Crosshair.SelectedCrosshair;
            trackBar1.Value = Globals.Crosshair.Opacity;
            label9.Text = Convert.ToString(trackBar1.Value * 10) + "%";

            try
            {
                switch (Globals.Program.SelectedTheme)
                {
                    default:
                        Globals.Program.SelectedThemeIndex = 0;
                        pictureBox1.Image = Properties.Resources.IMG_LOGO_BIG_BLACK;
                        pictureBox2.Image = Properties.Resources.ICON_SMALL_SETTINGS_BLACK;
                        this.BackColor = Color.White;
                        panel1.BackColor = SystemColors.Control;
                        label1.ForeColor = Color.Black;
                        label2.ForeColor = Color.Black;
                        label3.ForeColor = Color.Black;
                        label4.ForeColor = Color.Black;
                        label5.ForeColor = Color.Black;
                        label6.ForeColor = Color.Black;
                        label7.ForeColor = Color.Black;
                        label8.ForeColor = Color.Gainsboro;
                        label9.ForeColor = Color.Black;
                        label10.ForeColor = Color.Black;
                        label11.ForeColor = Color.Gainsboro;
                        checkBox1.ForeColor = Color.Black;
                        checkBox2.ForeColor = Color.Black;
                        checkBox3.ForeColor = Color.Black;
                        checkBox4.ForeColor = Color.Black;
                        checkBox5.ForeColor = Color.Black;
                        checkBox6.ForeColor = Color.Black;
                        checkBox7.ForeColor = Color.Black;
                        button3.BackColor = Color.Black;
                        button3.ForeColor = Color.White;
                        button5.BackColor = Color.DimGray;
                        button6.BackColor = Color.DimGray;
                        button7.BackColor = Color.DarkGray;
                        button8.BackColor = Color.DimGray;
                        break;

                    case "Legacy":
                        Globals.Program.SelectedThemeIndex = 1;
                        pictureBox5.Visible = false;
                        pictureBox1.Image = Properties.Resources.IMG_LOGO_BIG_WHITE;
                        pictureBox2.Image = Properties.Resources.ICON_SMALL_SETTINGS_WHITE;
                        this.BackColor = Color.FromArgb(255, 46, 51, 73);
                        panel1.BackColor = Color.FromArgb(255, 24, 30, 54);
                        label1.ForeColor = Color.White;
                        label2.ForeColor = Color.White;
                        label3.ForeColor = Color.White;
                        label4.ForeColor = Color.White;
                        label5.ForeColor = Color.White;
                        label6.ForeColor = Color.White;
                        label7.ForeColor = Color.White;
                        label8.ForeColor = Color.DimGray;
                        label9.ForeColor = Color.White;
                        label10.ForeColor = Color.White;
                        label11.ForeColor = Color.DimGray;
                        checkBox1.ForeColor = Color.White;
                        checkBox2.ForeColor = Color.White;
                        checkBox3.ForeColor = Color.White;
                        checkBox4.ForeColor = Color.White;
                        checkBox5.ForeColor = Color.White;
                        checkBox6.ForeColor = Color.White;
                        checkBox7.ForeColor = Color.White;
                        button3.BackColor = Color.IndianRed;
                        button3.ForeColor = Color.White;
                        button5.BackColor = Color.RoyalBlue;
                        button6.BackColor = Color.RoyalBlue;
                        button7.BackColor = Color.SlateBlue;
                        button8.BackColor = Color.RoyalBlue;
                        break;

                    case "DarkMemories":
                        Globals.Program.SelectedThemeIndex = 2;
                        pictureBox5.Visible = false;
                        pictureBox1.Image = Properties.Resources.IMG_LOGO_BIG_WHITE;
                        pictureBox2.Image = Properties.Resources.ICON_SMALL_SETTINGS_WHITE;
                        this.BackColor = Color.FromArgb(255, 44, 47, 51);
                        panel1.BackColor = Color.FromArgb(255, 35, 39, 42);
                        label1.ForeColor = Color.White;
                        label2.ForeColor = Color.White;
                        label3.ForeColor = Color.White;
                        label4.ForeColor = Color.White;
                        label5.ForeColor = Color.White;
                        label6.ForeColor = Color.White;
                        label7.ForeColor = Color.White;
                        label8.ForeColor = Color.DimGray;
                        label9.ForeColor = Color.White;
                        label10.ForeColor = Color.White;
                        label11.ForeColor = Color.DimGray;
                        checkBox1.ForeColor = Color.White;
                        checkBox2.ForeColor = Color.White;
                        checkBox3.ForeColor = Color.White;
                        checkBox4.ForeColor = Color.White;
                        checkBox5.ForeColor = Color.White;
                        checkBox6.ForeColor = Color.White;
                        checkBox7.ForeColor = Color.White;
                        button3.BackColor = Color.FromArgb(255, 65, 65, 65);
                        button3.ForeColor = Color.White;
                        button5.BackColor = Color.FromArgb(255, 85, 85, 85);
                        button6.BackColor = Color.FromArgb(255, 85, 85, 85);
                        button7.BackColor = Color.SlateBlue;
                        button8.BackColor = Color.FromArgb(255, 85, 85, 85);
                        break;

                    case "SaintsInaRow":
                        Globals.Program.SelectedThemeIndex = 3;
                        pictureBox5.Visible = false;
                        pictureBox1.Image = Properties.Resources.IMG_LOGO_BIG_WHITE;
                        pictureBox2.Image = Properties.Resources.ICON_SMALL_SETTINGS_WHITE;
                        this.BackColor = Color.FromArgb(255, 37, 13, 57);
                        panel1.BackColor = Color.FromArgb(255, 55, 20, 86);
                        label1.ForeColor = Color.White;
                        label2.ForeColor = Color.White;
                        label3.ForeColor = Color.White;
                        label4.ForeColor = Color.White;
                        label5.ForeColor = Color.White;
                        label6.ForeColor = Color.White;
                        label7.ForeColor = Color.White;
                        label8.ForeColor = Color.DimGray;
                        label9.ForeColor = Color.White;
                        label10.ForeColor = Color.White;
                        label11.ForeColor = Color.DimGray;
                        checkBox1.ForeColor = Color.White;
                        checkBox2.ForeColor = Color.White;
                        checkBox3.ForeColor = Color.White;
                        checkBox4.ForeColor = Color.White;
                        checkBox5.ForeColor = Color.White;
                        checkBox6.ForeColor = Color.White;
                        checkBox7.ForeColor = Color.White;
                        button3.BackColor = Color.FromArgb(255, 89, 67, 218);
                        button3.ForeColor = Color.White;
                        button5.BackColor = Color.FromArgb(255, 118, 93, 222);
                        button6.BackColor = Color.FromArgb(255, 118, 93, 222);
                        button7.BackColor = Color.SlateBlue;
                        button8.BackColor = Color.FromArgb(255, 118, 93, 222);
                        break;
                }
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e) => this.Close();
        private void button2_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
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
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            trayIcon.Icon = Properties.Resources.icon_tray;
            trayIcon.MouseClick += new MouseEventHandler(Event_TrayMouseClick);
            trayIcon.Visible = true;

            Messaging.ShowNotify("Program Has Been Minimized To Tray...", "Cursed Market Still Working!\n\nLMB To The Tray Button To Show Up Program.", Properties.Resources.icon_tray);
        }


        private void PerformVersionCheck()
        {
            Networking.CreateNewWebProxyInstance();
            try
            {
                Networking.Request.Get($"https://dbd.cranchpalace.info/market/heartBeat", new string[] { });
            }
            catch (NullReferenceException e)
            {
                Messaging.ShowMessage("Cursed Market Failder To Create GET Request!\nSomething on Your PC Prevents Cursed Market Ethernet Connection, However, Sometimes It Can Be Bypassed Using VPN (This isn't Real Solution)");
                goto VersionCheckFail;
            }


            string[] headers =
            {
                "User-Agent: Cursed Market 2022"
            };
            var request = Networking.Request.Get($"https://dbd.cranchpalace.info/market/versionCheck?version={Globals.OfflineVersion}", headers);
            if (request.Item1 != Networking.E_StatusCode.OK)
            {
                if (Messaging.ShowDialog("Cursed Market Failed To Check Client Version!\nThis Issue Can Be Caused by Running on Your PC Proxy, Disable It? (If Running)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FiddlerCore.Stop();
                    Application.Restart();
                }

                goto VersionCheckFail;
            }

            if (request.Item3.IsJson() == false)
            {
                Messaging.ShowMessage("Failed To Check Client Version!\nReason: Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject json = JObject.Parse(request.Item3);
            if ((bool)json["isLatest"] == false)
            {
                if (Messaging.ShowDialog($"New Version Of Cursed Market is Available! Download It?\nCurrent Version: {Globals.OfflineVersion}\nLatest Version: {json["onlineVersion"]}\n\nSave To: {Networking.Utilities.Windows.SE_WinFolder.Downloads}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var download = Networking.Request.Download((string)json["Download"], Networking.Utilities.Windows.SE_WinFolder.Downloads);
                    if (download.Item1 == false)
                    {
                        if (Messaging.ShowDialog("Failed To Download Latest Cursed Market Version!\nShall We Try To Use Legacy Download Method?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start((string)json["Download"]);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        Process.Start(download.Item2);
                        Application.Exit();
                    }
                }
            }

            if (json.ContainsKey("Discord"))
                Globals_Session.Creator.Discord = (string)json["Discord"];

            if ((bool)json["inventories"]["enabled"] == true)
                ObtainMarketFile();

            if ((bool)json["fullProfile"]["enabled"] == true)
                ObtainFullProfile();



            if ((bool)json["catalog"]["enabled"] == true)
                ObtainCatalog();
            else
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label2.Text += "Disabled";
                }));


            if ((bool)json["seasonManager"]["enabled"] == true)
                ObtainSeasonManager();
            else
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label3.Text += "Disabled";
                }));


            if ((bool)json["killSwitch"]["enabled"] == true)
                ObtainKillSwitch();
            else
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label10.Text += "Disabled";
                }));

            IsProgramInitialized = true;
            return;


        VersionCheckFail:
            ObtainMarketFile(false, true);
            ObtainFullProfile(true);
            Globals_Cache._MAIN.Invoke(new Action(() =>
            {
                label2.Text += "Disabled";
                label3.Text += "Disabled";
                comboBox3.SelectedIndex = 0;
                label10.Text += "Disabled";
            }));


        }
        private void ObtainMarketFile(bool DLCOnly = false, bool IsOffline = false)
        {
            if (File.Exists("market_Local.json"))
            {
                if (Messaging.ShowDialog("Cursed Market Detected \"market_Local.json\", Shall We Use It For Skins Unlockment?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Globals_Session.market = File.ReadAllText("market_Local.json");
                    }
                    catch (Exception e)
                    {
                        Messaging.ShowMessage($"Failed To Initialize Locally Stored Market File!\nReason: {e.Message}");
                    }
                }
            }

            if (IsOffline == false)
            {
                switch (DLCOnly)
                {
                    case true:
                        var request_dlc = Networking.Request.Get("https://dbd.cranchpalace.info/market/market_dlc.json", new string[] { });
                        if (request_dlc.Item1 == Networking.E_StatusCode.OK)
                            Globals_Session.market = request_dlc.Item3;

                        else Messaging.ShowMessage("Failed To Obtain Market File!");
                        break;

                    case false:
                        var request_base = Networking.Request.Get("https://dbd.cranchpalace.info/market/market.json", new string[] { });
                        if (request_base.Item1 == Networking.E_StatusCode.OK)
                            Globals_Session.market = request_base.Item3;

                        else Messaging.ShowMessage("Failed To Obtain Market File!");
                        break;
                }
            }


            IsMarketFileInitialized = true;
        }

        private void ObtainFullProfile(bool IsOffline = false)
        {
            if (File.Exists("fullProfile_Local.json"))
            {
                if (Messaging.ShowDialog("Cursed Market Detected \"fullProfile_Local.json\", Shall We Use It For Temp. Progression?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Globals_Session.fullProfile = File.ReadAllText("fullProfile_Local.json");
                    }
                    catch (Exception e)
                    {
                        Messaging.ShowMessage($"Failed To Initialize Locally Stored fullProfile File!\nReason: {e.Message}");
                    }
                }
            }

            if (IsOffline == false)
            {
                var request = Networking.Request.Get("https://dbd.cranchpalace.info/market/fullProfile.json", new string[] { });
                if (request.Item1 == Networking.E_StatusCode.OK)
                    Globals_Session.fullProfile = request.Item3;
                else Messaging.ShowMessage("Failed To Obtain fullProfile File!");
            }
        }

        private void ObtainCatalog()
        {
            var request = Networking.Request.Get("https://dbd.cranchpalace.info/market/advancedSkinsControl.json", new string[] { });
            if (request.Item1 == Networking.E_StatusCode.OK)
            {
                Globals_Session.Advanced.catalog = request.Item3;
                Globals.FiddlerCoreTunables.IsCatalogEnabled = true;

                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label2.Text += "Enabled";
                }));
            }

            else
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label2.Text += "Disabled";
                }));


            comboBox1.Items.Add("Running Catalog");
        }

        private void ObtainSeasonManager()
        {
            var request = Networking.Request.Get("https://dbd.cranchpalace.info/market/seasonManager?check", new string[] { });
            if (request.Item1 == Networking.E_StatusCode.OK)
            {
                Globals.FiddlerCoreTunables.IsSeasonManagerEnabled = true;
                JArray json = JArray.Parse(request.Item3);

                foreach (JValue obj in json)
                {
                    Globals_Cache._MAIN.Invoke(new Action(() =>
                    {
                        comboBox3.Items.Add(obj);
                    }));
                }

                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label3.Text += "Enabled";
                    comboBox3.SelectedIndex = 0;
                    IsSeasonManagerInitialized = true;
                }));
            }
            else
                label3.Text += "Disabled";


            comboBox1.Items.Add("Running SpecialEvents");
        }

        private void ObtainKillSwitch()
        {
            var request = Networking.Request.Get("https://dbd.cranchpalace.info/market/killSwitch.json", new string[] { });
            if (request.Item1 == Networking.E_StatusCode.OK)
            {
                Globals.FiddlerCoreTunables.IsKillSwitchEnabled = true;
                Globals_Session.Advanced.killSwitch = request.Item3;

                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label10.Text += "Enabled";
                }));
            }
            else
                label10.Text += "Disabled";


            comboBox1.Items.Add("Running KillSwitch");
        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Globals_Cache._SETTINGS.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsProgramInitialized == false)
                return;

            FiddlerCore.Start();
            button3.Visible = false;
            textBox5.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                Globals.FiddlerCoreTunables.IsTutorialSkipEnabled = true;
            else
                Globals.FiddlerCoreTunables.IsTutorialSkipEnabled = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                Globals.FiddlerCoreTunables.IsFullProfileEnabled = true;
            else
            {
                var gameRunning = Globals.GetIsGameRunning();
                if ((gameRunning.Item1 == true) && (Globals_Session.bhvrSession != null))
                {
                    DialogResult result = Messaging.ShowDialog("State of this Feature Can't Be Changed While Game Is Running!\nShall We Close Dead By Daylight?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        gameRunning.Item2.Kill();

                    checkBox2.Checked = true;
                }
                else
                    Globals.FiddlerCoreTunables.IsFullProfileEnabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                Globals_Cache._OVERLAY.Show();
            }
            else
            {
                Globals_Cache._OVERLAY.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (Globals_Session.bhvrSession != null)
                {
                    string[] headers =
                    {
                        $"Cookie: bhvrSession={Globals_Session.bhvrSession}",
                        $"User-Agent: {Globals_Session.Game.user_agent}",
                        $"x-kraken-client-platform: {Globals_Session.Game.client_platform}",
                        $"x-kraken-client-provider: {Globals_Session.Game.client_provider}",
                        $"x-kraken-client-os: {Globals_Session.Game.client_os}",
                        $"x-kraken-client-version: {Globals_Session.Game.client_version}",
                        "Content-Type: application/json"
                    };
                    Networking.Request.Post($"https://{Globals_Session.GetSpecificPlatformHostNames(Globals_Session.platformRunning)[0]}/api/v1/players/friends/add", headers, "{\"ids\":[\"" + textBox1.Text + "\"],\"platform\":\"kraken\"}");
                }
                else Messaging.ShowMessage("It's Required To Initialize Cursed Market & Obtain bhvrSession before Sending Friends Request!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else Messaging.ShowMessage("It's Required To Specify CloudID of The Person You Want To add In Case to Proceed!\nCloudID Can Be Found at Very Bottom Of The Game Settings...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Globals.FiddlerCoreTunables.IsCurrencySpoofEnabled = checkBox4.Checked;
            label7.Visible = checkBox4.Checked;
            textBox2.Visible = checkBox4.Checked;
            textBox3.Visible = checkBox4.Checked;
            textBox4.Visible = checkBox4.Checked;
            pictureBox6.Visible = checkBox4.Checked;
            pictureBox7.Visible = checkBox4.Checked;
            pictureBox8.Visible = checkBox4.Checked;
        }

        private static bool isKeypressDigit(KeyPressEventArgs e, string currentstring)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                return false;
            else if (currentstring != "")
            {
                if (currentstring[0] == '0')
                {
                    if (e.KeyChar >= 49 && e.KeyChar <= 57 && e.KeyChar != 8)
                        return false;
                }
            }
            return true;
        }
        private void textBox2_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.Currency_BloodPoints = textBox2.Text;
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.Currency_BloodPoints))
                e.Handled = true;
        }
        private void textBox3_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.Currency_IridescentShards = textBox3.Text;
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.Currency_IridescentShards))
                e.Handled = true;
        }
        private void textBox4_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.Currency_AuricCells = textBox4.Text;
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.Currency_AuricCells))
                e.Handled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            ObtainMarketFile(checkBox5.Checked);

            if (FiddlerCore.GetIsRunning() == true)
            {
                var gameRunning = Globals.GetIsGameRunning();
                if (gameRunning.Item1 == true)
                {
                    DialogResult result = Messaging.ShowDialog("Changes We're Made When Game Is Already Running! It's Required to Restart It To See Changes.\nShall We Close Dead By Daylight?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        gameRunning.Item2.Kill();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                default:
                    return;

                case "Running Market File":
                    using (SaveFileDialog saveDiag = new SaveFileDialog())
                    {
                        saveDiag.InitialDirectory = Environment.CurrentDirectory;
                        saveDiag.FilterIndex = 1; saveDiag.RestoreDirectory = true;
                        saveDiag.FileName = "MarketFile.json";
                        if (saveDiag.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(saveDiag.FileName, Globals_Session.market);
                        }
                    }
                    break;

                case "Running FullProfile":
                    using (SaveFileDialog saveDiag = new SaveFileDialog())
                    {
                        saveDiag.InitialDirectory = Environment.CurrentDirectory;
                        saveDiag.FilterIndex = 1; saveDiag.RestoreDirectory = true;
                        saveDiag.FileName = "FullProfile.json";
                        if (saveDiag.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(saveDiag.FileName, Globals_Session.fullProfile);
                        }
                    }
                    break;

                case "Running Catalog":
                    using (SaveFileDialog saveDiag = new SaveFileDialog())
                    {
                        saveDiag.InitialDirectory = Environment.CurrentDirectory;
                        saveDiag.FilterIndex = 1; saveDiag.RestoreDirectory = true;
                        saveDiag.FileName = "Catalog.json";
                        if (saveDiag.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(saveDiag.FileName, Globals_Session.Advanced.catalog);
                        }
                    }
                    break;

                case "Running SpecialEvents":
                    using (SaveFileDialog saveDiag = new SaveFileDialog())
                    {
                        saveDiag.InitialDirectory = Environment.CurrentDirectory;
                        saveDiag.FilterIndex = 1; saveDiag.RestoreDirectory = true;
                        saveDiag.FileName = "SpecialEvents.json";
                        if (saveDiag.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(saveDiag.FileName, Globals_Session.Advanced.specialEvents);
                        }
                    }
                    break;

                case "Running KillSwitch":
                    using (SaveFileDialog saveDiag = new SaveFileDialog())
                    {
                        saveDiag.InitialDirectory = Environment.CurrentDirectory;
                        saveDiag.FilterIndex = 1; saveDiag.RestoreDirectory = true;
                        saveDiag.FileName = "ItemKillSwitch.json";
                        if (saveDiag.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(saveDiag.FileName, Globals_Session.Advanced.specialEvents);
                        }
                    }
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e) => Clipboard.SetText(textBox5.Text);

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Globals.FiddlerCoreTunables.IsFreeBloodWebEnabled = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                if (Globals.Crosshair.SelectedCrosshair == "NONE")
                    comboBox2.SelectedIndex = 0;
                Globals_Cache._CROSSHAIR.Show();
                comboBox2.Visible = true;
                trackBar1.Visible = true;
                label9.Visible = true;
            }
            else
            {
                Globals_Cache._CROSSHAIR.Hide();
                comboBox2.Visible = false;
                trackBar1.Visible = false;
                label9.Visible = false;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsProgramInitialized == true)
            {
                if (WinReg.SetValue("SelectedCrosshair", comboBox2.SelectedItem.ToString()))
                    Globals.Crosshair.SelectedCrosshair = comboBox2.SelectedItem.ToString();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (IsProgramInitialized == true)
            {
                if (WinReg.SetValue_INT32("CrosshairOpacity", trackBar1.Value))
                {
                    Globals.Crosshair.Opacity = trackBar1.Value;
                    label9.Text = Convert.ToString(trackBar1.Value * 10) + "%";
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsSeasonManagerInitialized == true)
            {
                if (comboBox3.SelectedIndex == 0)
                    Globals.FiddlerCoreTunables.IsSeasonManagerEnabled = false;

                else
                {
                    var request = Networking.Request.Get($"https://dbd.cranchpalace.info/market/seasonManager?specifiedSeason={comboBox3.SelectedIndex - 1}", new string[] { });
                    if (request.Item1 == Networking.E_StatusCode.OK)
                    {
                        Globals_Session.Advanced.specialEvents = request.Item3;
                        Globals.FiddlerCoreTunables.IsSeasonManagerEnabled = true;
                    }
                    else
                        Globals.FiddlerCoreTunables.IsSeasonManagerEnabled = false;
                }
            }
        }

        public void UpdateBhvrSession() => Globals_Cache._MAIN.Invoke(new Action(() => { textBox5.Text = Globals_Session.bhvrSession; button8.Visible = true; }));

        public async void UpdateQueueStatus(bool wasMatchFound, int queuePosition = 0)
        {
            if (wasMatchFound == true)
            {
                Globals_Cache._MAIN.Invoke(new Action(() =>
                {
                    label5.Text = "QUEUE POSITION: MATCH FOUND";
                    Globals.PlayQueueNotifySound(Globals.Program.SelectedQueueNotifySound);
                }));
            }
            else
            {
                await Task.Run(() =>
                {
                    if (queuePosition == -1)
                    {
                        Thread.Sleep(8000);
                        Globals_Cache._MAIN.Invoke(new Action(() =>
                        {
                            label5.Text = "QUEUE POSITION: NONE";
                        }));
                    }
                    else if (queuePosition == -2)
                        Globals_Cache._MAIN.Invoke(new Action(() =>
                        {
                            label5.Text = "QUEUE POSITION: NONE";
                        }));
                    else
                        Globals_Cache._MAIN.Invoke(new Action(() =>
                        {
                            label5.Text = $"QUEUE POSITION: {queuePosition}";
                        }));

                });
            }
        }
    }
}
