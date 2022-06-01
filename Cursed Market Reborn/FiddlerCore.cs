using CranchyLib.Networking;
using Fiddler;
using System.IO;
using System.Linq;

namespace Cursed_Market_Reborn
{
    public static class FiddlerCore
    {
        public static string rootCertificatePath = $"{Networking.Utilities.Windows.SE_WinFolder.Appdata_Roaming}\\CursedMarket_RootCertificate.p12";
        public static string rootCertificatePassword = "B99fceaaa1f3b2458ccae74fb734cb76";

        static FiddlerCore()
        {
            FiddlerApplication.BeforeRequest += FiddlerToCatchBeforeRequest;
            FiddlerApplication.AfterSessionComplete += FiddlerToCatchAfterSessionComplete;
            FiddlerApplication.ResetSessionCounter();
        }
        private static bool EnsureRootCertificate()
        {
            BCCertMaker.BCCertMaker certProvider = new BCCertMaker.BCCertMaker();
            CertMaker.oCertProvider = certProvider;

            if (!File.Exists(rootCertificatePath))
            {
                certProvider.CreateRootCertificate();
                certProvider.WriteRootCertificateAndPrivateKeyToPkcs12File(rootCertificatePath, rootCertificatePassword);
            }
            else certProvider.ReadRootCertificateAndPrivateKeyFromPkcs12File(rootCertificatePath, rootCertificatePassword);

            if (!CertMaker.rootCertIsTrusted())
            {
                CertMaker.trustRootCert();
            }

            return true;

        }
        public static bool DestroyRootCertificates()
        {
            CertMaker.removeFiddlerGeneratedCerts(true);
            return true;
        }



        public static bool Start()
        {
            if (EnsureRootCertificate())
                FiddlerApplication.Startup(new FiddlerCoreStartupSettingsBuilder().ListenOnPort(8866).RegisterAsSystemProxy().ChainToUpstreamGateway().DecryptSSL().OptimizeThreadPool().Build());
            return true;
        }
        public static void Stop()
        {
            FiddlerApplication.Shutdown();
        }
        public static bool GetIsRunning() { return FiddlerApplication.IsStarted(); }



        private static void PerformFakeGameRequest(string url)
        {
            string[] headers =
            {
                $"Cookie: bhvrSession={Globals_Session.bhvrSession}",
                $"User-Agent: {Globals_Session.Game.user_agent}",
                $"x-kraken-client-platform: {Globals_Session.Game.client_platform}",
                $"x-kraken-client-provider: {Globals_Session.Game.client_provider}",
                $"x-kraken-client-os: {Globals_Session.Game.client_os}",
                $"x-kraken-client-version: {Globals_Session.Game.client_version}",
                "x-kraken-check: True"
            };
            Networking.Request.Get(url, headers);
        }

