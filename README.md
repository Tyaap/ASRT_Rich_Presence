# S&ASRT Rich Presence
#### [Discord Rich Rresence](https://discord.com/rich-presence) for Sonic & All-Stars Racing Transformed! ####

When playing S&ASRT your profile will show:
* The track/arena you are playing
* The game mode you are playing
* When you are in an online lobby, and how many people are in it
* Many more details, for example the lap and position in a race
* Pretty icons for the track and game mode you are playing

#### You can also join other players from their Discord profile, and send invitations directly in the Discord chat!* ####

*The other players must also have S&ASRT Rich Presence installed for this to work, so get them to install it too. :)
## How to install
1. Copy these files to the game directory:
   - `dinput8.dll`
   - `RichPresence.dll`
   - `DiscordRPC.dll`
   - `Newtonsoft.Json.dll`
2. Check the [Discord settings](https://i2.wp.com/www.techjunkie.com/wp-content/uploads/2020/08/Screenshot-21.png) to make sure S&ASRT is added.
3. Run the game and check your Discord status!

## How it works
1. When the game launches it loads `dinput8.dll`. This is written in pure C, and is a proxy for the real DirectInput API made by Microsoft.
2. The game calls `DirectInput8Create` from this DLL, which triggers the creation of a new thread.
3. This thread runs a CLR bootstrapper which loads `RichPresence.dll`
4. The .NET code inside `RichPresence.dll` hosts a Discord rich presence client.

## How to develop
* Clone and build the project using [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/).
* Rich presence features are to be developed inside the `RichPresence` project.
* The code for the project starts in `Program.cs`.
* The application ID used in `Program.cs` is for a Discord app which was created using the [Discord Developer Portal](https://discord.com/developers/applications).
* Documentation on Discord rich presence is available [here](https://discord.com/developers/docs/rich-presence/how-to).
* Game memory can be easily accessed using the `MemoryHelper` class.

## Libraries used
* DirectInput Proxy DLL - https://github.com/zerosum0x0/dinput-proxy-dll
* Discord RPC C# - https://github.com/Lachee/discord-rpc-csharp
