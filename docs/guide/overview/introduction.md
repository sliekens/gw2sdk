# Introduction

GW2SDK is a .NET library for interacting with the **Guild Wars 2 API** and **game client**.

| Data Source | Description |
|-------------|-------------|
| 🌐 **Web API** | Account info, PvP seasons, WvW matches, trading post, and more |
| 🎮 **Game Client** | Realtime player position, UI state, and camera data (Windows) |

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

---

## 🚀 Entry Points

| Class | Purpose |
|-------|----------|
| `Gw2Client` | Query the Guild Wars 2 web API |
| `GameLink` | Stream realtime data from the game client (Windows only) |

---

## 📦 Platform Support

GW2SDK targets **.NET Standard 2.0** for broad compatibility:

| Platform | Version |
|----------|--------|
| .NET | 8.0+ |
| .NET Framework | 4.6.2+ |
| Mono | 5.4+ |
| Xamarin.iOS | 10.14+ |
| Xamarin.Mac | 3.8+ |
| Xamarin.Android | 8.0+ |
| UWP | 10.0.16299+ |
| Unity | 2018.1+ |

> [!NOTE]
> Game client integration (MumbleLink) requires Windows. Wine compatibility is untested.
