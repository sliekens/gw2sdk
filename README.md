# GW2SDK
[![Continuous Integration](https://github.com/StevenLiekens/gw2sdk/workflows/Continuous%20Integration/badge.svg)][actions] [![codecov](https://codecov.io/gh/StevenLiekens/gw2sdk/branch/main/graph/badge.svg)][codecov] [![Twitter Follow](https://img.shields.io/twitter/follow/LiekensSteven?style=social)][twitter]

C# client SDK for interacting with the Guild Wars 2 API

## Quick navigation

- Code coverage: <https://codecov.io/gh/StevenLiekens/gw2sdk>
- API reference: <https://wiki.guildwars2.com/wiki/API:Main>

## Installation

GW2SDK is still under development. Preview packages will be published to nuget.org later.

You can install development packages from GitHub. These packages are updated on every commit to the main branch and versioned by date and time. (Format: 1.0.0-CI-YYYYMMDD-hhmmss)

First create a personal access token [here][tokens] with the _read:packages_ scope. Then run the following command.

``` sh
dotnet nuget add source https://nuget.pkg.github.com/stevenliekens/index.json --name sliekens --username <USERNAME> --password <TOKEN>
```

You must replace:

- USERNAME with the name of your user account on GitHub.
- TOKEN with your personal access token.

By default, your token is stored in encrypted format in your user directory.

- %userprofile%\.nuget\packages on windows
- ~/.nuget/packages on Mac/Linux

Encryption is not supported on every platform. If you get an error, try the command again with `--store-password-in-clear-text`.

Now you can install packages from [here][packages].

[//]:# (add links to the section below)
[actions]:https://github.com/StevenLiekens/gw2sdk/actions?query=workflow%3A%22Continuous+Integration%22
[codecov]:https://codecov.io/gh/StevenLiekens/gw2sdk
[twitter]:https://twitter.com/LiekensSteven
[tokens]:https://github.com/settings/tokens
[packages]:https://github.com/StevenLiekens/gw2sdk/packages
