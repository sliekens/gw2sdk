# Game link

The entry point for accessing game client information is `GuildWars2.GameLink`. This class implements `IObservable` so you should be familiar with the publisher-subscriber model. I recommend to use the Reactive Extensions ([System.Reactive][Rx]) library to subscribe to the GameLink. Rx is a powerful library that makes it easy to work with asynchronous data streams.

To use the GameLink, pass an observer to `GameLink.Subscribe()`. An observer is a class that implements `IObserver<GameTick>`, or an `Action<GameTick>` when using Rx. The GameLink will then start pushing GameTicks to the observer (with configurable intervals). Each GameTick corresponds to a single update from the game client and it's usually updated on every frame. You can use `GameLink.Open(TimeSpan)` with a refresh interval of your choice if you need less frequent updates.

The game client (Gw2-64.exe) must be running for the GameLink to work.

## What information does the game client provide?

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

Technically, the game writes this information to a memory-mapped file on every frame, which we then read on a configureable refresh interval. The GameLink is not a hack or a cheat. It's a supported feature of the game client.

## Basic usage

The general usage pattern is to open the GameLink, subscribe to it, and then wait for updates.

``` csharp
using System;
using GuildWars2;

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException();
}

var refreshInterval = TimeSpan.FromSeconds(1);
using var gameLink = GameLink.Open(refreshInterval);

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

The default name of the memory-mapped file is `MumbleLink` but it can be changed with a command line flag, which is useful if you need to run multiple game clients and use the GameLink with each running instance.

``` cmd
gw2-64.exe -mumble OtherLink
```

Now you must pass the same name to `GameLink.Open()`.

``` csharp
var link = GameLink.Open(name: "OtherLink");
```

(Side note: `-mumble 0` disables MumbleLink output.)

## Example

Here is an example observer that prints an update to the console every second.

It illustrates the following concepts:

- How to check if the GameLink is supported on the current platform
- How to subscribe to the `GameLink` with Rx
- How to use the `GameTick` properties to get the player's current map and specialization name
- How to use the `Gw2Client` to query the API for the map and specialization names
- How to check if the player is in combat, typing, looking at the map, etc.

[!code-csharp[](../../samples/Mumble/Program.cs)]

Example output:

(Some lines were removed for brevity. I recommend to run this sample yourself to see the output in realtime.)

``` text
TODO update me
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/
