using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using static MemoryHelper;

namespace ASRT_RichPresence
{
    class Program
    {
        public DiscordRpcClient client;
        public int Run()
        {
            // The main thread, where the rich presence magic will happen :)
            try
            {
                // Initialise game memory access
                MemoryHelper.Initialise();

                // Create a rich presence client
                client = new DiscordRpcClient("759459364951031821");

                // Connect to the Discord IPC
                client.Initialize();

                // Initial code for online support
                client.RegisterUriScheme(executable: "explorer steam://rungameid/212480");
                client.SetSubscription(EventType.Join);
                client.OnJoin += OnJoin;

                // Defining important variable
                string menuState = "";
                string trackName = "";
                string trackImage = "";
                string racemodeName = "";
                string racemodeImage = "";
                string richStateExtra= "";
                string richDetailsExtra = "";
                bool inMenu = true;
                bool isOnlineMode = false;
                string lobbyID = "";
                int lobbySize = 0;
                DateTime startTimestamp = DateTime.UtcNow;
                int playerRating = 0;
                int gpRaceNumber = 0;

                // Final variables for Discord RichPresence
                string richState = "";
                string richDetails = "";
                string lastRichState = "";
                string lastRichDetails = "";

                while (true)
                {
                    // Determine track name
                    switch (ReadUInt(ReadUInt(0xBC7434)))
                    {
                        case 0xD4257EBD:
                            trackName = "Ocean View";
                            trackImage = "oceanview";
                            gpRaceNumber = 1;
                            break;
                        case 0x32D305A8:
                            trackName = "Samba Studios";
                            trackImage = "sambastudios";
                            gpRaceNumber = 2;
                            break;
                        case 0xC72B3B98:
                            trackName = "Carrier Zone";
                            trackImage = "carrierzone";
                            gpRaceNumber = 3;
                            break;
                        case 0x03EB7FFF:
                            trackName = "Dragon Canyon";
                            trackImage = "dragoncanyon";
                            gpRaceNumber = 4;
                            break;
                        case 0xE3121777:
                            trackName = "Temple Trouble";
                            trackImage = "templetrouble";
                            gpRaceNumber = 1;
                            break;
                        case 0x4E015AB6:
                            trackName = "Galactic Parade";
                            trackImage = "galacticparade";
                            gpRaceNumber = 2;
                            break;
                        case 0x503C1CBC:
                            trackName = "Seasonal Shrines";
                            trackImage = "seasonalshrines";
                            gpRaceNumber = 3;
                            break;
                        case 0x7534B7CA:
                            trackName = "Rogue's Landing";
                            trackImage = "rogueslanding";
                            gpRaceNumber = 4;
                            break;
                        case 0x38A394ED:
                            trackName = "Dream Valley";
                            trackImage = "dreamvalley";
                            gpRaceNumber = 1;
                            break;
                        case 0xC5C9DEA1:
                            trackName = "Chilly Castle";
                            trackImage = "chillycastle";
                            gpRaceNumber = 2;
                            break;
                        case 0xD936550C:
                            trackName = "Graffiti City";
                            trackImage = "graffiticity";
                            gpRaceNumber = 3;
                            break;
                        case 0x4A0FF7AE:
                            trackName = "Sanctuary Falls";
                            trackImage = "sanctuaryfalls";
                            gpRaceNumber = 4;
                            break;
                        case 0xCD8017BA:
                            trackName = "Graveyard Gig";
                            trackImage = "graveyardgig";
                            gpRaceNumber = 1;
                            break;
                        case 0xDC93F18B:
                            trackName = "Adder's Lair";
                            trackImage = "adderslair";
                            gpRaceNumber = 2;
                            break;
                        case 0x2DB91FC2:
                            trackName = "Burning Depths";
                            trackImage = "burningdepths";
                            gpRaceNumber = 3;
                            break;
                        case 0x94610644:
                            trackName = "Race of AGES";
                            trackImage = "raceofages";
                            gpRaceNumber = 4;
                            break;
                        case 0xE6CD97F0:
                            trackName = "Sunshine Tour";
                            trackImage = "sunshinetour";
                            gpRaceNumber = 1;
                            break;
                        case 0xE87FDF22:
                            trackName = "Shibuya Downtown";
                            trackImage = "shibuyadowntown";
                            gpRaceNumber = 2;
                            break;
                        case 0x17463C8D:
                            trackName = "Roulette Road";
                            trackImage = "rouletteroad";
                            gpRaceNumber = 3;
                            break;
                        case 0xFEBC639E:
                            trackName = "Egg Hangar";
                            trackImage = "egghangar";
                            gpRaceNumber = 4;
                            break;
                        case 0x1EF56CE1:
                            trackName = "Outrun Bay";
                            trackImage = "outrunbay";
                            break;
                        case 0xB9B67B8F:
                            trackName = "Neon Docks";
                            trackImage = "neondocks";
                            break;
                        case 0x7583BBD6:
                            trackName = "Battle Bay";
                            trackImage = "battlebay";
                            break;
                        case 0x997E9C42:
                            trackName = "Creepy Courtyard";
                            trackImage = "creepycourtyard";
                            break;
                        case 0x8DABE769:
                            trackName = "Rooftop Rumble";
                            trackImage = "rooftoprumble";
                            break;
                        case 0x38A73138:
                            trackName = "Monkey Ball Park";
                            trackImage = "monkeyballpark";
                            break;
                    }

                    // Determine race mode
                    // Todo: finish uploading the images!
                    richStateExtra = "";
                    richDetailsExtra = "";
                    switch (ReadUInt(ReadUInt(0xBCE914) + 0x38))
                    {
                        case 0x4AFC561D:
                            racemodeName = "Single Race";
                            racemodeImage = "singlerace";
                            richDetailsExtra = GetRaceInfo();
                            break;
                        case 0x447473BC:
                            racemodeName = "Battle Arena";
                            racemodeImage = "battlearena";
                            richDetailsExtra = GetBattleInfo(false);
                            break;
                        case 0xE64B5DD8:
                            racemodeName = "Battle Race";
                            racemodeImage = "battlerace";
                            richDetailsExtra = GetBattleInfo(true);
                            break;
                        case 0xCCB41574:
                            racemodeName = "Capture the Chao";
                            racemodeImage = "capturethechao";
                            richDetailsExtra = GetChaoInfo();
                            break;
                        case 0x3CBA89B4:
                            racemodeName = "Boost Challenge";
                            racemodeImage = "boostchallenge";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0x79E12D5C:
                            racemodeName = "Time Attack";
                            racemodeImage = "timeattack";
                            string pb = GetTimeString(ReadFloat(ReadInt(0xBCE910) + 0x12C));
                            string sb = GetTimeString(ReadFloat(ReadInt(0xBCE910) + 0x130));
                            richDetailsExtra = pb != "" ? "PB: " + pb : "";
                            richStateExtra = sb != "" ? "Session Best: " + sb : "";
                            break;
                        case 0x77E95ADC:
                            racemodeName = "Sprint";
                            racemodeImage = "sprint";
                            break;
                        case 0xBC7C19CF:
                            racemodeName = "Persuit";
                            racemodeImage = "persuit";
                            // todo: extraInfo = Tank: [number]%
                            break;
                        case 0xB06F818B:
                            racemodeName = "Drift Challenge";
                            racemodeImage = "driftchallenge";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0x9FBFB99B:
                            racemodeName = "Traffic Attack";
                            racemodeImage = "trafficattack";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0xEC587062:
                            racemodeName = "Versus";
                            racemodeImage = "versus";
                            // todo: extraInfo = "opponent [number]/[number]"
                            break;
                        case 0x091F29F4:
                            racemodeName = "Grand Prix";
                            racemodeImage = "gprace";
                            string raceInfo = GetRaceInfo();
                            richStateExtra = "Race " + gpRaceNumber + (raceInfo == "" ? "" : ", " + raceInfo);
                            // todo: extraInfo = "Race "
                            break;
                        case 0xAD538D8D:
                            racemodeName = "Ring Race";
                            racemodeImage = "ringrace";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0x61FF5D42:
                            racemodeName = "Boost Race";
                            racemodeImage = "boostrace";
                            richDetailsExtra = GetRaceInfo();
                            break;
                    }

                    // Determine if racing in Mirror mode
                    if (ReadInt(0xBC744C) == 1)
                    {
                        trackName = trackName + " (Mirror)";
                    }

                    // Determine if ingame or in-menu
                    inMenu = ReadUInt(ReadUInt(0xBCE914) + 0x38) == 0;

                    // Determine if in online mode or not
                    isOnlineMode = ReadUShort(ReadUInt(0xEC1A88) + 0x525) != 0;

                    // Determine menu state

                    switch (ReadInt(0xC51AD0))
                    {
                        case 0:
                            switch (ReadInt(0xC56890))
                            {
                                case 0:
                                    // Determine tour
                                    switch (ReadInt(0xC55F1C))
                                    {
                                        case 0:
                                            menuState = "Sunshine Coast";
                                            break;
                                        case 1:
                                            menuState = "Frozen Valley";
                                            break;
                                        case 2:
                                            menuState = "Scorching Skies";
                                            break;
                                        case 3:
                                            menuState = "Twilight Engine";
                                            break;
                                        case 4:
                                            menuState = "Moonlight Park";
                                            break;
                                        case 5:
                                            menuState = "Superstar Showdown";
                                            break;
                                    }
                                    break;
                                case 1:
                                    // Determine Grand Prix
                                    switch (ReadInt(0xC51D44))
                                    {
                                        case 0:
                                            menuState = "Dragon Cup";
                                            break;
                                        case 1:
                                            menuState = "Rogue Cup";
                                            break;
                                        case 2:
                                            menuState = "Emerald Cup";
                                            break;
                                        case 3:
                                            menuState = "Arcade Cup";
                                            break;
                                        case 4:
                                            menuState = "Classic Cup";
                                            break;
                                        case 5:
                                            menuState = "Dragon Cup (Mirror)";
                                            break;
                                        case 6:
                                            menuState = "Rogue Cup (Mirror)";
                                            break;
                                        case 7:
                                            menuState = "Emerald Cup (Mirror)";
                                            break;
                                        case 8:
                                            menuState = "Arcade Cup (Mirror)";
                                            break;
                                        case 9:
                                            menuState = "Classic Cup (Mirror)";
                                            break;
                                    }
                                    break;
                                case 2:
                                    menuState = "Time Attack";
                                    break;
                                case 3:
                                    menuState = "Single Race";
                                    break;
                            }
                            break;
                        case 1:
                            menuState = "Matchmaking";
                            break;
                        case 2:
                            menuState = "Custom Game";
                            break;
                        case 3:
                            menuState = "Options";
                            break;
                    }

                    // Online / offline states
                    if (isOnlineMode)
                    {
                        lobbyID = ReadULong(ReadUInt(0xEC1A88) + 0x2F8).ToString();
                        lobbySize = DetermineNetworkLobbyMembers();

                        if (inMenu)
                        {
                            richDetails = "In Lobby";
                            richDetailsExtra = DeterminePlayerPerformance();
                            trackName = "";
                            trackImage = "asrtransformed";
                            racemodeName = "Online";
                            racemodeImage = "online";
                        }
                        else
                        {
                            richDetails = racemodeName;
                            if (racemodeName == "Capture the Chao")
                            {
                                richDetails = "Chao"; //Shorten to Chao
                            }
                        }

                        int playerPtr = DetermineNetworkPlayerPointer();
                        // Determine online mode
                        switch ((ReadULong(ReadUInt(0xEC1A88) + 0x101D6C) & 0x3F) - 13)
                        {
                            case 0:
                                richState = "MM Race";
                                playerRating = ReadInt(playerPtr + 0x2628);
                                if (!inMenu)
                                {
                                    richDetails = ""; // already know they are playing single race
                                }
                                break;
                            case 1:
                                richState = "MM Arena";
                                break;
                            case 2:
                                richState = "Lucky Dip";
                                break;
                            case 3:
                                richState = "Custom Game";
                                break;
                        }
                    }
                    else
                    {
                        lobbyID = "";
                        lobbySize = 0;
                        if (inMenu)
                        {
                            richState = "Game Menu";
                            richDetails = "";
                            trackName = "";
                            trackImage = "asrtransformed";
                            racemodeName = "";
                            racemodeImage = "";
                        }
                        else
                        {
                            if (racemodeName != menuState && racemodeName != "Grand Prix") // only show race mode info when necessary
                            {
                                richState = menuState;
                                richDetails = racemodeName;
                            }
                            else
                            {
                                richState = "";
                                if (menuState == "Time Attack")
                                {
                                    richDetails = "TA"; // Shorten Time Attack to TA
                                }
                                else
                                {
                                    richDetails = menuState;
                                }
                            }
                        }
                    }

                    // Set timestamp
                    if ((lastRichState != richState || richDetails != lastRichDetails))
                    {
                        startTimestamp = DateTime.UtcNow;
                    }
                    lastRichState = richState;
                    lastRichDetails = richDetails;
                    
                    // Add extra info
                    if (richStateExtra != "")
                    {
                        richState += (richState == "" ? "" : " - ") + richStateExtra;
                    }
                    if (richDetailsExtra != "")
                    {
                        richDetails += (richDetails == "" ? "" : " - ") + richDetailsExtra;
                    }

                    client.SetPresence(new RichPresence()
                    {
                        Details = richDetails,
                        State = richState,
                        Timestamps = new Timestamps
                        {
                            Start = startTimestamp
                        },
                        
                        Party = new Party()
                        {
                            ID = lobbyID,
                            Size = lobbySize,
                            Max = 10,
                        },
                        Assets = new Assets()
                        {
                            LargeImageKey = trackImage,
                            LargeImageText = trackName,
                            SmallImageKey = racemodeImage,
                            SmallImageText = racemodeName,
                        },
                        Secrets = new Secrets()
                        {
                            JoinSecret = lobbyID == "" ? "" : "secret_" + lobbyID,
                        }
                    });;

                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            // The rich presence program will shut down when this function returns
            return 0;
        }

        public string GetTimeString(float time)
        {
            if (time <= 0 || time >= 600)
            {
                return "";
            }
            return TimeSpan.FromSeconds(time).ToString("m\\:ss\\.fff");
        }

        public int DetermineNetworkLobbyMembers()
        {
            int connectedClients = ReadByte(ReadInt(0xEC1A88) + 0x525);
            int total = 0;
            for (int i = 0; i < connectedClients; i++)
            {
                total += ReadInt(ReadInt(ReadInt(0xEC1A88) + 0x528 + i * 4) + 0x25D0);
            }
            return total;
        }

        public string GetRaceInfo()
        {
            int playerPtr = DetermineEnginePlayerPointer();
            if (playerPtr == 0)
            {
                return "";
            }
            int position = DeterminePosition(playerPtr, true);
            int lap = DetermineRaceLap(playerPtr);
            if (position == -1 || lap == -1)
            {
                return "";
            }

            if (lap == 4 || ReadBoolean(0xBCE930)) // either finishes the race or gets DNF
            {
                return "Finished " + position + GetOrdinal(position);             
            }
            else
            {
                return "Lap " + lap + ", " + position + GetOrdinal(position);
            }
        }

        public int DetermineRaceLap(int playerPtr)
        {
            if (playerPtr == 0)
            {
                return -1;
            }
            int lap = ReadByte(ReadInt(playerPtr + 0xC1B8) + 0x8) + 1;
            if (lap > 4)
            {
                return -1;
            }
            return lap;
        }

        // The player pointer used by the game engine
        public int DetermineEnginePlayerPointer()
        {
            for (int i = 0; i < 10; i++) // iterate over player list
            {
                int playerPtr = ReadInt(ReadInt(0xBCE920) + i * 4);
                if (playerPtr != 0 && ReadInt(playerPtr + 0xC878) == 0)
                {
                    return playerPtr; // player found
                }
            }
            return 0;
        }

        // The player pointer used by the game engine
        public int DetermineEnginePlayerIndex()
        {
            for (int i = 0; i < 10; i++) // iterate over player list
            {
                int playerPtr = ReadInt(ReadInt(0xBCE920) + i * 4);
                if (playerPtr != 0 && ReadInt(playerPtr + 0xC878) == 0)
                {
                    return i; // player found
                }
            }
            return -1;
        }


        // The player pointer used for networking features
        public int DetermineNetworkPlayerPointer()
        {
            int connectedClients = ReadByte(ReadInt(0xEC1A88) + 0x525);
            for (int i = 0; i < connectedClients; i++) // iterate over player list
            {
                int playerPtr = ReadInt(ReadInt(0xEC1A88) + 0x528 + i * 4);
                if (ReadInt(playerPtr + 0x10) == 0)
                {
                    return playerPtr; // player found
                }
            }
            return 0;
        }

        public string GetOrdinal(int position)
        {
            switch (position)
            {
                case 1:
                    return "ˢᵗ";
                case 2:
                    return "ⁿᵈ";
                case 3:
                    return "ʳᵈ";
                default:
                    return "ᵗʰ";
            }
        }

        public string DeterminePlayerPerformance()
        {
            int playerPtr = DetermineNetworkPlayerPointer();
            if (playerPtr == 0)
            {
                return "";
            }
            if ((ReadByte(ReadInt(0xEC1A88) + 0x101D64) & 0x80) == 0)
            {
                return "Rating: " + ReadInt(playerPtr + 0x25FC) / 100000;
            }
            else
            {
                return "Score: " + ReadInt(playerPtr + 0x2628);
            }
        }

        public string GetChaoInfo()
        {
            int playerPtr = DetermineEnginePlayerPointer();
            if (playerPtr == 0)
            {
                return "";
            }
            int chaosCaptured = DetermineChaosCaptured(playerPtr);
            int position = DeterminePosition(playerPtr, false);
            if (chaosCaptured == -1 || position == -1)
            {
                return "";
            }

            if (ReadBoolean(0xBCE930))
            {
                return "Finished " + position + GetOrdinal(position);
            }
            else
            {
                return chaosCaptured + " captured, " + position + GetOrdinal(position);
            }            
        }

        public string GetBattleInfo(bool race)
        {
            int playerPtr = DetermineEnginePlayerPointer();
            if (playerPtr == 0)
            {
                return "";
            }
            int lives = DetermineLives(playerPtr);
            if (lives == -1)
            {
                return "";
            }
            int position = DeterminePosition(playerPtr, race);
            if (position == -1)
            {
                return "";
            }

            if (ReadBoolean(0xBCE930))
            {
                return "Finished " + position + GetOrdinal(position);
            }

            string info = "";
            switch (lives)
            {
                case 0:
                    switch (DetermineGhostHits(playerPtr))
                    {
                        case 0:
                            info = "👻";
                            break;
                        case 1:
                            info = "👻💔";
                            break;
                        case 2:
                            info = "👻💔💔";
                                break;
                    }
                    break;
                case 1:
                    info = "❤️";
                    break;
                case 2:
                    info = "❤️❤️";
                    break;
                case 3:
                    info = "❤️❤️❤️";
                    break;
            }
            info += " " + position + GetOrdinal(position);
            return info;
        }

        public int DetermineChaosCaptured(int playerPtr)
        {
            if (playerPtr == 0)
            {
                return -1;
            }
            int chaosCaptured = ReadByte(ReadInt(playerPtr + 0xC1B8) + 0xC0);
            if (chaosCaptured > 5)
            {
                return -1;
            }
            return chaosCaptured;
        }

        public int DeterminePosition(int playerPtr, bool race)
        {
            if (playerPtr == 0)
            {
                return -1;
            }
            int position = ReadByte(ReadInt(playerPtr + 0xC1B8) + (race ? 0x14 : 0x10)) + 1;
            if (position > 10)
            {
                return -1;
            }
            return position;
        }

        public int DetermineLives(int playerPtr)
        {
            if (playerPtr == 0)
            {
                return -1;
            }
            int lives = ReadByte(ReadInt(playerPtr + 0xC1B8) + 0xBC);
            if (lives > 3)
            {
                return -1;
            }
            return lives;
        }

        public int DetermineGhostHits(int playerPtr)
        {
            if (playerPtr == 0)
            {
                return -1;
            }
            int ghostHits = ReadByte(ReadInt(playerPtr + 0xC1B8) + 0xBE);
            if (ghostHits > 2)
            {
                return -1;
            }
            return ghostHits;
        }

        private void OnJoin(object sender, JoinMessage args)
        {
            Process.Start("steam://joinlobby/212480/" + args.Secret.Substring(7));
        }

        public string GetSteamLocation()
        {
            using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam"))
            {
                if (key == null) return null;
                return key.GetValue("SteamExe") as string;
            }
        }
    }
}
