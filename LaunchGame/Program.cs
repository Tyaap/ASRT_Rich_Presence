// Launches the game through Steam and joins a lobby
using System.Diagnostics;

namespace LaunchGame
{
    class Program
    {
        static void Main()
        {
            Process.Start("steam://rungameid/212480");
        }
    }
}
