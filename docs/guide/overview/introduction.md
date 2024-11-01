# Introduction

GW2SDK is a .NET code library for interacting with the Guild Wars 2 API and game
client.

The Guild Wars 2 API is accessible over HTTPS. It provides information about the
game, your account, PvP seasons, WvW matches and the in-game economy.

The game client on Windows provides realtime information about the player's movement
in the world and the location and size of UI elements.

## Features

The SDK provides an interface to the Guild Wars 2 API and game client. It is designed
to be easy to use and to provide a high level of performance.

It provides the following features and benefits:

- Asynchronous methods to query the API
- Asynchronous methods to stream data from the game client
- High performance, low-allocation JSON conversions with System.Text.Json
- Type safety and nullability annotations for C# 8.0+
- Pure C# implementation, no native dependencies
- Cross-platform support
- AOT compilation support
- Free and open source under the MIT license

The package has the following entrypoint classes:

- `GuildWars2.Gw2Client` provides access to the API,
- `GuildWars2.GameLink` provides realtime information from the game client
  (Windows only)

## Platform support

GW2SDK is compiled for .NET Standard 2.0 so it supports a wide range of platforms:

- .NET Core 2.0+
- .NET Framework 4.6.2+
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Mac 3.8+
- Xamarin.Android 8.0+
- Universal Windows Platform 10.0.16299+
- Unity 2018.1+

Retrieving information from the game client is only supported on Windows due to
the use of named memory-mapped files. It might work in Wine, but it has not been
tested.
