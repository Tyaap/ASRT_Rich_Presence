// Launches the game through Steam, because for some reason Discord can't do it!
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
