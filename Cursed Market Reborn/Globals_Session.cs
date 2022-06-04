namespace Cursed_Market_Reborn
{
    public static class Globals_Session
    {
        public enum E_GamePlatform
        {
            None,
            Android,
            DMM,
            IOS = 4,
            Switch = 8,
            PS4 = 16,
            Steam = 32,
            Steam_PrivateServer,
            Steam_PTB = 48,
            WinGDK = 64,
            Xbox = 128,
            Stadia = 512,
            PS5 = 1024,
            XSX = 2048,
            Epic = 4096
        }
        public static E_GamePlatform platformRunning = E_GamePlatform.None;

        public static E_GamePlatform[] LimitedPlatforms =
        {
            E_GamePlatform.Steam_PTB,
            E_GamePlatform.Steam_PrivateServer
        };

        public static string[] GetSpecificPlatformHostNames(E_GamePlatform platform = E_GamePlatform.None)
        {
            switch (platformRunning)
            {
                case E_GamePlatform.Steam:
                    return new string[]
                    {
                        "steam.live.bhvrdbd.com",
                        "cdn.live.dbd.bhvronline.com",
                        "cdn.live.bhvrdbd.com"
                    };

                case E_GamePlatform.Steam_PrivateServer:
                    return new string[]
                    {
                        "dbdserver.archengius.dev"
                    };

                case E_GamePlatform.Steam_PTB:
                    return new string[]
                    {
                        "latest.ptb.bhvrdbd.com",
                        "cdn.ptb.dbd.bhvronline.com"
                    };

                case E_GamePlatform.WinGDK:
                    return new string[]
                    {
                        "grdk.live.bhvrdbd.com",
                        "cdn.live.dbd.bhvronline.com",
                        "cdn.live.bhvrdbd.com"
                    };

                case E_GamePlatform.Epic:
                    return new string[]
                    {
                        "brill.live.bhvrdbd.com",
                        "cdn.live.dbd.bhvronline.com",
                        "cdn.live.bhvrdbd.com"
                    };

                default:
                    return hostNames;
            }
        }

        public static string[] GetRunningPlatformHostNames() {
            return GetSpecificPlatformHostNames(platformRunning);
        }

        public static E_GamePlatform GetRunningPlatformFromHostName(string host)
        {
            switch (host)
            {
                case "steam.live.bhvrdbd.com":
                    return E_GamePlatform.Steam;

                case "dbdserver.archengius.dev":
                    return E_GamePlatform.Steam_PrivateServer;

                case "latest.ptb.bhvrdbd.com":
                    return E_GamePlatform.Steam_PTB;

                case "grdk.live.bhvrdbd.com":
                    return E_GamePlatform.WinGDK;

                case "brill.live.bhvrdbd.com":
                    return E_GamePlatform.Epic;

                default:
                    return E_GamePlatform.None;
            }
        }

        private static string[] hostNames =
        {
            "steam.live.bhvrdbd.com",
            "grdk.live.bhvrdbd.com",
            "brill.live.bhvrdbd.com",


            "cdn.live.dbd.bhvronline.com",
            "cdn.live.bhvrdbd.com",


            "latest.ptb.bhvrdbd.com",
            "cdn.ptb.dbd.bhvronline.com",


            "dbdserver.archengius.dev"
        };



        public static string bhvrSession = null;
        public static string userId = null;
        public static string UID = null;

        public static string market = Properties.Resources.Offline_inventories;
        public static string fullProfile = Properties.Resources.Offline_fullProfile;

        public static class Advanced
        {
            public static string catalog = null;
            public static string specialEvents = null;
            public static string killSwitch = null;
        }

        public static class Game
        {
            public static string user_agent = null;
            public static string client_version = null;
            public static string client_provider = null;
            public static string client_platform = null;
            public static string client_os = null;
        }

        public static class Match
        {
            public static string ID = null;
            public static int QueuePosition = 0;
        }

        public static class Creator
        {
            public static string Discord = null;
        }
    }
}
