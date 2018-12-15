
![alt text](https://i.imgur.com/TGihCLc.png)

# Introduction
RWRichPresence is a mod I've made for the RimWorld that lets it interface with Discord and lets it have a rich presence.
Currently the rich presence shows your colony name, days (and hours into the day) and the current quadrum - keeping it as minimal as possible. It updates your rich presence twice per ingame hour. You are free to look into the code if you want to do something like this yourself for another application or game. 

# Installation
Go to the releases tab and grab the latest zip. In it you will find two folders, 'Mods' and 'RimWorldWin_Data'. Drop both of these into the root of your game folder (the folder containing the rimworld exe). Only Windows version as of now. Might work with other OS too, havent really tested that.

You might notice the installation Is different than other mods, because it also adds a 0discord-rpc.dll file into your RimWorldWin_Data/Mono (RimWorldWin64_Data/Mono if youre running the 64bit version) folder. That DLL is needed to be in the mono folder and, trust me I'd have it in the mod's assembly folder but the problem is the game cant load it from the assembly folder (and I have no idea why).

Discord-rpc.dll file is gotten directly from Discord's GitHub (https://github.com/discordapp/discord-rpc). Go to releases tab and choose the windows one, open the zip, find the win64/32 (depends on if you're running the 32 bit version or not) dynamic folder and put the discord-rpc.dll into your RimWorldWin_Data64/Mono then rename it to '0discord-rpc.dll'. 

#
For Discord to recognize that youre playing it and add the presence you need to add the RimWorld as a game in your Discord settings.
