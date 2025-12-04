#!/usr/bin/env pwsh
dotnet run -c Release --project src/GuildWars2.TestDataHelper/GuildWars2.TestDataHelper.csproj -- "$PSScriptRoot/tests/GuildWars2.Tests/Data"