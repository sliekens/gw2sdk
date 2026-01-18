# Basic Usage

## 🚀 Quick Start

The entry point is `Gw2Client`, which requires an `HttpClient`:

```csharp
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);
```

The client organizes APIs into logical groups. For example, trading post APIs are in `Gw2Client.Commerce`.

<details>
<summary>📚 Click to see all API groups</summary>

- `Gw2Client.Commerce`: Provides query methods for Black Lion Trading Company services.
- `Gw2Client.Exploration`: Provides query methods for maps and map features.
- `Gw2Client.Files`: Provides query methods to retrieve assets (icons) from the
  Guild Wars 2 API.
- `Gw2Client.Guilds`: Provides query methods for guilds (permissions, ranks, members,
  teams, bank, upgrades, logs) and guild emblems.
- `Gw2Client.Hero`: Provides query methods for APIs related to the player account
  or character.
- `Gw2Client.Hero.Account`: Provides query methods for a player account and characters
  that belong to the account.
- `Gw2Client.Hero.Achievements`: Provides query methods for achievements and titles
  in the game and achievement progress on the account.
- `Gw2Client.Hero.Bank`: Provides query methods for the account bank and material
  storage.
- `Gw2Client.Hero.Builds`: Provides query methods for build templates, skills,
  specializations, traits, legends (Revenant) and builds in the build storage on
  the account.
- `Gw2Client.Hero.Crafting`: Provides query methods for APIs related to crafting.
- `Gw2Client.Hero.Crafting.Disciplines`: Provides query methods for learned crafting
  disciplines.
- `Gw2Client.Hero.Crafting.Recipes`: Provides query methods for recipes, recipe
  search, recipes learned by characters and recipes unlocked account-wide.
- `Gw2Client.Hero.Crafting.Daily`: Provides query methods for items which can be
  crafted once per day.
- `Gw2Client.Hero.Emotes`: Provides query methods for emotes and unlocked emotes
  on the account.
- `Gw2Client.Hero.Equipment`: Provides query methods for APIs related to equipment
  and cosmetic items.
- `Gw2Client.Hero.Equipment.Dyes`: Provides query methods for dye colors and unlocked
  dyes.
- `Gw2Client.Hero.Equipment.Finishers`: Provides query methods for enemy finishers
  and unlocked finishers on the account.
- `Gw2Client.Hero.Equipment.Templates`: Provides query methods for items equipped
  by a character and legendary items on the account.
- `Gw2Client.Hero.Equipment.Gliders`: Provides query methods for glider skins and
  skins unlocked on the account.
- `Gw2Client.Hero.Equipment.JadeBots`: Provides query methods for jade bot skins
  and skins unlocked on the account.
- `Gw2Client.Hero.Equipment.MailCarriers`: Provides query methods for mail carriers
  and mail carriers unlocked on the account.
- `Gw2Client.Hero.Equipment.Miniatures`: Provides query methods for miniatures and
  miniatures unlocked on the account.
- `Gw2Client.Hero.Equipment.Mounts`: Provides query methods for mounts and mounts
  unlocked on the account.
- `Gw2Client.Hero.Equipment.Novelties`: Provides query methods for novelties and
  novelties unlocked on the account.
- `Gw2Client.Hero.Equipment.Outfits`: Provides query methods for outfits and outfits
  unlocked on the account.
- `Gw2Client.Hero.Equipment.Skiffs`: Provides query methods for skiffs and skiffs
  unlocked on the account.
- `Gw2Client.Hero.Equipment.Wardrobe`: Provides query methods for armor and weapon
  skins and skins unlocked on the account.
- `Gw2Client.Hero.Inventory`: Provides query methods for bags and the shared inventory.
- `Gw2Client.Hero.Masteries`: Provides query methods for masteries, mastery points
  and mastery progress on the account.
- `Gw2Client.Hero.Races`: Provides query method for playable races.
- `Gw2Client.Hero.StoryJournal`: Provides query methods for the story progress and
  the backstory of a character.
- `Gw2Client.Hero.Training`: Provides query methods for skill and specialization
  training progress of a character.
- `Gw2Client.Hero.Wallet`: Provides query methods for currencies in the game and
  in the account wallet.
- `Gw2Client.Items`: Provides query methods for items and item stats.
- `Gw2Client.Metadata`: Provides query methods for game metadata and API metadata.
- `Gw2Client.Pve`: Provides query methods for APIs related to open world gameplay
  (PvE).
