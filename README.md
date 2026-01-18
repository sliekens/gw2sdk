<div align="center">

# GW2SDK

[![NuGet][nuget-v-badge]][nuget]
[![NuGet pre-release][nuget-vpre-badge]][nuget-pre]
[![codecov][codecov-badge]][codecov]
[![Continuous Integration][ci-badge]][actions]

*A .NET library for the Guild Wars 2 API and game client.*

[Introduction][introduction] · [Installation][installation] · [Usage][usage] · [API Docs][api-docs] · [API Keys][applications]

</div>

---

## ✨ Features

| | |
|---|---|
| ⚡ **High Performance** | Low-allocation JSON with System.Text.Json |
| 🔄 **Async First** | Stream data from both the API and game client |
| 🎯 **Type Safe** | Full nullability annotations for C# 8.0+ |
| 🧩 **Pure C#** | No native dependencies |
| 🌍 **Cross-Platform** | Runs anywhere .NET runs |
| 🚀 **AOT Ready** | Supports ahead-of-time compilation |
| 📜 **MIT License** | Free and open source |

## ⚡ Quick Start

```csharp
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// Get today's daily achievements
var dailies = await gw2.Hero.Achievements.GetDailyAchievements();

// Get trading post prices
var prices = await gw2.Commerce.GetItemPriceById(19721); // Glob of Ectoplasm
Console.WriteLine($"Buy: {prices.Value.BestBid}, Sell: {prices.Value.BestAsk}");
```

## 📦 Platform Support

GW2SDK targets .NET Standard 2.0, supporting modern and legacy runtimes:

| Platform | Version |
|----------|---------|
| .NET | 8.0+ |
| .NET Framework | 4.6.2+ |
| Mono | 5.4+ |
| Xamarin.iOS | 10.14+ |
| Xamarin.Mac | 3.8+ |
| Xamarin.Android | 8.0+ |
| UWP | 10.0.16299+ |
| Unity | 2018.1+ |

> **Note:** Game client integration (MumbleLink) requires Windows. Wine compatibility is untested.

## 🤝 Contributing

Check the [wiki] for contributor guidelines and the [documentation] site for user guides.

<div align="center">

[![Open in GitHub Codespaces][codespaces-badge]](https://codespaces.new/sliekens/gw2sdk)

</div>

**Other ways to contribute:**
- 🐛 [Report a bug][new-issue] or 💡 [request a feature][new-issue]
- 💬 [Start a discussion][new-discussion] for questions or feedback
- ✏️ [Quick edit][github.dev] typos directly in your browser

<details>
<summary>More cloud development options</summary>

[![Open in Codeanywhere][codeanywhere-badge]](https://app.codeanywhere.com/#https://github.com/sliekens/gw2sdk)
[![Open in Gitpod][gitpod-badge]](https://gitpod.io/#https://github.com/sliekens/gw2sdk)

> [!TIP]
> GitHub Codespaces offers ~60 free hours/month.

</details>

## 📚 Resources

| Resource | Description |
|----------|-------------|
| [Codecov][codecov] | Test coverage reports |
| [GW2 Wiki: API][api] | Official API documentation |
| [GW2 Wiki: Chat Links][chatlinks] | Chat link format specification |
| [GW2 Wiki: MumbleLink][mumblelink] | MumbleLink structure format |
| [API Explorer](https://api.guildwars2.com/v2) | Browse available endpoints |
| [API Schema](https://api.guildwars2.com/v2.json?v=latest) | Machine-readable endpoint data |

---

<div align="center">

Made with ❤️ for the Guild Wars 2 community

</div>

[//]:# (add links to the section below)
[actions]:https://github.com/sliekens/gw2sdk/actions?query=workflow%3A%22Continuous+Integration%22
[api]:https://wiki.guildwars2.com/wiki/API:Main
[chatlinks]:https://wiki.guildwars2.com/wiki/Chat_link_format
[ci-badge]:https://img.shields.io/github/actions/workflow/status/sliekens/gw2sdk/ci.yml?style=for-the-badge&logo=github&label=CI
[codecov-badge]:https://img.shields.io/codecov/c/github/sliekens/gw2sdk?style=for-the-badge&logo=codecov&logoColor=white
[codecov]:https://codecov.io/gh/sliekens/gw2sdk
[gitpod-badge]:https://gitpod.io/button/open-in-gitpod.svg
[codeanywhere-badge]:https://codeanywhere.com/img/open-in-codeanywhere-btn.svg
[codespaces-badge]:https://github.com/codespaces/badge.svg
[free]:https://docs.github.com/en/billing/managing-billing-for-github-codespaces/about-billing-for-github-codespaces#monthly-included-storage-and-core-hours-for-personal-accounts
[github.dev]:https://github.dev/sliekens/gw2sdk
[installation]:https://sliekens.github.io/gw2sdk/guide/overview/installation
[introduction]:https://sliekens.github.io/gw2sdk/guide/overview/introduction
[api-docs]:https://sliekens.github.io/gw2sdk/api/GuildWars2.html
[mumblelink]:https://wiki.guildwars2.com/wiki/API:MumbleLink
[new-discussion]:https://github.com/sliekens/gw2sdk/discussions/new/choose
[new-issue]:https://github.com/sliekens/gw2sdk/issues/new
[nuget-v-badge]:https://img.shields.io/nuget/v/GW2SDK?style=for-the-badge&logo=nuget&logoColor=white
[nuget-vpre-badge]:https://img.shields.io/nuget/vpre/GW2SDK?style=for-the-badge&logo=nuget&logoColor=white&label=nuget%20pre
[nuget]:https://www.nuget.org/packages/GW2SDK/
[nuget-pre]:https://www.nuget.org/packages/GW2SDK/absoluteLatest 
[usage]:https://sliekens.github.io/gw2sdk/guide/getting-started/usage
[documentation]:https://sliekens.github.io/gw2sdk/
[wiki]:https://github.com/sliekens/gw2sdk/wiki
[applications]:https://account.arena.net/applications
