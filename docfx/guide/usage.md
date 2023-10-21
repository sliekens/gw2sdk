# Basic usage

The entry point for API access is `GuildWars2.Gw2Client`. From there you can use IntelliSense to discover resources.

The `Gw2Client` has a single dependency on `System.Net.Http.HttpClient` which you must provide from your application code.

A very simple console app might look like this.

@[code cs{4-5,9,14,20}](../../samples/BasicUsage/Program.cs)

Output

``` text
The current game version is 152,325

282 gems cost 100 gold
1 gem costs 35 silver, 46 copper

Raid: forsaken_thicket
  W1: spirit_vale
    Encounter 1: vale_guardian (Boss)
    Encounter 2: spirit_woods (Checkpoint)
    Encounter 3: gorseval (Boss)
    Encounter 4: sabetha (Boss)
  W2: salvation_pass
    Encounter 1: slothasor (Boss)
    Encounter 2: bandit_trio (Boss)
    Encounter 3: matthias (Boss)
  W3: stronghold_of_the_faithful
    Encounter 1: escort (Boss)
    Encounter 2: keep_construct (Boss)
    Encounter 3: twisted_castle (Checkpoint)
    Encounter 4: xera (Boss)

Raid: bastion_of_the_penitent
  W1: bastion_of_the_penitent
    Encounter 1: cairn (Boss)
    Encounter 2: mursaat_overseer (Boss)
    Encounter 3: samarog (Boss)
    Encounter 4: deimos (Boss)

Raid: hall_of_chains
  W1: hall_of_chains
    Encounter 1: soulless_horror (Boss)
    Encounter 2: river_of_souls (Boss)
    Encounter 3: statues_of_grenth (Boss)
    Encounter 4: voice_in_the_void (Boss)

Raid: mythwright_gambit
  W1: mythwright_gambit
    Encounter 1: conjured_amalgamate (Boss)
    Encounter 2: twin_largos (Boss)
    Encounter 3: qadim (Boss)

Raid: the_key_of_ahdashim
  W1: the_key_of_ahdashim
    Encounter 1: gate (Checkpoint)
    Encounter 2: adina (Boss)
    Encounter 3: sabir (Boss)
    Encounter 4: qadim_the_peerless (Boss)
```