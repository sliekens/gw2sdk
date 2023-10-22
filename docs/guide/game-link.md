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

The entry point for accessing this information is `GuildWars2.GameLink`. This class implements `IObservable` so you should be familiar with the publisher-subscriber model. I recommend to use the Reactive Extensions ([System.Reactive][Rx]) library to subscribe to the GameLink. Rx is a powerful library that makes it easy to work with asynchronous data streams.

To use the GameLink, pass an observer to `GameLink.Subscribe()`. An observer is a class that implements `IObserver<GameTick>`, or an `Action<GameTick>` when using Rx. The GameLink will then start pushing GameTicks to the observer (with configurable intervals). Each GameTick corresponds to a single update from the game client and it's usually updated on every frame. You can use `GameLink.Open(TimeSpan)` with a refresh interval of your choice if you need less frequent updates.

The game client (Gw2-64.exe) must be running for the GameLink to work.

Technically, it reads from a memory-mapped file which the game client writes to. The game client writes its state to this file on every frame. The GameLink reads from this file on a timer. The GameLink is not a hack or a cheat. It's a supported feature of the game client.

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

Here is an example observer that prints each update to the console. The link provides map IDs and specialization IDs which refer to `/v2/maps` and `/v2/specializations`. The example uses the `Gw2Client` to query the API for the map and specialization names.

[!code-csharp[](../../samples/Mumble/GameReporter.cs)]

Example output:

(Some lines were removed for brevity. I recommend to run this sample yourself to see the output in realtime.)

``` text
18:01:43.103 info: Microsoft.Hosting.Lifetime[0] Application started. Press Ctrl+C to shut down.
18:01:43.148 info: Microsoft.Hosting.Lifetime[0] Hosting environment: Production
18:01:43.148 info: Microsoft.Hosting.Lifetime[0] Content root path: C:\Users\Steven\source\repos\GW2SDK
18:01:43.138 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100] Start processing HTTP request GET https://api.guildwars2.com/v2/maps?ids=all&v=2022-03-23T19%3A00%3A00.000Z
18:01:43.149 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100] Sending HTTP request GET https://api.guildwars2.com/v2/maps?ids=all&v=2022-03-23T19%3A00%3A00.000Z
18:01:43.796 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101] Received HTTP response headers after 629.9712ms - 200
18:01:43.797 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101] End processing HTTP request after 676.5506ms - 200
18:01:43.912 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[100] Start processing HTTP request GET https://api.guildwars2.com/v2/specializations?ids=all&v=2022-03-23T19%3A00%3A00.000Z
18:01:43.912 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[100] Sending HTTP request GET https://api.guildwars2.com/v2/specializations?ids=all&v=2022-03-23T19%3A00%3A00.000Z
18:01:45.323 info: System.Net.Http.HttpClient.Gw2Client.ClientHandler[101] Received HTTP response headers after 1410.9161ms - 200
18:01:45.323 info: System.Net.Http.HttpClient.Gw2Client.LogicalHandler[101] End processing HTTP request after 1411.1086ms - 200
18:01:45.358 info: Mumble.GameReporter[0] [1407125] Invert Control, the Human Engineer (Mechanist) is afk
18:01:45.368 info: Mumble.GameReporter[0] [1407127] Invert Control, the Human Engineer (Mechanist) is afk
18:01:45.399 info: Mumble.GameReporter[0] [1407128] Invert Control, the Human Engineer (Mechanist) is afk
18:01:47.418 info: Mumble.GameReporter[0] [1407236] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.83417, Longitude = 526.19763, Elevation = 2.2133152 }
18:01:47.449 info: Mumble.GameReporter[0] [1407237] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.8286, Longitude = 526.5725, Elevation = 2.2133152 }
18:01:47.464 info: Mumble.GameReporter[0] [1407238] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.8286, Longitude = 526.5725, Elevation = 2.2133152 }
18:01:47.483 info: Mumble.GameReporter[0] [1407239] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.83, Longitude = 526.94586, Elevation = 2.187867 }
18:01:47.508 info: Mumble.GameReporter[0] [1407240] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.83, Longitude = 526.94586, Elevation = 2.187867 }
18:01:47.524 info: Mumble.GameReporter[0] [1407241] Invert Control, the Human Engineer (Mechanist) is on foot in Arborstone (PublicMini), Position: { Latitude = -811.8314, Longitude = 527.3192, Elevation = 2.1878686 }
18:01:52.932 info: Mumble.GameReporter[0] [1407565] Invert Control, the Human Engineer (Mechanist) is afk
18:01:52.962 info: Mumble.GameReporter[0] [1407566] Invert Control, the Human Engineer (Mechanist) is afk
18:01:52.967 info: Microsoft.Hosting.Lifetime[0] Application is shutting down...
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/