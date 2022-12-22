$endpoint = $args[0]
$script = New-TemporaryFile

Set-Content -Path $script -Value @(
    "connect https://api.guildwars2.com",
    "set header X-Schema-Version 3",
    "get $endpoint"
)

dotnet httprepl run $script

Remove-Item $script