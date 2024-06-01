# Installation

The recommended way to install the Guild Wars 2 SDK is to use the [NuGet package][nuget]. You can install the package from the command line using the .NET CLI.

``` sh
dotnet add package GW2SDK
```

## Development packages

You can install development packages from GitHub from [here][packages]. These packages are updated on every commit to the main branch and versioned by number of git commits since the last release. (Format: 1.0.0-preview.0.123)

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

## Uninstalling development packages

To stop using development packages, you can remove the NuGet source from your machine. This will remove the source and your token.

``` sh
dotnet nuget remove source sliekens
```

[tokens]:https://github.com/settings/tokens
[packages]:https://github.com/sliekens/gw2sdk/packages
[nuget]:https://www.nuget.org/packages/GW2SDK/