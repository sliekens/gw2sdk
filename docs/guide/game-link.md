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

``` text
09:59:47.657 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100] Start processing HTTP request GET https://api.guildwars2.com/v2/maps?ids=all&v=2022-03-23T19:00:00.000Z
09:59:47.703 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100] Sending HTTP request GET https://api.guildwars2.com/v2/maps?ids=all&v=2022-03-23T19:00:00.000Z
09:59:47.958 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101] Received HTTP response headers after 250.0283ms - 200
09:59:47.959 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101] End processing HTTP request after 317.3058ms - 200
09:59:48.061 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100] Start processing HTTP request GET https://api.guildwars2.com/v2/specializations?ids=all&v=2022-03-23T19:00:00.000Z
09:59:48.061 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100] Sending HTTP request GET https://api.guildwars2.com/v2/specializations?ids=all&v=2022-03-23T19:00:00.000Z
09:59:48.101 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101] Received HTTP response headers after 39.319ms - 200
09:59:48.101 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101] End processing HTTP request after 39.5207ms - 200
09:59:48.136 info: GuildWars2.GameLink[0] [58914] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -856.519, Longitude = 570.44257, Elevation = 28.097889 }
09:59:48.151 info: Microsoft.Hosting.Lifetime[0] Application started. Press Ctrl+C to shut down.
09:59:48.151 info: Microsoft.Hosting.Lifetime[0] Hosting environment: Production
09:59:48.151 info: Microsoft.Hosting.Lifetime[0] Content root path: X:\src\GW2SDK
09:59:49.116 info: GuildWars2.GameLink[0] [58975] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -856.519, Longitude = 570.44257, Elevation = 28.097889 }
09:59:50.114 info: GuildWars2.GameLink[0] [59034] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -856.519, Longitude = 570.44257, Elevation = 28.097889 }
09:59:51.113 info: GuildWars2.GameLink[0] [59097] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -856.519, Longitude = 570.44257, Elevation = 28.097889 }
09:59:52.123 info: GuildWars2.GameLink[0] [59160] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -856.519, Longitude = 570.44257, Elevation = 28.097889 }
09:59:53.118 info: GuildWars2.GameLink[0] [59226] Invert Control, the Human Engineer (Mechanist) is on Skyscale in Arborstone (PublicMini), Position: { Latitude = -854.0615, Longitude = 568.6599, Elevation = 27.56997 }
09:59:54.117 info: GuildWars2.GameLink[0] [59278] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -844.7674, Longitude = 561.7398, Elevation = 22.347445 }
09:59:55.125 info: GuildWars2.GameLink[0] [59333] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -838.48975, Longitude = 556.62787, Elevation = 9.980106 }
09:59:56.117 info: GuildWars2.GameLink[0] [59399] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -833.0732, Longitude = 549.5106, Elevation = 2.9267163 }
09:59:57.127 info: GuildWars2.GameLink[0] [59464] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -826.98865, Longitude = 541.961, Elevation = 2.0309002 }
09:59:58.126 info: GuildWars2.GameLink[0] [59521] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -820.667, Longitude = 535.5705, Elevation = 2.1029532 }
09:59:59.119 info: GuildWars2.GameLink[0] [59581] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle in Arborstone (PublicMini), Position: { Latitude = -831.20386, Longitude = 537.6345, Elevation = 1.891227 }
10:00:00.114 info: GuildWars2.GameLink[0] [59650] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle in Arborstone (PublicMini), Position: { Latitude = -852.5697, Longitude = 552.98627, Elevation = -0.5047009 }
10:00:01.114 info: GuildWars2.GameLink[0] [59725] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle in Arborstone (PublicMini), Position: { Latitude = -880.77545, Longitude = 582.04364, Elevation = 12.155696 }
10:00:02.124 info: GuildWars2.GameLink[0] [59808] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle in Arborstone (PublicMini), Position: { Latitude = -912.4395, Longitude = 612.05066, Elevation = 11.06934 }
10:00:03.123 info: GuildWars2.GameLink[0] [59886] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -934.91766, Longitude = 633.8974, Elevation = 14.476834 }
10:00:04.114 info: GuildWars2.GameLink[0] [59980] Invert Control, the Human Engineer (Mechanist) is looking at the map
10:00:05.129 info: GuildWars2.GameLink[0] [60122] Invert Control, the Human Engineer (Mechanist) is looking at the map
10:00:06.129 info: GuildWars2.GameLink[0] [60263] Invert Control, the Human Engineer (Mechanist) is looking at the map
10:00:07.118 info: GuildWars2.GameLink[0] [60394] Invert Control, the Human Engineer (Mechanist) is looking at the map
10:00:08.127 info: GuildWars2.GameLink[0] [60504] Invert Control, the Human Engineer (Mechanist) is typing
10:00:09.126 info: GuildWars2.GameLink[0] [60619] Invert Control, the Human Engineer (Mechanist) is typing
10:00:10.119 info: GuildWars2.GameLink[0] [60732] Invert Control, the Human Engineer (Mechanist) is typing
10:00:11.118 info: GuildWars2.GameLink[0] [60846] Invert Control, the Human Engineer (Mechanist) is typing
10:00:12.114 info: GuildWars2.GameLink[0] [60956] Invert Control, the Human Engineer (Mechanist) is typing
10:00:13.127 info: GuildWars2.GameLink[0] [61072] Invert Control, the Human Engineer (Mechanist) is typing
10:00:14.124 info: GuildWars2.GameLink[0] [61182] Invert Control, the Human Engineer (Mechanist) is typing
10:00:15.126 info: GuildWars2.GameLink[0] [61289] Invert Control, the Human Engineer (Mechanist) is afk
10:00:16.119 info: GuildWars2.GameLink[0] [61398] Invert Control, the Human Engineer (Mechanist) is afk
10:00:17.117 info: GuildWars2.GameLink[0] [61511] Invert Control, the Human Engineer (Mechanist) is afk
10:00:17.536 info: Microsoft.Hosting.Lifetime[0] Application is shutting down...
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/
