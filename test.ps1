#!/usr/bin/env pwsh

# Remove results from previous runs
$coveragePath = Join-Path $PSScriptRoot "artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/coverage.xml"
New-Item -ErrorAction Ignore -ItemType Directory -Force (Split-Path $coveragePath) | Out-Null
Remove-Item -ErrorAction Ignore -Recurse artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/*

# Ignore exit code 8 (test session ran zero tests)
$env:TESTINGPLATFORM_EXITCODE_IGNORE = "8"


# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.ps1 --treenode-filter */*/Colors*/*
dotnet test --framework net10.0 `
    --config-file tests/testconfig.json `
    --coverage `
    --coverage-output-format xml `
    --coverage-output $coveragePath `
    @args

# Remove reports from previous runs
Remove-Item -ErrorAction Ignore -Recurse reports

# Generate coverage report
dotnet reportgenerator -reports:$coveragePath -targetdir:artifacts/report
