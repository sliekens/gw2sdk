#!/usr/bin/env pwsh
dotnet run -c Release --project .\GW2SDK.TestDataHelper\GW2SDK.TestDataHelper.csproj -- "$PSScriptRoot/GW2SDK.Tests/Data"