- `Gw2Client.Pve.Dungeons`: Provides query methods for dungeons and completed dungeon
  paths.
- `Gw2Client.Pve.Home`: Provides query methods for home instances and homesteads.
- `Gw2Client.Pve.MapChests`: Provides query methods for daily map rewards.
- `Gw2Client.Pve.Pets`: Provides query methods for Ranger pets.
- `Gw2Client.Pve.Raids`: Provides query methods for raids and completed encounters.
- `Gw2Client.Pve.SuperAdventureBox`: Provides query methods for Super Adventure
  Box progress.
- `Gw2Client.Pve.WorldBosses`: Provides query methods for defeated world bosses.
- `Gw2Client.Pvp`: Provides query methods for PvP matches, seasons, rank, leaderboards,
  equipment and mist chanpions.
- `Gw2Client.Quaggans`: Provides query methods for images of Quaggans.
- `Gw2Client.Tokens`: Provides query methods for access token introspection and
  subtoken creation.
- `Gw2Client.Worlds`: Provides query methods for World (servers).
- `Gw2Client.Wvw`: Provides query methods for WvW matches, objectives, abilities,
  ranks, and upgrades.
- `Gw2Client.WizardsVault`: Provides query methods for the Wizard's Vault (daily,
  weekly and special objectives and Astral Rewards).

</details>

---

## 🔄 The Programming Model

Query methods return a tuple with two items:

| Item | Description |
|------|-------------|
| **Value** | The response data as a strongly-typed object |
| **MessageContext** | HTTP metadata (headers, caching info) |

```csharp
var (quaggans, context) = await gw2.Quaggans.GetQuaggans();
```

Don't need the metadata? Use the discard operator or `ValueOnly()`:

```csharp
// Option 1: Discard
var (quaggans, _) = await gw2.Quaggans.GetQuaggans();

// Option 2: Extension method
var quaggans = await gw2.Quaggans.GetQuaggans().ValueOnly();
```

---

## 🔗 Cross-Referencing Data

The API returns normalized data, so you often need multiple requests. Use `AsDictionary()` for easy lookups:

```csharp
// Create a map lookup by ID
var maps = await gw2.Exploration.GetMapSummaries()
    .AsDictionary(static map => map.Id)
    .ValueOnly();

// Quick access by ID
MapSummary queensdale = maps[15];
```

---

## 📝 Example: Trading Post Items

This example shows:
1. Using `GetItemPricesBulk` to fetch prices
2. Cross-referencing with `GetItemById` for item names

[!code-csharp[](~/samples/BasicUsage/Program.cs)]

Output

```text
Item           : Sealed Package of Snowballs
Highest buyer  : 99 copper
Lowest seller  : 1 silver, 70 copper
Bid-ask spread : 71 copper

Item           : Mighty Country Coat
Highest buyer  : 83 copper
Lowest seller  : 97 copper
Bid-ask spread : 14 copper

Item           : Mighty Country Coat
Highest buyer  : 31 copper
Lowest seller  : 93 copper
Bid-ask spread : 62 copper

Item           : Mighty Studded Coat
Highest buyer  : 28 copper
Lowest seller  : 46 copper
Bid-ask spread : 18 copper

Item           : Mighty Worn Chain Greaves
Highest buyer  : 35 copper
Lowest seller  : 76 copper
Bid-ask spread : 41 copper

Item           : Berserker's Sneakthief Mask of the Afflicted
Highest buyer  : 3 gold, 38 silver, 41 copper
Lowest seller  : 15 gold
Bid-ask spread : 11 gold, 61 silver, 59 copper

Item           : Berserker's Sneakthief Mask of Dwayna
Highest buyer  : 34 silver, 2 copper
Lowest seller  : 54 silver, 31 copper
Bid-ask spread : 20 silver, 29 copper

Item           : Mighty Worn Chain Greaves
Highest buyer  : 29 copper
Lowest seller  : 14 silver, 99 copper
Bid-ask spread : 14 silver, 70 copper

Item           : Berserker's Sneakthief Mask of Strength
Highest buyer  : 28 silver, 75 copper
Lowest seller  : 32 silver, 64 copper
Bid-ask spread : 3 silver, 89 copper

Item           : Berserker's Seer Coat of the Flame Legion
Highest buyer  : 4 gold, 51 silver, 13 copper
Lowest seller  : 29 gold, 85 silver, 11 copper
Bid-ask spread : 25 gold, 33 silver, 98 copper

```
