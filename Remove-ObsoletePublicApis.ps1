#!/usr/bin/env pwsh
<#
.SYNOPSIS
Removes obsolete public API entries from PublicAPI.Shipped.txt based on RS0017 warnings.

.DESCRIPTION
This script runs dotnet build, parses RS0017 warnings from the build output,
and removes the corresponding lines from PublicAPI.Shipped.txt files.

.EXAMPLE
.\Remove-ObsoletePublicApis.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

# Run dotnet build with --no-incremental to get RS0017 warnings
Write-Verbose "Running dotnet build --no-incremental..."
$buildOutput = & dotnet build --no-incremental 2>&1
$buildOutputString = $buildOutput -join "`n"

# Parse RS0017 warnings
# RS0017 format: "Symbol 'X.Y.Z' is part of the declared API, but is either not public or could not be found"
$rs0017Pattern = "warning RS0017:.*?Symbol '([^']+)'"
$apisToRemove = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)

foreach ($match in [System.Text.RegularExpressions.Regex]::Matches($buildOutputString, $rs0017Pattern, [System.Text.RegularExpressions.RegexOptions]::Singleline)) {
    $apiName = $match.Groups[1].Value
    [void]$apisToRemove.Add($apiName)
    Write-Verbose "Found RS0017 warning for: $apiName"
}

if ($apisToRemove.Count -eq 0) {
    Write-Host "No RS0017 warnings found. No changes needed."
    exit 0
}

Write-Host "Found $($apisToRemove.Count) RS0017 warning(s). Processing PublicAPI.Shipped.txt files..."

# Find all PublicAPI.Shipped.txt files
$publicApiFiles = Get-ChildItem -Path . -Filter "PublicAPI.Shipped.txt" -Recurse

foreach ($file in $publicApiFiles) {
    Write-Verbose "Processing: $($file.FullName)"
    
    $content = Get-Content -Path $file.FullName -Encoding UTF8
    $originalLineCount = $content.Count
    
    # Filter out lines matching the APIs to remove
    $newContent = $content | Where-Object {
        $line = $_
        $shouldKeep = $true
        
        foreach ($api in $apisToRemove) {
            # Check if the line starts with the API name or is an abstract member of the API
            if ($line -eq $api -or $line.StartsWith("$api.") -or $line.StartsWith("abstract $api.")) {
                $shouldKeep = $false
                Write-Host "  Removing: $line"
                break
            }
        }
        
        return $shouldKeep
    }
    
    $newLineCount = if ($newContent -is [array]) { $newContent.Count } else { if ($newContent) { 1 } else { 0 } }
    $removedCount = $originalLineCount - $newLineCount
    
    if ($removedCount -gt 0) {
        Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8
        Write-Host "  Removed $removedCount line(s) from $($file.Name)"
    }
}

Write-Host "Done!"
