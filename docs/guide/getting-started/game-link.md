# Game link

The game client provides the following realtime information about the player:

- The player's character name, [race], [profession], and [specialization].
- The player's [world] ID.
- Whether the player is in a competitive game type (PvP, WvW).
- The player's team color in a competitive game.
- Whether the player is in combat.
- Whether the player is the leader of a squad.
- The player's mount (while using a mount).
- The player's current map ID and map type
- The player's coordinates and orientation.
- The camera's coordinates, orientation and vertical field of view.

It also provides information about UI elements:

- The UI size setting (Small, Normal, Large, Larger).
- The width and height of the in-game compass.
- The compass/world map zoom factor.
- Whether the world map is open.
- Whether the compass is docked at the top or bottom.
- Whether rotation is enabled for the compass, and the current rotation.
- Whether a textbox has focus.

Last but not least, some metadata about the game:

- The server IP address (same as typing /ip in the game).
- The shard ID (its purpose is currently unknown).
- The instance ID (seems unused).
- The build number of the game client.
- The process ID of the game client.
- Whether the game client has focus.

> [!NOTE]
> The GameLink is not a hack nor a cheat, it's a supported feature of the game client.
> It is an implementation of the MumbleLink protocol, which is used by the Mumble voice chat software.
> Technically, the game periodically writes all this information to a memory-mapped file,
> which we then read on a configureable refresh interval.

## Basic usage

The entry point for accessing game client information is `GuildWars2.GameLink`.
This class implements `IObservable` so you should be familiar with the
publisher-subscriber model. The general usage pattern is to open the GameLink,
subscribe to it, and then wait for updates.

I recommend to use the Reactive Extensions
([System.Reactive][Rx]) library to subscribe to the GameLink. Rx is a powerful
library that makes it easy to work with asynchronous data streams.

To use the GameLink, pass an observer to `GameLink.Subscribe()`. An observer is
a class that implements `IObserver<GameTick>`, or an `Action<GameTick>` when
using Rx. The GameLink will then start pushing GameTicks to the observer (with
configurable intervals). Each GameTick corresponds to a single update from the
game client and it's usually updated on every frame. You can use
`GameLink.Open(TimeSpan)` with a refresh interval of your choice if you need less
frequent updates.

The game client (Gw2-64.exe) must be running for the GameLink to work.

``` csharp
using System;
using GuildWars2;

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException();
}

var refreshInterval = TimeSpan.FromSeconds(1);
await using var gameLink = GameLink.Open(refreshInterval);

gameLink.Subscribe(
    gameTick =>
    {
        var player = gameTick.GetIdentity();
        if (player is not null)
        {
            Console.WriteLine($"{player.Name} is ready to go!");
        }
    }
);
```

## Multiple game clients

The default name of the memory-mapped file is `MumbleLink` but it can be changed
with a command line flag, which is useful if you need to run multiple game clients
and use the GameLink with each running instance.

``` cmd
gw2-64.exe -mumble OtherLink
```

Now you must pass the same name to `GameLink.Open()`.

``` csharp
var link = GameLink.Open(name: "OtherLink");
```

(Side note: `-mumble 0` disables MumbleLink output.)

## Example

Here is an example observer that prints all available information to the console.
You can download and run this example from the [samples directory](https://github.com/sliekens/gw2sdk/tree/main/samples/Mumble).

It illustrates the following concepts:

- How to check if the GameLink is supported on the current platform
- How to subscribe to the `GameLink` with Rx
- How to use the `GameTick` properties to get the player's current map, specialization, etc.
- How to use the `Gw2Client` to cross-reference information from the game client with API data
- How to check if the player is in combat, typing, looking at the map, etc.

The example consists of a few classes:

- `GameListener` is the observer that prints the game client information to the console.
- `ReferenceData` contains the API data that is used to cross-reference the game client information.
- `DataService` is a helper class that fetches the API data.
- `Program` is the entry point that wires everything together.

### GameListener.cs
[!code-csharp[](~/samples/Mumble/GameListener.cs)]

### ReferenceData.cs
[!code-csharp[](~/samples/Mumble/ReferenceData.cs)]

### DataService.cs
[!code-csharp[](~/samples/Mumble/DataService.cs)]

### Program.cs
[!code-csharp[](~/samples/Mumble/Program.cs)]

### Example output

``` text
Build                          : 172,493
Process                        : 49,752
World                          : Ring of Fire (Language: English, Region: Europe, Population: High, ID: 2005)
Server IP                      : 3.77.138.137
Shard ID                       : 000007D5
Name                           : Invert Control
Race                           : Human
Profession                     : Engineer
Specialization                 : Holosmith (ID: 57)
Map                            : Gilded Hollow (Instance, ID: 1121)
Instance                       : 00000000
Current mount                  :
UI Size                        : Normal
Compass dimensions             : Width = 362 Height = 170
Compass can rotate             : ❌
Compass docked top-right       : ❌
Compass/world map scale        : 1.000
World map is open              : ❌
Player is in PvP game type     : ❌
Team color                     : none
Player is in combat            : ❌
Player is squad leader         : ❌
Textbox has focus              : ❌
Game has focus                 : ❌
Field of View                  : 0.646
Map Center                     : X = 35,824.773 Y = 33,211.641
Player Coordinates             : X = 35,824.773 Y = 33,211.641
Avatar Coordinates             : X =    -87.311 Y =     61.660 Z =    -36.357
Avatar Orientation             : X =      0.238 Y =      0.000 Z =     -0.971
Avatar Top                     : X =      0.000 Y =      0.000 Z =      0.000
Camera Coordinates             : X =    -90.676 Y =     70.302 Z =    -22.400
Camera Orientation             : X =      0.224 Y =     -0.335 Z =     -0.915
Camera Top                     : X =      0.000 Y =      0.000 Z =      0.000
Tick                           : 150,712
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/
