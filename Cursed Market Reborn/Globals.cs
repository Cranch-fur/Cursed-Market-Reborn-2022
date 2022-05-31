using CranchyLib.SaveFile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Cursed_Market_Reborn
{
    public static class Globals
    {
        ///////////////////////////////// => High Priority Variables
        public static readonly string SelfExecutableName = AppDomain.CurrentDomain.FriendlyName;
        public const string OfflineVersion = "4002";
        public static DateTime NETDateTime { get; private set; }


        public static class Program
        {
            public static string SelectedTheme = WinReg.GetValue("SelectedTheme") ?? "DarkMemories";
            public static int SelectedThemeIndex = 0;
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
            json["data"]["playerId"] = Globals_Session.playerId;
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

        public static bool IsGameRunning()
        {
            Process[] pList = Process.GetProcesses();
            if (pList.Length > 0)
            {
                foreach (Process p in pList)
                {
                    if (p.ProcessName.Contains("DeadByDaylight-"))
                        return true;
                }
            }

            return false;
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

        //public static string FIDDLERCORE_VALUETRANSFER_QUEUEPOSITION(string input)
        //{
        //    if (FIDDLERCORE_VALUE_QUEUEPOSITION != null)
        //    {
        //        try
        //        {
        //            var JsQueueResponse = JObject.Parse(input);
        //            if ((string)JsQueueResponse["status"] == "QUEUED")
        //                return (string)JsQueueResponse["queueData"]["position"];
        //            else if ((string)JsQueueResponse["status"] == "MATCHED")
        //            {
        //                FIDDLERCORE_VALUE_CURRENTMATCHID = (string)JsQueueResponse["matchData"]["matchId"];
        //                Overlay.IsMMRObtained = false;
        //                return "MATCHED";
        //            }
        //            else
        //                return "NONE";
        //        }
        //        catch { return "NONE"; }
        //    }
        //    else return "NONE";
        //}
    }
}