# GW2SDK [![Continuous Integration][ci-badge]][actions] [![codecov][codecov-badge]][codecov] [![Twitter Follow][twitter-badge]][twitter]

A .NET code library for interacting with the Guild Wars 2 API and game client.

## Quick navigation

- User manual: <https://sliekens.github.io/gw2sdk/>
- Code coverage: [https://codecov.io/gh/sliekens/gw2sdk][codecov]
- API reference: <https://wiki.guildwars2.com/wiki/API:Main>

## Installation

GW2SDK is still under development. Preview packages will be published to nuget.org later.

You can install development packages from GitHub from [here][packages]. These packages are updated on every commit to the main branch and versioned by date and time. (Format: 1.0.0-CI-YYYYMMDD-hhmmss)

First create a personal access token [here][tokens] with the _read:packages_ scope. Then run the following command.

``` sh
dotnet nuget add source https://nuget.pkg.github.com/sliekens/index.json --name sliekens --username <USERNAME> --password <TOKEN>
```

Replace:

- USERNAME with the name of your user account on GitHub.
- TOKEN with your personal access token.

By default, your token is stored in encrypted format in your user directory.

- Windows: `%appdata%\NuGet\NuGet.Config`
- Mac/Linux: `~/.config/NuGet/NuGet.Config` or `~/.nuget/NuGet/NuGet.Config` (varies by OS distribution)

Encryption is not supported on every platform. If you get an error, try the command again with `--store-password-in-clear-text`.

To stop using development packages, you can remove the NuGet source from your machine. This will remove the source and your token.
``` sh
dotnet nuget remove source sliekens
```

[//]:# (add links to the section below)
[ci-badge]:https://github.com/sliekens/gw2sdk/workflows/Continuous%20Integration/badge.svg
[codecov-badge]:https://codecov.io/gh/sliekens/gw2sdk/branch/main/graph/badge.svg?token=2ZTDBRWWLR
[twitter-badge]:https://img.shields.io/twitter/follow/LiekensSteven?style=social
[actions]:https://github.com/sliekens/gw2sdk/actions?query=workflow%3A%22Continuous+Integration%22
[codecov]:https://codecov.io/gh/sliekens/gw2sdk
[twitter]:https://twitter.com/LiekensSteven
[tokens]:https://github.com/settings/tokens
[packages]:https://github.com/sliekens/gw2sdk/packages