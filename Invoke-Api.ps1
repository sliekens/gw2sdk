#!/usr/bin/env pwsh
param(
    [string]$Endpoint = '/'
)

$json = dotnet user-secrets list --json --project $PSScriptRoot/tests/GuildWars2.Tests
$secrets = $json | % { $_ -replace '//(BEGIN|END)' } | ConvertFrom-Json

$baseUri = [Uri]'https://api.guildwars2.com/'
$requestUri = [Uri]::new($baseUri, $Endpoint.TrimStart('/'))
$requestOptions = @{
    Uri = $requestUri
    Method = 'Get'
    SkipHttpErrorCheck = $true
    Headers = @{
        'Authorization' = "Bearer $($secrets.ApiKey)"
        'X-Schema-Version' = '3'
    }
}

$response = Invoke-WebRequest @requestOptions

$response.Content

if ([int]$response.StatusCode -ge 400) {
    exit 1
}