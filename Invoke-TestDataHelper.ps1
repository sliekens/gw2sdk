#!/usr/bin/env pwsh
dotnet run -c Release --project .\GW2SDK.TestDataHelper\GW2SDK.TestDataHelper.csproj --out-dir:"$PSScriptRoot/GW2SDK.Tests/Data"