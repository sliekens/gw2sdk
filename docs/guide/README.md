# Getting started

GW2SDK is a .NET code library for interacting with the Guild Wars 2 API and game client.

The Guild Wars 2 API is accessible over HTTPS. It provides information about the game, your account, PvP seasons, WvW matches and the in-game economy.

The game client on Windows provides realtime information about the player's movement in the world and the location and size of UI elements.

## Features

GW2SDK has the following entrypoints:

- `GuildWars2.Gw2Client` provides access to the API,
- `GuildWars2.GameLink` provides realtime information from the game client (Windows only)

## Authentication

Many features require an access token to retrieve account information. It is necessary to go to <https://account.arena.net/applications>, create a New Key, and store it somewhere safely in your application data (not in source control).

You can use the TokenProvider class to validate an access token that you received from someone else. You can also use TokenProvider to create a subtoken with a shorter time-to-live or with fewer privileges. This is useful if you wish to give someone else limited access to your account. For example: if you build a modular application that can be extended with 3rd-party plugins, you may ask the user for a full-access master token and then create a subtoken for each plugin, giving it only the permissions it needs and the time it needs.
