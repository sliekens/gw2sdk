#!/bin/env pwsh
param(
    [switch]$ApiCompatGenerateSuppressionFile
)

$packArgs = @(
    'src/GW2SDK/GW2SDK.csproj',
    '-c',
    'Release'
)

if ($ApiCompatGenerateSuppressionFile) {
    $packArgs += '/p:ApiCompatGenerateSuppressionFile=true'
}

dotnet pack @packArgs