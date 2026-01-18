# Game Link

The game client exposes realtime data via MumbleLink, a shared memory protocol.

> [!NOTE]
> This is **not** a hack—it's an officially supported feature used by Mumble voice chat.

---

## 🎮 Available Data

### Player Information

| Data | Description |
|------|-------------|
| Identity | Character name, [race], [profession], [specialization] |
| [World] ID | Home server |
| Game Mode | PvE, PvP, or WvW (with team color) |
| Status | In combat, squad leader, current mount |
| Location | Map ID, coordinates, orientation |

### Camera & UI

| Data | Description |
|------|-------------|
| Camera | Position, orientation, field of view |
| Compass | Size, rotation, docked position |
| Map | Open/closed state, zoom level |
| UI | Size setting, textbox focus state |

### Metadata

| Data | Description |
|------|-------------|
| Server | IP address, shard ID, build number |
| Process | Game client PID, focus state |

---

## 🚀 Basic Usage

`GameLink` implements `IObservable<GameTick>`, so use the publisher-subscriber pattern.

> [!TIP]
> Install [System.Reactive][Rx] for a better experience with observables.

```csharp
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

---

## 🔀 Multiple Game Clients

The default memory-mapped file is `MumbleLink`. For multiple instances, use a custom name:

```cmd
gw2-64.exe -mumble OtherLink
```

```csharp
var link = GameLink.Open(name: "OtherLink");
```

> [!TIP]
> Use `-mumble 0` to disable MumbleLink entirely.

---

## 📝 Complete Example

A full example is available in the [samples directory](https://github.com/sliekens/gw2sdk/tree/main/samples/Mumble).

**Features demonstrated:**
- Platform compatibility check
- Rx subscription
- Cross-referencing with API data
- Player state detection (combat, typing, map open)

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
⭑⭑⭑ Technical Information ⭑⭑⭑
Build ID                       : 172,493
Process ID                     : 43,212
World ID                       : 2005 (Ring of Fire)
World population               : High
World region                   : Europe
World language                 : English
Shard ID                       : 000007D5
Instance                       : 00000000
Server IP                      : 3.77.138.137
Tick                           : 667,962

⭑⭑⭑ User Interface ⭑⭑⭑
UI size                        : Normal
Game has focus                 : ❌
Textbox has focus              : ❌

⭑⭑⭑ Character Information ⭑⭑⭑
Name                           : Invert Control
Race                           : Human
Profession                     : Engineer
Specialization                 : Mechanist (ID: 70)
Squad leader                   : ❌
In combat                      : ❌
Current mount                  : Griffon

⭑⭑⭑ Competitive Games ⭑⭑⭑
Competitive game type          : ❌
Team color                     : none

⭑⭑⭑ World Map Coordinates ⭑⭑⭑
Compass location               : bottom-right
Compass dimensions             : Width = 362 Height = 361
Compass orientation            : static
World map is open              : ❌
Compass/world map scale        : 1.000
Map center                     : X = 35,973.246 Y = 33,210.469
Player position                : X = 35,973.246 Y = 33,210.469

⭑⭑⭑ Positional Information ⭑⭑⭑
Map ID                         : 1121 (Gilded Hollow, Instance)
Avatar position                : X =      3.198 Y =     61.329 Z =    -35.641
Avatar orientation             : X =      0.006 Y =      0.000 Z =      1.000
Avatar top                     : X =      0.000 Y =      0.000 Z =      0.000

⭑⭑⭑ Camera Information ⭑⭑⭑
Field of fiew                  : 1.065
Camera coordinates             : X =      3.290 Y =     68.311 Z =    -50.490
Camera orientation             : X =     -0.006 Y =     -0.225 Z =      0.974
Camera top                     : X =      0.000 Y =      0.000 Z =      0.000
```

[race]:https://wiki.guildwars2.com/wiki/Playable_races
[profession]:https://wiki.guildwars2.com/wiki/Profession
[specialization]:https://wiki.guildwars2.com/wiki/Specialization
[world]:https://wiki.guildwars2.com/wiki/World
[Rx]:https://www.nuget.org/packages/System.Reactive/
