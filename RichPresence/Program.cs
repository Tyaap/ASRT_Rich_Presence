using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System;
using System.Diagnostics;
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
                client.OnJoin += OnJoin;

                // Connect to the Discord IPC
                client.Initialize();

                // Initial code for online support
                client.RegisterUriScheme("212480");
                client.SetSubscription(EventType.Join | EventType.JoinRequest);

                // Defining important variable
                string menuState = "";
                string trackName = "";
                string trackImage = "";
                string racemodeName = "";
                string racemodeImage = "";
                int inMenu = 1;
                int isOnlineMode = 0;
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
                    switch (ReadUInt(ReadUInt(0xBCE914) + 0x38))
                    {
                        case 0x4AFC561D:
                            racemodeName = "Single Race";
                            racemodeImage = "singlerace";
                            break;
                        case 0x447473BC:
                            racemodeName = "Battle Arena";
                            racemodeImage = "battlearena";
                            break;
                        case 0xE64B5DD8:
                            racemodeName = "Battle Race";
                            racemodeImage = "battlerace";
                            break;
                        case 0xCCB41574:
                            racemodeName = "Capture the Chao";
                            racemodeImage = "capturethechao";
                            break;
                        case 0x3CBA89B4:
                            racemodeName = "Boost Challenge";
                            racemodeImage = "boostchallenge";
                            break;
                        case 0x79E12D5C:
                            racemodeName = "Time Attack";
                            racemodeImage = "timeattack";
                            break;
                        case 0x77E95ADC:
                            racemodeName = "Sprint";
                            racemodeImage = "sprint";
                            break;
                        case 0xBC7C19CF:
                            racemodeName = "Persuit";
                            racemodeImage = "persuit";
                            break;
                        case 0xB06F818B:
                            racemodeName = "Drift Challenge";
                            racemodeImage = "driftchallenge";
                            break;
                        case 0x9FBFB99B:
                            racemodeName = "Traffic Attack";
                            racemodeImage = "trafficattack";
                            break;
                        case 0xEC587062:
                            racemodeName = "Versus";
                            racemodeImage = "versus";
                            break;
                        case 0x091F29F4:
                            racemodeName = "Grand Prix";
                            racemodeImage = "gprace";
                            break;
                        case 0xAD538D8D:
                            racemodeName = "Ring Race";
                            racemodeImage = "ringrace";
                            break;
                        case 0x61FF5D42:
                            racemodeName = "Boost Race";
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
                            trackName = "Graffity City";
                            trackImage = "graffitycity";
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
                            trackName = "Race Of Ages";
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
                            trackImage = "galacticparade";
                            break;
                        case 0x7583BBD6:
                            trackName = "Battle Bay";
                            trackImage = "carrierzone";
                            break;
                        case 0x997E9C42:
                            trackName = "Creepy Courtyard";
                            trackImage = "graveyardgig";
                            break;
                        case 0x8DABE769:
                            trackName = "Rooftop Rumble";
                            trackImage = "graffitycity";
                            break;
                        case 0x38A73138:
                            trackName = "Monkey Ball Park";
                            trackImage = "templetrouble";
                            break;
                    }

                    // Determine if racing in Mirror mode
                    if (ReadInt(0xBC744C) == 1)
                    {
                        trackName = trackName + " (Mirror)";
                    }


                    // Determine if ingame or in-menu
                    if (ReadUInt(ReadUInt(ReadUInt(0xBCE920) + 0) + 0xC1B8) == 0)
                    {
                        inMenu = 1;
                    }
                    else
                    {
                        inMenu = 0;
                    }

                    // Determine if in online mode or not
                    if (ReadUShort(ReadUInt(0xEC1A88) + 0x525) == 0)
                    {
                        isOnlineMode = 0;
                    }
                    else
                    {
                        isOnlineMode = 1;
                    }


                    // Determine menu state
                    switch (ReadInt(0xC56890))
                    {
                        case 0:
                            menuState = "World Tour";
                            break;
                        case 1:
                            menuState = "Grand Prix";
                            break;
                        case 2:
                            menuState = "Time Attack";
                            break;
                        case 3:
                            menuState = "Single Race";
                            break;
                    }



                    // If in-menu and offline, don't show 
                    if (inMenu == 1)
                    {
                        if (isOnlineMode == 1)
                        {
                            richDetails = "In Lobby";
                            richState = "Matchmaking";
                            lobbyID = ReadULong(ReadUInt(0xEC1A88) + 0x2F8).ToString();
                            lobbySize = ReadUShort(ReadUInt(0xEC1A88) + 0x525);
                        }
                        else
                        {
                            richDetails = "";
                            richState = "Game Menu";
                            lobbyID = "";
                            lobbySize = 0;
                        }
                        trackImage = "asrtransformed";
                    }
                    else
                    {
                        if (isOnlineMode == 1)
                        {
                            richState = "Matchmaking";
                            lobbyID = ReadULong(ReadUInt(0xEC1A88) + 0x2F8).ToString();
                            lobbySize = ReadUShort(ReadUInt(0xEC1A88) + 0x525);
                        }
                        else
                        {
                            richState = menuState;
                            lobbyID = "";
                            lobbySize = 0;
                        }

                        if (richState != racemodeName)
                        {
                            richDetails = racemodeName;
                        }
                    }

                    // Set timestamp
                    if (lastRichState != richState || richDetails != lastRichDetails)
                    {
                        startTimestamp = DateTime.UtcNow;
                    }
                    lastRichState = richState;
                    lastRichDetails = richDetails;

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

        private static void OnJoin(object sender, JoinMessage args)
        {
            Process.Start("steam://joinlobby/212480/" + args.Secret.Substring(7));
        }
    }
}
