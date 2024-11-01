# Game link

The entry point for accessing game client information is `GuildWars2.GameLink`.
This class implements `IObservable` so you should be familiar with the
publisher-subscriber model. I recommend to use the Reactive Extensions
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

Technically, the game writes this information to a memory-mapped file on every frame,
which we then read on a configureable refresh interval. The GameLink is not a hack
or a cheat. It's a supported feature of the game client.

## Basic usage

The general usage pattern is to open the GameLink, subscribe to it, and then wait
for updates.

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

Here is an example observer that prints an update to the console every second.

It illustrates the following concepts:

- How to check if the GameLink is supported on the current platform
- How to subscribe to the `GameLink` with Rx
- How to use the `GameTick` properties to get the player's current map and
  specialization name
- How to use the `Gw2Client` to query the API for the map and specialization names
- How to check if the player is in combat, typing, looking at the map, etc.

[!code-csharp[](~/samples/Mumble/Program.cs)]

Example output:

``` text
18:35:23.275 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100]
      Start processing HTTP request GET https://api.guildwars2.com/v2/maps?v=2024-07-20T01:00:00.000Z&ids=all
18:35:23.327 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100]
      Sending HTTP request GET https://api.guildwars2.com/v2/maps?v=2024-07-20T01:00:00.000Z&ids=all
18:35:23.714 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101]
      Received HTTP response headers after 376.1521ms - 200
18:35:23.715 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101]
      End processing HTTP request after 462.5584ms - 200
18:35:23.807 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100]
      Start processing HTTP request GET https://api.guildwars2.com/v2/specializations?v=2024-07-20T01:00:00.000Z&ids=all
18:35:23.807 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100]
      Sending HTTP request GET https://api.guildwars2.com/v2/specializations?v=2024-07-20T01:00:00.000Z&ids=all
18:35:23.847 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101]
      Received HTTP response headers after 40.0665ms - 200
18:35:23.847 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101]
      End processing HTTP request after 40.2671ms - 200
18:35:23.915 info: GuildWars2.GameLink[0]
      [3488] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -33.8534
      Longitude : -49.47576
      Elevation : 13.945048
18:35:23.929 info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
18:35:23.929 info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
18:35:23.929 info: Microsoft.Hosting.Lifetime[0]
      Content root path: X:\src\gw2sdk
18:35:24.908 info: GuildWars2.GameLink[0]
      [3609] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -46.651352
      Longitude : -29.516237
      Elevation : 17.691519
18:35:25.876 info: GuildWars2.GameLink[0]
      [3714] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -57.658558
      Longitude : -6.914744
      Elevation : 14.740211
18:35:26.869 info: GuildWars2.GameLink[0]
      [3845] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -62.316986
      Longitude : 7.527554
      Elevation : 14.1446085
18:35:27.881 info: GuildWars2.GameLink[0]
      [3963] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -64.49885
      Longitude : 21.395458
      Elevation : 13.836236
18:35:28.873 info: GuildWars2.GameLink[0]
      [4091] Invert Control, the Human Engineer (Mechanist) is on Raptor
      Map       : Hearth's Glow (Instance)
      Latitude  : -64.9567
      Longitude : 35.947556
      Elevation : 15.052669
18:35:29.884 info: GuildWars2.GameLink[0]
      [4219] Invert Control, the Human Engineer (Mechanist) is on Griffon
      Map       : Hearth's Glow (Instance)
      Latitude  : -62.35296
      Longitude : 48.556736
      Elevation : 17.96384
18:35:30.875 info: GuildWars2.GameLink[0]
      [4294] Invert Control, the Human Engineer (Mechanist) is on Griffon
      Map       : Hearth's Glow (Instance)
      Latitude  : -46.91702
      Longitude : 49.754066
      Elevation : 15.200244
18:35:31.888 info: GuildWars2.GameLink[0]
      [4367] Invert Control, the Human Engineer (Mechanist) is on Griffon
      Map       : Hearth's Glow (Instance)
      Latitude  : -41.02358
      Longitude : 38.50398
      Elevation : 15.194383
18:35:32.883 info: GuildWars2.GameLink[0]
      [4436] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Hearth's Glow (Instance)
      Latitude  : -31.513483
      Longitude : 32.636326
      Elevation : 18.421219
18:35:33.878 info: GuildWars2.GameLink[0]
      [4511] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : -21.3906
      Longitude : 29.216703
      Elevation : 15.269578
18:35:34.870 info: GuildWars2.GameLink[0]
      [4584] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 4.274415
      Longitude : 23.628635
      Elevation : 15.269595
18:35:35.884 info: GuildWars2.GameLink[0]
      [4651] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 46.92945
      Longitude : 14.538852
      Elevation : 15.195555
18:35:36.878 info: GuildWars2.GameLink[0]
      [4711] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 91.49019
      Longitude : 7.5282254
      Elevation : 15.2694645
18:35:37.871 info: GuildWars2.GameLink[0]
      [4772] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 135.37587
      Longitude : 0.5434828
      Elevation : 15.196241
18:35:38.883 info: GuildWars2.GameLink[0]
      [4844] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 179.20514
      Longitude : -2.1454942
      Elevation : 15.224134
18:35:39.874 info: GuildWars2.GameLink[0]
      [4936] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 222.37964
      Longitude : -3.9784033
      Elevation : 15.2230835
18:35:40.885 info: GuildWars2.GameLink[0]
      [5057] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 264.95358
      Longitude : -5.361451
      Elevation : 15.26961
18:35:41.878 info: GuildWars2.GameLink[0]
      [5194] Invert Control, the Human Engineer (Mechanist) is on RollerBeetle
      Map       : Hearth's Glow (Instance)
      Latitude  : 306.87436
      Longitude : -6.74818
      Elevation : 15.196122
18:35:42.873 info: GuildWars2.GameLink[0]
      [5291] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Hearth's Glow (Instance)
      Latitude  : 318.6937
      Longitude : -7.420908
      Elevation : 15.234914
18:35:43.882 info: GuildWars2.GameLink[0]
      [5414] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Hearth's Glow (Instance)
      Latitude  : 318.6937
      Longitude : -7.420908
      Elevation : 15.234914
18:35:44.880 info: GuildWars2.GameLink[0]
      [5560] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Hearth's Glow (Instance)
      Latitude  : 318.6937
      Longitude : -7.420908
      Elevation : 15.234914
18:35:45.874 info: GuildWars2.GameLink[0]
      [5624] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Hearth's Glow (Instance)
      Latitude  : 318.6937
      Longitude : -7.420908
      Elevation : 15.234914
18:35:52.881 info: GuildWars2.GameLink[0]
      [5670] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Divinity's Reach (Public)
      Latitude  : -307.03738
      Longitude : 61.644375
      Elevation : 60.82651
18:35:53.878 info: GuildWars2.GameLink[0]
      [5726] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Divinity's Reach (Public)
      Latitude  : -307.03738
      Longitude : 61.644375
      Elevation : 60.82651
18:35:54.873 info: GuildWars2.GameLink[0]
      [5781] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:35:55.883 info: GuildWars2.GameLink[0]
      [5864] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:35:56.880 info: GuildWars2.GameLink[0]
      [5952] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:35:57.875 info: GuildWars2.GameLink[0]
      [6033] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:35:58.872 info: GuildWars2.GameLink[0]
      [6107] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:35:59.869 info: GuildWars2.GameLink[0]
      [6189] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:36:00.882 info: GuildWars2.GameLink[0]
      [6276] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:36:01.878 info: GuildWars2.GameLink[0]
      [6328] Invert Control, the Human Engineer (Mechanist) is looking at the map
18:36:07.877 info: GuildWars2.GameLink[0]
      [6356] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -165.02733
      Longitude : 72.124
      Elevation : 3.0515137
18:36:08.885 info: GuildWars2.GameLink[0]
      [6429] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -165.02733
      Longitude : 72.124
      Elevation : 3.0515137
18:36:09.883 info: GuildWars2.GameLink[0]
      [6487] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -169.45564
      Longitude : 68.11285
      Elevation : 2.6663609
18:36:10.874 info: GuildWars2.GameLink[0]
      [6564] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -174.86848
      Longitude : 60.60133
      Elevation : 2.1442716
18:36:11.882 info: GuildWars2.GameLink[0]
      [6658] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -181.90218
      Longitude : 54.47012
      Elevation : 1.3165135
18:36:12.874 info: GuildWars2.GameLink[0]
      [6767] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -188.92157
      Longitude : 48.604156
      Elevation : -0.5576468
18:36:13.873 info: GuildWars2.GameLink[0]
      [6877] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -197.05478
      Longitude : 44.036755
      Elevation : -0.67757004
18:36:14.880 info: GuildWars2.GameLink[0]
      [6952] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -205.50684
      Longitude : 40.087452
      Elevation : -0.6779618
18:36:15.879 info: GuildWars2.GameLink[0]
      [7016] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -215.0471
      Longitude : 36.672283
      Elevation : -0.68073654
18:36:16.883 info: GuildWars2.GameLink[0]
      [7091] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:17.880 info: GuildWars2.GameLink[0]
      [7178] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:18.869 info: GuildWars2.GameLink[0]
      [7263] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:19.878 info: GuildWars2.GameLink[0]
      [7344] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -234.5483
      Longitude : 29.191357
      Elevation : -0.68667835
18:36:20.871 info: GuildWars2.GameLink[0]
      [7440] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -242.35175
      Longitude : 24.066732
      Elevation : -0.66047573
18:36:21.881 info: GuildWars2.GameLink[0]
      [7535] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -250.22687
      Longitude : 19.07481
      Elevation : -0.67880875
18:36:22.880 info: GuildWars2.GameLink[0]
      [7633] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:23.875 info: GuildWars2.GameLink[0]
      [7733] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:24.879 info: GuildWars2.GameLink[0]
      [7835] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:25.874 info: GuildWars2.GameLink[0]
      [7937] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:26.882 info: GuildWars2.GameLink[0]
      [8021] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:27.887 info: GuildWars2.GameLink[0]
      [8100] Invert Control, the Human Engineer (Mechanist) is in combat
18:36:28.869 info: GuildWars2.GameLink[0]
      [8165] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -270.60995
      Longitude : 22.80896
      Elevation : -0.23545928
18:36:29.883 info: GuildWars2.GameLink[0]
      [8243] Invert Control, the Human Engineer (Mechanist) is on foot
      Map       : Queensdale (Public)
      Latitude  : -270.60995
      Longitude : 22.80896
      Elevation : -0.23545928
18:36:30.701 info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/
