# Game link

The game client provides information about the server IP address, the player's position and movement on the current map, the player's camera and field of view, the location and size of the in-game compass, whether the player is using a commander tag, the player's color in competitive games, whether the player is in combat, whether the player is using a mount, the player's race, profession and specialization.

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

