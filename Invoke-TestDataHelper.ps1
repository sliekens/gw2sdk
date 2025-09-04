#!/usr/bin/env pwsh
dotnet run -c Release --project .\src\GW2SDK.TestDataHelper\GW2SDK.TestDataHelper.csproj -- "$PSScriptRoot/tests/GW2SDK.Tests/Data"