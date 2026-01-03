#!/usr/bin/env pwsh
<#
.SYNOPSIS
Adds missing public API entries to PublicAPI.Unshipped.txt files based on RS0016 warnings.

.DESCRIPTION
This script runs dotnet build, parses RS0016 warnings from the build output,
and adds the corresponding lines to the appropriate PublicAPI.Unshipped.txt files
based on the target framework specified in the warning.

.EXAMPLE
.\Fix-RS0016.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

# Load config to map TFM to Unshipped file paths
# Resolve config path relative to this script's location
$scriptDir = Split-Path -Parent $PSCommandPath
$configPath = Join-Path $scriptDir "config.json"
if (-not (Test-Path $configPath)) {
    Write-Error "Configuration file not found: $configPath"
    exit 1
}

$config = Get-Content $configPath -Raw | ConvertFrom-Json
$tfmToUnshippedPath = @{}

foreach ($tfm in $config.TargetFrameworks.PSObject.Properties.Name) {
    $unshippedPath = $config.TargetFrameworks.$tfm.PublicAPI.UnshippedFilePath
    $tfmToUnshippedPath[$tfm] = $unshippedPath
    Write-Verbose "TFM '$tfm' -> '$unshippedPath'"
}

# Run dotnet build with --no-incremental to get RS0016 warnings
Write-Verbose "Running dotnet build --no-incremental..."
$buildOutput = & dotnet build --no-incremental 2>&1
$buildOutputString = $buildOutput -join "`n"

# Parse RS0016 warnings
# RS0016 format: "/path/to/file.cs(line,col): warning RS0016: Symbol 'X.Y.Z' is not part of the declared public API ... [/path/to/project.csproj::TargetFramework=TFM]"
# Extract symbol name and target framework
$rs0016Pattern = "warning RS0016:.*?Symbol '([^']+)'.*?\[.*?TargetFramework=([^\]]+)\]"
$symbolsByTfm = @{}  # Dictionary mapping TFM to list of symbols

foreach ($line in ($buildOutputString -split "`n")) {
    $match = [System.Text.RegularExpressions.Regex]::Match($line, $rs0016Pattern)
    if ($match.Success) {
        $symbolName = $match.Groups[1].Value
        $tfm = $match.Groups[2].Value
        
        if (-not $symbolsByTfm.ContainsKey($tfm)) {
            $symbolsByTfm[$tfm] = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
        }
        
        [void]$symbolsByTfm[$tfm].Add($symbolName)
        Write-Verbose "Found RS0016 warning for: $symbolName (TFM: $tfm)"
    }
}

if ($symbolsByTfm.Count -eq 0) {
    Write-Host "No RS0016 warnings found. No changes needed."
    exit 0
}

$totalSymbols = ($symbolsByTfm.Values | Measure-Object -Property Count -Sum).Sum
Write-Host "Found $totalSymbols RS0016 warning(s) in $($symbolsByTfm.Count) target framework(s). Processing..."

# Process each TFM
foreach ($tfm in $symbolsByTfm.Keys) {
    if (-not $tfmToUnshippedPath.ContainsKey($tfm)) {
        Write-Warning "No Unshipped file configured for TFM: $tfm"
        continue
    }
    
    $unshippedPath = $tfmToUnshippedPath[$tfm]
    
    if (-not (Test-Path $unshippedPath)) {
        Write-Warning "Unshipped file not found: $unshippedPath"
        continue
    }
    
    Write-Verbose "Processing: $unshippedPath (TFM: $tfm)"
    
    $content = Get-Content -Path $unshippedPath -Encoding UTF8
    if ($null -eq $content) {
        $content = @()
    } elseif ($content -isnot [array]) {
        $content = @($content)
    }
    
    $symbolsToAdd = $symbolsByTfm[$tfm]
    $existingSymbols = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    foreach ($symbol in $content) {
        [void]$existingSymbols.Add($symbol)
    }
    $addedCount = 0
    
    foreach ($symbol in $symbolsToAdd) {
        if (-not $existingSymbols.Contains($symbol)) {
            $content += $symbol
            $addedCount++
            Write-Host "  Adding: $symbol"
        } else {
            Write-Verbose "  Symbol already exists: $symbol"
        }
    }
    
    if ($addedCount -gt 0) {
        # Sort the content to maintain consistency
        $content = $content | Sort-Object
        Set-Content -Path $unshippedPath -Value $content -Encoding UTF8
        Write-Host "  Added $addedCount symbol(s) to $([System.IO.Path]::GetFileName($unshippedPath))"
    }
}

Write-Host "Done!"
