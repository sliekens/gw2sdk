# Game link

The game client provides realtime information about the player:

- the player's character name, [race], [profession] and [specialization]
- the player's [world] ID
- whether the player is in a competitive game mode
- the player's team color in competitive games
- whether the player is in combat
- whether the player is using a commander tag
- the player's mount (while using a mount)
- the player's current map id, map type and player/map coordinates
- the player's camera and field of view

It also provides information about UI elements:

- the UI size setting (Small, Normal, Large, Larger)
- the width and height of the in-game compass
- whether the compass is docked at the top or bottom
- whether rotation is enabled for the compass, and the current rotation factor

Last but not least, some metadata about the game.

- the process ID of the game client
- the build number of the game client
- whether the game client has focus
- whether the chat box has focus
- the server IP address that the client connects to (same as typing /ip in the game)

The entry point for accessing this information is `GuildWars2.GameLink`. This class implements `IObservable` so you should be familiar with the publisher-subscriber model. (Or have enough time and patience to learn it now. :D)

To use the GameLink, create an observer object that implements `IObserver<Snapshot>` and pass it to `GameLink.Subscribe()`. The GameLink will then start pushing status updates to your observer class on a timer (with configurable intervals).

Here is an example observer that prints each update to the console. The link provides map IDs and specialization IDs that can be used to fetch data from the API.

<<< @/samples/Mumble/Program.cs

Output

> [2720160] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.73032, Up = 60.812393, Front = -72.88447 }  
> [2720161] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.73032, Up = 60.812393, Front = -72.88447 }  
> [2720162] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.60268, Up = 60.812363, Front = -74.61272 }  
> [2720163] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.47545, Up = 60.812332, Front = -76.33568 }  
> [2720164] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.3486, Up = 60.8123, Front = -78.053375 }  
> [2720165] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.22212, Up = 60.812267, Front = -79.7658 }  
> [2720166] Invert Control, the Human Engineer (Mechanist) is traveling on RollerBeetle in Divinity's Reach, Position: { Right = -203.22212, Up = 60.82527, Front = -79.7658 }  
> [2720167] Invert Control, the Human Engineer (Mechanist) is traveling on foot in Divinity's Reach, Position: { Right = -203.26134, Up = 63.417263, Front = -78.74602 }  
> [2720168] Invert Control, the Human Engineer (Mechanist) is traveling on foot in Divinity's Reach, Position: { Right = -203.26134, Up = 63.752113, Front = -78.74602 }  
> [2720169] Invert Control, the Human Engineer (Mechanist) is traveling on foot in Divinity's Reach, Position: { Right = -203.26134, Up = 64.034134, Front = -78.74602 }  
> Ctrl+C  
> Goodbye.

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World