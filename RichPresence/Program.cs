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

				// Simple rich presence test
                while (true)
                {
					System.Threading.Thread.Sleep(5000);
					client.SetPresence(new RichPresence()
					{
						Details = "S&ASRT rich presence test",
						State = "Playing S&ASRT"
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