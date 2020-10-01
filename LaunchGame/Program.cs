// Launches the game through Steam and joins a lobby

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

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
