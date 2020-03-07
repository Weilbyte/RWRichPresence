
<p align="center">
<img src="https://i.imgur.com/ruELjgO.png">
</p>

# RWRichPresence
RWRichPresence updates your Discord status with some information about your game. Now updated for the latest version of the game - 1.1!

## Feautures

* Display various game information 
* Customizable text 
* Plenty of settings, so you can enable and disable certain aspects!

*Currently shows:*
* Colony name
* Amount of days colony has lasted
* Hour of the current day
* Quadrum
* Amount of time your game has been running for
* Year

## Installation
*Skip to step 3 if you have the mod from Steam Workshop*

1. Download the latest release [(link)](https://github.com/Weilbyte/RWRichPresence/releases)
2. Extract the `RimRPC` folder from the zip to your RimWorld Mod folder as per usual.
3. Go to Discord's `discord-rpc` release page. [(link)](https://github.com/discordapp/discord-rpc/releases)
4. Download `discord-rpc-win.zip` for windows or `discord-rpc-linux.zip` if you are using linux.
5. Open the zip and follow instructions below depending on your PC architecture and platform:  
  * *(Windows 32 bit)*  Copy  `discord-rpc\win32-dynamic\bin\discord-rpc.dll` into your RimWorld folder - `RimWorld\RimWorldWin_Data\Mono`  
  * *(Windows 64 bit)* Copy `discord-rpc\win64-dynamic\bin\discord-rpc.dll` into your RimWorld folder - `RimWorld\RimWorldWin64_Data\Mono` or for 1.1 `Rimworld\RimWorldWin64_Data\Plugins`
  * *(Linux 64 bit)* Copy `discord-rpc\linux-dynamic\lib\discord-rpc.so` into your RimWorld folder - `RimWorld/RimWorldLinux_Data/Mono/x86_64/`
6. Rename `discord-rpc.dll` to `0discord-rpc.dll`.
  * On linux you will need to rename`discord-rpc.so` to `lib0discord-rpc.so` or `0discord-rpc`

Thats pretty much it, youre set. 

##  Usage
Can be added and removed (spits out a single error that you can ignore) mid-game.  
Once in-game, presence will update twice every **in-game** hour which equals to once every *20.5* **real-time** seconds.

There is a mod settings menu which you can use to tweak what is displayed. From there you can also set custom text (as shown in the image) to replace either the top or bottom line.


Make sure RimWorld is added as a game in Discord's settings.

## Credits
Jdalt490

## Links

[Steam](https://steamcommunity.com/sharedfiles/filedetails/?id=1463057070)
