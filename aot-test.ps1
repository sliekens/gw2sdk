#!/usr/bin/env pwsh

# Publishes tests/GuildWars2.AotTests with Native AOT (reflection-based serialization disabled) and runs the native
# binary. It is a consumer of the public SDK surface; see https://github.com/sliekens/gw2sdk/issues/521.
# Requires the Native AOT prerequisites: https://aka.ms/nativeaot-prerequisites (on Linux: clang and zlib1g-dev).

$ErrorActionPreference = "Stop"
$project = Join-Path $PSScriptRoot "tests/GuildWars2.AotTests/GuildWars2.AotTests.csproj"
$rid = [System.Runtime.InteropServices.RuntimeInformation]::RuntimeIdentifier
$output = Join-Path $PSScriptRoot "artifacts/aot/$rid"

# The same checks with the JIT, as a baseline
dotnet run --project $project --configuration Release -- @args
if ($LASTEXITCODE -ne 0) { throw "JIT run failed with exit code $LASTEXITCODE" }

dotnet publish $project --configuration Release --runtime $rid --output $output
if ($LASTEXITCODE -ne 0) { throw "Native AOT publish failed with exit code $LASTEXITCODE" }

$binary = Join-Path $output ($IsWindows ? "GuildWars2.AotTests.exe" : "GuildWars2.AotTests")
& $binary --native @args
if ($LASTEXITCODE -ne 0) { throw "Native AOT run failed with exit code $LASTEXITCODE" }
