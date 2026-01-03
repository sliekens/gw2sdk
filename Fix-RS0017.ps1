#!/usr/bin/env pwsh
<#
.SYNOPSIS
Removes obsolete public API entries from PublicAPI.Shipped.txt based on RS0017 warnings.

.DESCRIPTION
This script runs dotnet build, parses RS0017 warnings from the build output,
and removes the corresponding lines from PublicAPI.Shipped.txt files.

.EXAMPLE
.\Fix-RS0017.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

# Run dotnet build with --no-incremental to get RS0017 warnings
Write-Verbose "Running dotnet build --no-incremental..."
$buildOutput = & dotnet build --no-incremental 2>&1
$buildOutputString = $buildOutput -join "`n"

# Parse RS0017 warnings
# RS0017 format: "/path/to/file(line,col): warning RS0017: Symbol 'X.Y.Z' is part of..."
# Extract both file path and symbol name
$rs0017Pattern = "^(.+?)\(\d+,\d+\): warning RS0017:.*?Symbol '([^']+)'"
$apisToRemove = @{}  # Dictionary mapping file path to list of symbols

foreach ($line in ($buildOutputString -split "`n")) {
    $match = [System.Text.RegularExpressions.Regex]::Match($line, $rs0017Pattern)
    if ($match.Success) {
        $filePath = $match.Groups[1].Value
        $symbolName = $match.Groups[2].Value
        
        if (-not $apisToRemove.ContainsKey($filePath)) {
            $apisToRemove[$filePath] = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
        }
        
        [void]$apisToRemove[$filePath].Add($symbolName)
        Write-Verbose "Found RS0017 warning for: $symbolName (in $filePath)"
    }
}

if ($apisToRemove.Count -eq 0) {
    Write-Host "No RS0017 warnings found. No changes needed."
    exit 0
}

$totalSymbols = ($apisToRemove.Values | Measure-Object -Property Count -Sum).Sum
Write-Host "Found $totalSymbols RS0017 warning(s) in $($apisToRemove.Count) file(s). Processing..."

# Process each file that has RS0017 warnings
foreach ($filePath in $apisToRemove.Keys) {
    if (-not (Test-Path $filePath)) {
        Write-Warning "File not found: $filePath"
        continue
    }
    
    Write-Verbose "Processing: $filePath"
    
    $content = Get-Content -Path $filePath -Encoding UTF8
    $originalLineCount = $content.Count
    $apisInFile = $apisToRemove[$filePath]
    
    # Filter out lines matching the APIs to remove
    $newContent = $content | Where-Object {
        $line = $_
        $shouldKeep = $true
        
        foreach ($api in $apisInFile) {
            # Check if the line matches the API exactly
            if ($line -eq $api) {
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
        Set-Content -Path $filePath -Value $newContent -Encoding UTF8
        Write-Host "  Removed $removedCount line(s) from $([System.IO.Path]::GetFileName($filePath))"
    }
}

Write-Host "Done!"
