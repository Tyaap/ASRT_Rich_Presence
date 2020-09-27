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

                // Defining important variable
                string menuState = "";
                string trackName = "";
                int inMenu = 1;
                int isOnlineMode = 0;
                string lobbyID = "";
                int lobbySize = 0;


                // Final variables for Discord RichPresence
                string richState = "Game Started";
                string richDetails = "";

				// Simple rich presence test
                while (true)
                {
					System.Threading.Thread.Sleep(5000);

                    // Determine track name
                    switch (ReadUInt(ReadUInt(0xBC7434) + 0))
                    {
                        case 0:
                            trackName = "Dummy";
                            break;
                        case 0xD4257EBD:
                            trackName = "Ocean View";
                            break;
                        case 0x32D305A8:
                            trackName = "Samba Studios";
                            break;
                        case 0xC72B3B98:
                            trackName = "Carrier Zone";
                            break;
                        case 0x03EB7FFF:
                            trackName = "Dragon Canyon";
                            break;
                        case 0xE3121777:
                            trackName = "Temple Trouble";
                            break;
                        case 0x4E015AB6:
                            trackName = "Galactic Parade";
                            break;
                        case 0x503C1CBC:
                            trackName = "Seasonal Shrines";
                            break;
                        case 0x7534B7CA:
                            trackName = "Rogue's Landing";
                            break;
                        case 0x38A394ED:
                            trackName = "Dream Valley";
                            break;
                        case 0xC5C9DEA1:
                            trackName = "Chilly Castle";
                            break;
                        case 0xD936550C:
                            trackName = "Graffity City";
                            break;
                        case 0x4A0FF7AE:
                            trackName = "Sancturay Falls";
                            break;
                        case 0xCD8017BA:
                            trackName = "Graveyard Gig";
                            break;
                        case 0xDC93F18B:
                            trackName = "Adder's Lair";
                            break;
                        case 0x2DB91FC2:
                            trackName = "Burning Depths";
                            break;
                        case 0x94610644:
                            trackName = "Race Of Ages";
                            break;
                        case 0xE6CD97F0:
                            trackName = "Sunshine Tour";
                            break;
                        case 0xE87FDF22:
                            trackName = "Shibuya Downtown";
                            break;
                        case 0x17463C8D:
                            trackName = "Roulette Road";
                            break;
                        case 0xFEBC639E:
                            trackName = "Egg Hangar";
                            break;
                        case 0x1EF56CE1:
                            trackName = "Outrun Bay";
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
                            menuState = "GP mode";
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
                        richDetails = trackName;
                    }

                    // Initial code for online support
                    client.RegisterUriScheme("212480");

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
      /*                  Secrets = new Secrets()
                        {
                            //CreateSecret = lobbyID,
                            JoinSecret = lobbyID + "_salt",
                        }*/
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
