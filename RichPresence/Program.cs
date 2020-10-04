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
                client.RegisterUriScheme(executable: Directory.GetCurrentDirectory() + "\\LaunchGame.exe");
                client.SetSubscription(EventType.Join);
                client.OnJoin += OnJoin;

                // Defining important variable
                string menuState = "";
                string trackName = "";
                string trackImage = "";
                string racemodeName = "";
                string racemodeImage = "";
                string extraInfo = "";
                bool inMenu = true;
                bool isOnlineMode = false;
                string lobbyID = "";
                int lobbySize = 0;
                DateTime startTimestamp = DateTime.UtcNow;

                // Final variables for Discord RichPresence
                string richState = "";
                string richDetails = "";
                string lastRichState = "";
                string lastRichDetails = "";

                // Simple rich presence test
                while (true)
                {
                    // Determine race mode
                    // Todo: upload the images!
                    extraInfo = "";
                    switch (ReadUInt(ReadUInt(0xBCE914) + 0x38))
                    {
                        case 0x4AFC561D:
                            racemodeName = "Single Race";
                            int position = DetermineRacePosition();
                            if (position > 0)
                            {
                                extraInfo = position + GetOrdinal(position) + " place";
                            }
                            racemodeImage = "singlerace";
                            break;
                        case 0x447473BC:
                            racemodeName = "Battle Arena";
                            racemodeImage = "battlearena";
                            // todo: extraInfo = "[number] lives" or "ghosted"
                            break;
                        case 0xE64B5DD8:
                            racemodeName = "Battle Race";
                            racemodeImage = "battlerace";
                            // todo: extraInfo = "[number] lives" or "ghosted"
                            break;
                        case 0xCCB41574:
                            racemodeName = "Capture the Chao";
                            racemodeImage = "capturethechao";
                            // todo: extraInfo = "[number] captured"
                            break;
                        case 0x3CBA89B4:
                            racemodeName = "Boost Challenge";
                            racemodeImage = "boostchallenge";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0x79E12D5C:
                            racemodeName = "Time Attack";
                            racemodeImage = "timeattack";
                            // todo: extraInfo = "PB [time]"
                            break;
                        case 0x77E95ADC:
                            racemodeName = "Sprint";
                            racemodeImage = "sprint";
                            break;
                        case 0xBC7C19CF:
                            racemodeName = "Persuit";
                            racemodeImage = "persuit";
                            // todo: extraInfo = tank health?
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
                            // todo: extraInfo = "race [number]/4"
                            break;
                        case 0xAD538D8D:
                            racemodeName = "Ring Race";
                            racemodeImage = "ringrace";
                            // todo: extraInfo = "checkpoint [number]/[number]"
                            break;
                        case 0x61FF5D42:
                            racemodeName = "Boost Race";
                            int position2 = DetermineRacePosition();
                            if (position2 > 0)
                            {
                                extraInfo = position2 + GetOrdinal(position2) + " place";
                            }
                            racemodeImage = "boostrace";
                            break;
                    }

                    // Determine track name
                    switch (ReadUInt(ReadUInt(0xBC7434) + 0))
                    {
                        case 0xD4257EBD:
                            trackName = "Ocean View";
                            trackImage = "oceanview";
                            break;
                        case 0x32D305A8:
                            trackName = "Samba Studios";
                            trackImage = "sambastudios";
                            break;
                        case 0xC72B3B98:
                            trackName = "Carrier Zone";
                            trackImage = "carrierzone";
                            break;
                        case 0x03EB7FFF:
                            trackName = "Dragon Canyon";
                            trackImage = "dragoncanyon";
                            break;
                        case 0xE3121777:
                            trackName = "Temple Trouble";
                            trackImage = "templetrouble";
                            break;
                        case 0x4E015AB6:
                            trackName = "Galactic Parade";
                            trackImage = "galacticparade";
                            break;
                        case 0x503C1CBC:
                            trackName = "Seasonal Shrines";
                            trackImage = "seasonalshrines";
                            break;
                        case 0x7534B7CA:
                            trackName = "Rogue's Landing";
                            trackImage = "rogueslanding";
                            break;
                        case 0x38A394ED:
                            trackName = "Dream Valley";
                            trackImage = "dreamvalley";
                            break;
                        case 0xC5C9DEA1:
                            trackName = "Chilly Castle";
                            trackImage = "chillycastle";
                            break;
                        case 0xD936550C:
                            trackName = "Graffiti City";
                            trackImage = "graffiticity";
                            break;
                        case 0x4A0FF7AE:
                            trackName = "Sanctuary Falls";
                            trackImage = "sanctuaryfalls";
                            break;
                        case 0xCD8017BA:
                            trackName = "Graveyard Gig";
                            trackImage = "graveyardgig";
                            break;
                        case 0xDC93F18B:
                            trackName = "Adder's Lair";
                            trackImage = "adderslair";
                            break;
                        case 0x2DB91FC2:
                            trackName = "Burning Depths";
                            trackImage = "burningdepths";
                            break;
                        case 0x94610644:
                            trackName = "Race of AGES";
                            trackImage = "raceofages";
                            break;
                        case 0xE6CD97F0:
                            trackName = "Sunshine Tour";
                            trackImage = "sunshinetour";
                            break;
                        case 0xE87FDF22:
                            trackName = "Shibuya Downtown";
                            trackImage = "shibuyadowntown";
                            break;
                        case 0x17463C8D:
                            trackName = "Roulette Road";
                            trackImage = "rouletteroad";
                            break;
                        case 0xFEBC639E:
                            trackName = "Egg Hangar";
                            trackImage = "egghangar";
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

                    // Determine if racing in Mirror mode
                    if (ReadInt(0xBC744C) == 1)
                    {
                        trackName = trackName + " (Mirror)";
                    }

                    // Determine if ingame or in-menu
                    inMenu = (ReadInt(0xE9A92C) == 0) && (ReadInt(ReadInt(ReadInt(0xBCE920) + 0) + 0xC1B8) == 0);

                    // Determine if in online mode or not
                    isOnlineMode = ReadUShort(ReadUInt(0xEC1A88) + 0x525) != 0;

                    // Determine menu state
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

                    // Online / offline states
                    if (isOnlineMode)
                    {
                        lobbyID = ReadULong(ReadUInt(0xEC1A88) + 0x2F8).ToString();
                        lobbySize = DetermineNetworkLobbyMembers();

                        if (inMenu)
                        {
                            richDetails = "In Lobby";
                            trackName = "";
                            trackImage = "asrtransformed";
                            racemodeName = "Online";
                            racemodeImage = "online";
                        }
                        else
                        {
                            richDetails = racemodeName;
                        }

                        // Determine online mode
                        switch ((ReadULong(ReadUInt(0xEC1A88) + 0x101D6C) & 0x3F) - 13)
                        {
                            case 0:
                                richState = "MM Race";
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
                                racemodeName = "Custom Game";
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
                            richState = menuState;
                            if (racemodeName != menuState && racemodeName != "Grand Prix") // prevent repeating information
                            {
                                
                                richDetails = racemodeName;
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

                    // Add extra racemode info
                    if (extraInfo != "")
                    {
                        if (richDetails != "")
                        {
                            richDetails += " - " + extraInfo;
                        }
                        else
                        {
                            richState += " - " + extraInfo;
                        }
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

        public static int DetermineNetworkLobbyMembers()
        {
            int connectedClients = ReadUShort(ReadUInt(0xEC1A88) + 0x525);
            int total = 0;
            for (int i = 0; i < connectedClients; i++)
            {
                total += ReadUShort(ReadUInt((uint)(ReadUInt(0xEC1A88) + 0x528 + i * 4)) + 0x25D0);
            }
            return total;
        }

        public static int DetermineRacePosition()
        {
            for (int i = 0; i < 10; i++)
            {
                int playerPtr = ReadInt(ReadInt(0xBCE920) + i * 4);
                if (playerPtr != 0 && ReadInt(playerPtr + 0xC878) == 0)
                {
                    int position = ReadInt(ReadInt(playerPtr + 0xC1B8) + 0x14) + 1;
                    return position <= 10 ? position : 0;
                }
            }
            return 0;
        }

        public static string GetOrdinal(int n)
        {
            switch (n)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

        private static void OnJoin(object sender, JoinMessage args)
        {
            Process.Start("steam://joinlobby/212480/" + args.Secret.Substring(7));
        }
    }
}
