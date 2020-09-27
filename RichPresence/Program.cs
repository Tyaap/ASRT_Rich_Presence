using DiscordRPC;
using DiscordRPC.Logging;
using System;
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

				// Example event subscription
				client.OnReady += (sender, e) =>
				{
					MessageBox.Show("Received Ready from user " + e.User.Username);
				};


                // Connect to the Discord IPC
                client.Initialize();

                // Initial code for online support
                client.RegisterUriScheme("212480");

                // FriendlySecret
                string friendlySecret = Secrets.CreateFriendlySecret(new Random());
                string friendlySecret2 = "";

                // Defining important variable
                string menuState = "";
                string trackName = "";
                string trackImage = "";
                int inMenu = 1;
                int isOnlineMode = 0;
                string lobbyID = "";
                int lobbySize = 0;
                int racemode = 0;
                string racemodeText = "";


                // Final variables for Discord RichPresence
                string richState = "Game Started";
                string richDetails = "";

				// Simple rich presence test
                while (true)
                {
					System.Threading.Thread.Sleep(5000);


                    // Determine race mode
                    racemode = (ReadInt(0xBC7430) - ReadInt(0xBD0270)) / 0xB4;
                    if (racemode < 0) { racemode = 0; }
                    switch (racemode)
                    {
                        case 0:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 1:
                            racemodeText = "Time Attack";
                            break;
                        case 2:
                            racemodeText = "Grand Prix";
                            break;
                        case 3:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 4:
                            racemodeText = "Race";
                            break;
                        case 5:
                            racemodeText = "Race";
                            break;
                        case 6:
                            racemodeText = "Time Attack";
                            break;
                        case 7:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 8:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 9:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 10:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 11:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 12:
                            racemodeText = "Delphinus Defence";
                            break;
                        // case 13 crashes the game
                        case 14:
                            racemodeText = "Race";
                            break;
                        case 15:
                            racemodeText = "Battle";
                            break;
                        case 16:
                            racemodeText = "Battle";
                            break;
                        case 17:
                            racemodeText = "Battle Race";
                            break;
                        case 18:
                            racemodeText = "Capture The Chao";
                            break;
                        case 19:
                            racemodeText = "Capture The Chao";
                            break;
                        case 20:
                            racemodeText = "Target Smash";
                            break;
                        case 21:
                            racemodeText = "Boost Challenge";
                            break;
                        case 22:
                            racemodeText = "Boost Race";
                            break;
                        case 23:
                            racemodeText = "Crazy Conga";
                            break;
                        case 24:
                            racemodeText = "Crazy Conga";
                            break;
                        case 25:
                            racemodeText = "Time Attack";
                            break;
                        case 26:
                            racemodeText = "Sprint";
                            break;
                        case 27:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 28:
                            racemodeText = "Pursuit";
                            break;
                        case 29:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 30:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 31:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 32:
                            racemodeText = "Drift Challenge";
                            break;
                        case 33:
                            racemodeText = "Traffic Attack";
                            break;
                        case 34:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 35:
                            racemodeText = "Delphinus Defence";
                            break;
                        case 36:
                            racemodeText = "Versus";
                            break;
                        case 37:
                            racemodeText = "Traffic Attack?";
                            break;
                        case 38:
                            racemodeText = "Golden Axe";
                            break;
                        case 39:
                            racemodeText = "Delphinus Defence / Skies of arcadia";
                            break;
                        case 40:
                            racemodeText = "Ring Race";
                            break;
                        case 41:
                            racemodeText = "Delphinus Defence / All Star";
                            break;
                        case 42:
                            racemodeText = "Jet Set Tag";
                            break;
                    }



                    // Determine track name
                    switch (ReadUInt(ReadUInt(0xBC7434) + 0))
                    {
                        case 0:
                            trackName = "Dummy";
                            break;
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
                    } else
                    {
                        inMenu = 0;
                    }

                    // Determine if in online mode or not
                    if (ReadUShort(ReadUInt(0xEC1A88) + 0x525)  == 0)
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
                            friendlySecret2 = friendlySecret;
                        }
                        else
                        {
                            richDetails = "";
                            richState = "Game Menu";
                            lobbyID = "";
                            lobbySize = 0;
                            friendlySecret2 = "";
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
                            friendlySecret2 = friendlySecret;
                        }
                        else
                        {
                            richState = menuState;
                            lobbyID = "";
                            lobbySize = 0;
                            friendlySecret2 = "";
                        }
                        richDetails = trackName;
                    }


                   


                        //
                    client.SetSubscription(EventType.Join | EventType.Spectate | EventType.JoinRequest);

                    client.SetPresence(new RichPresence()
                    {
                        Details = richDetails,
                        State = richState,
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
                        },
                        Secrets = new Secrets()
                        {
                            //CreateSecret = lobbyID,
                            JoinSecret = friendlySecret2,
                        }
                    });

                }
			}
			catch(Exception e)
            {
				MessageBox.Show(e.ToString());
            }

			// The rich presence program will shut down when this function returns
			return 0;
        }
    }
}