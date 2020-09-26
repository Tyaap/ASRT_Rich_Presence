## How to use
Copy these files to the game directory and then run the game:
* dinput8.dll
* RichPresence.dll
* DiscordRPC.dll
* Newtonsoft.Json.dll

## How it works
1. When the game launches it loads dinput8.dll. THis is a proxy for the real DirectInput API made by Microsoft.
2. The game calls DirectInput8Create which triggers the creation of a new thread.
3. This thread runs a CLR bootstrapper which loads RichPresence.dll
4. The managed code inside RichPresence.dll runs the Discord rich presence client.

## How to develop
* Rich presence features are to be developed inside the RichPresence project.
* The code for the project starts in Program.cs. Currently there is just a simple rich presence test.

## Libraries used
* dinput-proxy-dll - https://github.com/zerosum0x0/dinput-proxy-dll
* discord-rpc-csharp - https://github.com/Lachee/discord-rpc-csharp
