# Getting started

GW2SDK is a .NET code library for interacting with the Guild Wars 2 API and game client.

The Guild Wars 2 API is accessible over HTTPS. It provides information about the game, your account, PvP seasons, WvW matches and the in-game economy.

The game client also provides information about the server IP address, the player's position and movement on the current map, the player's camera and field of view, the location and size of the in-game compass, whether the player is using a commander tag, the player's color in competitive games, whether the player is in combat, whether the player is using a mount, the player's race, profession and specialization.

## Features

GW2SDK provides access to both the API and the game client from the following entrypoints:

- Gw2Client
- GameLink (Windows only)

Additionally you can validate access tokens and create subtokens using the following entrypoint:

- TokenProvider

All of these entrypoint classes are inside the root `GuildWars2` namespace.

## Authentication

Many features require an access token to retrieve account information. It is necessary to go to <https://account.arena.net/applications>, create a New Key, and store it somewhere safely in your application data (not in source control).

You can use the TokenProvider class to validate an access token that you received from someone else. You can also use TokenProvider to create a subtoken with a shorter time-to-live or with fewer privileges. This is useful if you wish to give someone else limited access to your account. For example: if you build a modular application that can be extended with 3rd-party plugins, you may ask the user for a full-access master token and then create a subtoken for each plugin, giving it only the permissions it needs and limit that access to the time it needs to run.