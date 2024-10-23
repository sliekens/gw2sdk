# GW2SDK

[![NuGet][nuget-vpre-badge]][nuget]
[![codecov][codecov-badge]][codecov]
[![Continuous Integration][ci-badge]][actions]

A .NET code library for interacting with the Guild Wars 2 API and game client.

## Quick navigation

- [Introduction] to features in the SDK
- [Installation] instructions for NuGet packages
- [Basic usage][usage] example for working with the SDK
- [API documentation][api-docs] for the SDK
- [API key management][applications] for the Guild Wars 2 API

To give feedback:

- [GitHub Issues][new-issue] for bug reports and feature requests
- [GitHub Discussions][new-discussion] for other feedback or questions

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

## Contributing

You are welcome to create an [issue][new-issue] if you find something is missing
or broken, or a [discussion][new-discussion] for other feedback, questions or ideas.

You are also welcome to propose changes directly with a pull request.

- Small changes can be made with the free [github.dev] editor.
- A Dev Container definition is provided for larger changes.

The [wiki] contains information for contributors. The _docs_ directory contains
user-facing articles which are used to build the [documentation] site.

[![Open in GitHub Codespaces][codespaces-badge]](https://codespaces.new/sliekens/gw2sdk)

(Light usage of Codespaces is [free]*, up to around 60 hours per month if you choose
the lightest machine and don't exceed 15GB disk usage. This codespace weighs around
2.84GB.)

## Additional resources

- [Codecov][codecov] contains test coverage reports
- [Guild Wars 2 wiki: API][api] contains API endpoint documentation
- [Guild Wars 2 wiki: chat link format][chatlinks] contains the format of chat links
- <https://api.guildwars2.com/v2> contains an overview of API endpoints
- <https://api.guildwars2.com/v2.json?v=latest> contains API endpoints, schema versions
  and changelog in machine-readable format

[//]:# (add links to the section below)
[actions]:https://github.com/sliekens/gw2sdk/actions?query=workflow%3A%22Continuous+Integration%22
[api]:https://wiki.guildwars2.com/wiki/API:Main
[chatlinks]:https://wiki.guildwars2.com/wiki/Chat_link_format
[ci-badge]:https://github.com/sliekens/gw2sdk/actions/workflows/ci.yml/badge.svg
[codecov-badge]:https://codecov.io/gh/sliekens/gw2sdk/branch/main/graph/badge.svg?token=2ZTDBRWWLR
[codecov]:https://codecov.io/gh/sliekens/gw2sdk
[codespaces-badge]:https://github.com/codespaces/badge.svg
[free]:https://docs.github.com/en/billing/managing-billing-for-github-codespaces/about-billing-for-github-codespaces#monthly-included-storage-and-core-hours-for-personal-accounts
[github.dev]:https://github.dev/sliekens/gw2sdk
[installation]:https://sliekens.github.io/gw2sdk/guide/overview/installation
[introduction]:https://sliekens.github.io/gw2sdk/guide/overview/introduction
[api-docs]:https://sliekens.github.io/gw2sdk/api/GuildWars2.html
[new-discussion]:https://github.com/sliekens/gw2sdk/discussions/new/choose
[new-issue]:https://github.com/sliekens/gw2sdk/issues/new
[nuget-vpre-badge]:https://img.shields.io/nuget/vpre/GW2SDK
[nuget]:https://www.nuget.org/packages/GW2SDK/
[usage]:https://sliekens.github.io/gw2sdk/guide/getting-started/usage
[documentation]:https://sliekens.github.io/gw2sdk/
[wiki]:https://github.com/sliekens/gw2sdk/wiki
[applications]:https://account.arena.net/applications
