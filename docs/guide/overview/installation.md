# Installation

## 📦 From NuGet (Recommended)

Install the stable release from [NuGet][nuget]:

```sh
dotnet add package GW2SDK
```

---

## 🧪 Development Packages

Preview builds are available from [GitHub Packages][packages], updated on every commit to `main`.

> Version format: `1.0.0-preview.0.123` (commits since last release)

### Setup

1. Create a [personal access token][tokens] with the `read:packages` scope
2. Add the package source:

```sh
dotnet nuget add source https://nuget.pkg.github.com/sliekens/index.json \
    --name sliekens \
    --username <USERNAME> --password <TOKEN>
```

### Token Storage

Your token is encrypted and stored in:

| Platform | Location |
|----------|----------|
| Windows | `%appdata%\NuGet\NuGet.Config` |
| Mac/Linux | `~/.config/NuGet/NuGet.Config` or `~/.nuget/NuGet/NuGet.Config` |

> [!TIP]
> If encryption fails on your platform, add `--store-password-in-clear-text` to the command.

---

## 🗑️ Removing Development Packages

To stop using preview builds:

```sh
dotnet nuget remove source sliekens
```

[tokens]:https://github.com/settings/tokens
[packages]:https://github.com/sliekens/gw2sdk/packages
[nuget]:https://www.nuget.org/packages/GW2SDK/
