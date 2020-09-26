# S&ASRT Rich Presence
 Discord rich presence for Sonic & All-Stars Racing Transformed!

## How to use
1. Clone and build the project using [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/).
2. Copy these files to the game directory:
   - `dinput8.dll`
   - `RichPresence.dll`
   - `DiscordRPC.dll`
   - `Newtonsoft.Json.dll`
3. Make sure S&ASRT is added as an app in the Discord desktop client.
4. Run the game and check your Discord status!

## How it works
1. When the game launches it loads `dinput8.dll`. This is a proxy for the real DirectInput API made by Microsoft.
2. The game calls `DirectInput8Create` which triggers the creation of a new thread.
3. This thread runs a CLR bootstrapper which loads `RichPresence.dll`
4. The .NET code inside `RichPresence.dll` hosts a Discord rich presence client.

## How to develop
* Rich presence features are to be developed inside the RichPresence project.
* The code for the project starts in `Program.cs`. Currently there is just a simple rich presence test.
* The application ID used in Program.cs is for a Discord App which I creted using the [Discord Developer Portal](https://discord.com/developers/applications).

## Libraries used
* DirectInput Proxy DLL - https://github.com/zerosum0x0/dinput-proxy-dll
* Discord RPC C# - https://github.com/Lachee/discord-rpc-csharp
