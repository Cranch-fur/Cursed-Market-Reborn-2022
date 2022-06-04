using CranchyLib.Networking;
using CranchyLib.SaveFile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;

namespace Cursed_Market_Reborn
{
    public static class Globals
    {
        ///////////////////////////////// => High Priority Variables
        public static readonly string SelfExecutableName = AppDomain.CurrentDomain.FriendlyName;
        public static readonly string SelfExecutableFriendlyName = SelfExecutableName.Remove(Globals.SelfExecutableName.Length - 4, 4);
        public static readonly string SelfDataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Cursed Market 2022";
        public static void EnsureSelfDataFolderExists()
        {
            if (Directory.Exists(SelfDataFolder) == false)
                Directory.CreateDirectory(SelfDataFolder);
        }

        public const string OfflineVersion = "4005";

        public static DateTime NETDateTime { get; private set; }


        public static class Program
        {
            public static string SelectedTheme = WinReg.GetValue("SelectedTheme") ?? "DarkMemories";
            public static int SelectedThemeIndex = 0;

            public static string SelectedQueueNotifySound = WinReg.GetValue("SelectedQueueNotifySound") ?? "None";
        }


        public static class Crosshair
        {
            public static string SelectedCrosshair = WinReg.GetValue("SelectedCrosshair") ?? "Dot (Red)";
            public static int Opacity = WinReg.GetValue_INT32("CrosshairOpacity", 10);
        }


        public static class FiddlerCoreTunables
        {
            public static bool IsCatalogEnabled = false;
            public static bool IsSeasonManagerEnabled = false;
            public static bool IsKillSwitchEnabled = false;
            public static bool IsFullProfileEnabled = false;
            public static bool IsTutorialSkipEnabled = false;
            public static bool IsFreeBloodWebEnabled = false;


            public static bool IsCurrencySpoofEnabled = false;
            public static string Currency_BloodPoints = "0";
            public static string Currency_IridescentShards = "0";
            public static string Currency_AuricCells = "0";

        }

        public static string GetValidMarketFile()
        {
            JToken json = JObject.Parse(Globals_Session.market);
            json["data"]["playerId"] = Globals_Session.userId;
            return json.ToString();
        }

        public static string GetValidFullProfile()
        {
            dynamic json = JsonConvert.DeserializeObject(Globals_Session.fullProfile);
            json["playerUId"] = Globals_Session.UID;
            json["currentSeasonTicks"] = (long)((DateTime.Now.ToUniversalTime() - NETDateTime).TotalMilliseconds + 0.5);
            return SaveFile.EncryptSavefile(JsonConvert.SerializeObject(json, Formatting.None));
        }

        public static string ObtainUIDFromSteamAuthTicket(string token)
        {
            string output = token.Remove(0, token.IndexOf("14", StringComparison.InvariantCulture)).Remove(0, 24);
            output = output.Remove(16, output.Length - 16);
            return output;
        }

        public static string ObtainUserIdFromAuthResponse(string response)
        {
            if (response.IsJson() == false)
                return null;

            JObject json = JObject.Parse(response);
            if (json.ContainsKey("userId") == false)
                return null;

            return (string)json["userId"];
        }

        
        public static Process GetGameProcess()
        {
            Process[] pList = Process.GetProcesses();
            if (pList.Length > 0)
            {
                foreach (Process p in pList)
                {
                    if (p.ProcessName.Contains("DeadByDaylight-"))
                        return p;
                }
            }

            return null;
        }
        public static Tuple<bool, Process> GetIsGameRunning()
        {
            Process gameProcess = GetGameProcess();

            if (gameProcess == null)
                return Tuple.Create(false, gameProcess);

            return Tuple.Create(true, gameProcess);
        }



        public static void UpdateQueuePositionInfoFromServerResponse(string response)
        {
            JObject json = JObject.Parse(response);
            if ((string)json["status"] == "QUEUED")
            {
                Globals_Cache._OVERLAY.UpdateQueueStatus(false, (int)json["queueData"]["position"]);
                Globals_Cache._MAIN.UpdateQueueStatus(false, (int)json["queueData"]["position"]);
            }
            else if ((string)json["status"] == "MATCHED")
            {
                Globals_Cache._OVERLAY.UpdateQueueStatus(true);
                Globals_Cache._OVERLAY.UpdateQueueStatus(false, -1);

                Globals_Cache._MAIN.UpdateQueueStatus(true);
                Globals_Cache._MAIN.UpdateQueueStatus(false, -1);
            }
        }

        public static void UpdateQueuePositionInfo(bool matched, int position)
        {
            Globals_Cache._OVERLAY.UpdateQueueStatus(matched, position);
            Globals_Cache._MAIN.UpdateQueueStatus(matched, position);
        }

        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("[dd.MM.yyyy HH/mm/ss]");
        }

        public static void PlayQueueNotifySound(string name)
        {
            SoundPlayer sPlayer = new SoundPlayer();

            switch (name)
            {
                default:
                    return;

                case "ES_Gong":
                    sPlayer.Stream = Properties.Resources.ES_Gong;
                    break;

                case "ES_Xylophone":
                    sPlayer.Stream = Properties.Resources.ES_Xylophone;
                    break;

                case "ES_Applause":
                    sPlayer.Stream = Properties.Resources.ES_Applause;
                    break;

                case "ES_Nice":
                    sPlayer.Stream = Properties.Resources.ES_Nice;
                    break;
            }
            sPlayer.Play();
        }
    }
}