        public static void FiddlerToCatchBeforeRequest(Session oSession)
        {
            if (oSession.uriContains("/api/v1/config"))
            {
                if (oSession.oRequest["Cookie"].Length > 0)
                {
                    Globals_Session.bhvrSession = oSession.oRequest["Cookie"].Replace("bhvrSession=", string.Empty);
                    Globals_Cache._MAIN.UpdateBhvrSession();
                }

                if (oSession.oRequest["User-Agent"].Length > 0)
                    Globals_Session.Game.user_agent = oSession.oRequest["User-Agent"];

                if (oSession.oRequest["x-kraken-client-platform"].Length > 0)
                    Globals_Session.Game.client_platform = oSession.oRequest["x-kraken-client-platform"];

                if (oSession.oRequest["x-kraken-client-provider"].Length > 0)
                    Globals_Session.Game.client_provider = oSession.oRequest["x-kraken-client-provider"];

                if (oSession.oRequest["x-kraken-client-os"].Length > 0)
                    Globals_Session.Game.client_os = oSession.oRequest["x-kraken-client-os"];

                if (oSession.oRequest["x-kraken-client-version"].Length > 0)
                    Globals_Session.Game.client_version = oSession.oRequest["x-kraken-client-version"];

                Globals_Session.platformRunning = Globals_Session.GetRunningPlatformFromHostName(oSession.hostname);


                return;
            }

            if (Globals_Session.GetRunningPlatformHostNames().Contains(oSession.hostname))
            {
                if (oSession.oRequest["x-kraken-check"] != "True")
                {
                    if (oSession.uriContains("/api/v1/auth/provider/steam/login?token="))
                    {
                        Globals_Session.UID = Globals.ObtainUIDFromSteamAuthTicket(oSession.url.ToString());


                        return;
                    }


                    if (oSession.uriContains("/api/v1/inventories"))
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        oSession.utilSetResponseBody(Globals.GetValidMarketFile());
                        PerformFakeGameRequest(oSession.fullUrl);


                        return;
                    }


                    if (Globals.FiddlerCoreTunables.IsCatalogEnabled == true)
                    {
                        if (Globals_Session.LimitedPlatforms.Contains(Globals_Session.platformRunning))
                        {
                            Globals.FiddlerCoreTunables.IsCatalogEnabled = false;
                            return;
                        }


                        if (oSession.uriContains("/catalog.json"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody(Globals_Session.Advanced.catalog);
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsSeasonManagerEnabled == true)
                    {
                        if (Globals_Session.LimitedPlatforms.Contains(Globals_Session.platformRunning))
                        {
                            Globals.FiddlerCoreTunables.IsSeasonManagerEnabled = false;
                            return;
                        }

                        if (oSession.uriContains("/specialEventsContent.json"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody(Globals_Session.Advanced.specialEvents);
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsKillSwitchEnabled == true)
                    {
                        if (Globals_Session.LimitedPlatforms.Contains(Globals_Session.platformRunning))
                        {
                            Globals.FiddlerCoreTunables.IsKillSwitchEnabled = false;
                            return;
                        }

                        if (oSession.uriContains("/itemsKillswitch.json"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody(Globals_Session.Advanced.killSwitch);
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsFullProfileEnabled == true)
                    {
                        if (oSession.uriContains("/api/v1/players/me/states/FullProfile/binary"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.oResponse["Content-Type"] = "application/octet-stream";
                            oSession.oResponse["Kraken-State-Version"] = "1";
                            oSession.oResponse["Kraken-State-Schema-Version"] = "0";
                            oSession.utilSetResponseBody(Globals.GetValidFullProfile());
                            PerformFakeGameRequest(oSession.fullUrl);


                            return;
                        }


                        if (oSession.uriContains("/api/v1/players/me/states/binary?schemaVersion"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody("{\"version\":1,\"stateName\":\"FullProfile\",\"schemaVersion\":0,\"playerId\":\"" + Globals_Session.playerId + "\"}");
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsTutorialSkipEnabled == true)
                    {
                        if (oSession.fullUrl.EndsWith("/api/v1/onboarding/get-bot-match-status"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody("{\"survivorMatchPlayed\":true,\"killerMatchPlayed\":true}");
                            return;
                        }

                        if (oSession.fullUrl.EndsWith("/api/v1/onboarding"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody(Properties.Resources.Offline_onboarding);
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsFreeBloodWebEnabled == true)
                    {
                        if (oSession.uriContains("v1/wallet/withdraw"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody("{\"userId\":\"null\",\"balance\":0,\"currency\":\"USCents\"}");
                            return;
                        }
                    }


                    if (Globals.FiddlerCoreTunables.IsCurrencySpoofEnabled == true)
                    {
                        if (oSession.uriContains("api/v1/wallet/currencies"))
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody("{\"list\":[{\"balance\":" + Globals.FiddlerCoreTunables.Currency_IridescentShards + ",\"currency\":\"Shards\"},{\"balance\":" + Globals.FiddlerCoreTunables.Currency_AuricCells + ",\"currency\":\"Cells\"},{\"balance\":" + Globals.FiddlerCoreTunables.Currency_BloodPoints + ",\"currency\":\"BonusBloodpoints\"},{\"balance\":0,\"currency\":\"Bloodpoints\"}]}");

                            return;
                        }
                    }


                    if (oSession.uriContains("api/v1/friends/richPresence"))
                    {
                        Globals_Session.playerId = oSession.url.Remove(0, oSession.url.LastIndexOf("/") + 1);
                        return;
                    }
                }

            }
        }

        public static void FiddlerToCatchAfterSessionComplete(Session oSession)
        {
            if (Globals_Session.GetRunningPlatformHostNames().Contains(oSession.hostname))
            {
                if (oSession.oRequest["x-kraken-check"] != "True")
                {
                    if (oSession.fullUrl.EndsWith("/api/v1/queue"))
                    {
                        if (oSession.uriContains("/cancel"))
                            Globals.UpdateQueuePositionInfo(false, -2);

                        oSession.utilDecodeResponse();
                        string responseBody = oSession.GetResponseBodyAsString();

                        if (responseBody != string.Empty)
                            Globals.UpdateQueuePositionInfoFromServerResponse(oSession.GetResponseBodyAsString());

                        return;
                    }
                }
            }
        }
    }
}
