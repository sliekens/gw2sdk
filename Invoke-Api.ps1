#!/usr/bin/env pwsh
$endpoint = $args[0]

$json = dotnet user-secrets list --json --project $PSScriptRoot/tests/GuildWars2.Tests
$secrets = $json | % { $_ -replace '//(BEGIN|END)' } | ConvertFrom-Json
$script = New-TemporaryFile

Set-Content -Path $script -Value @(
    "connect https://api.guildwars2.com",
    # "echo on"
    "set header X-Schema-Version 3",
    "set header Authorization `"Bearer $($secrets.ApiKey)`""
    "get $endpoint"
)

dotnet httprepl run $script

Remove-Item $script