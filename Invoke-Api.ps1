$endpoint = $args[0]

$secrets = dotnet user-secrets list --json --project $PSScriptRoot/GW2SDK.Tests | ConvertFrom-Json
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