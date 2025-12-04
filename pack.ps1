#!/bin/env pwsh
param(
    [switch]$ApiCompatGenerateSuppressionFile
)

$packArgs = @(
    'src/GuildWars2/GuildWars2.csproj',
    '-c',
    'Release'
)

if ($ApiCompatGenerateSuppressionFile) {
    $packArgs += '/p:ApiCompatGenerateSuppressionFile=true'
}

dotnet pack @packArgs