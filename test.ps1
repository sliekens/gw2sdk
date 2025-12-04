#!/usr/bin/env pwsh
# Remove results from previous runs
Remove-Item -ErrorAction Ignore -Recurse artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/*

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.ps1 --treenode-filter */*/Colors*/*
dotnet run --project tests/GuildWars2.Tests --framework net10.0 -- `
    --config-file tests/testconfig.json `
    --coverage `
    @args

# Remove reports from previous runs
Remove-Item -ErrorAction Ignore -Recurse reports

# Generate coverage report
dotnet reportgenerator -reports:artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/coverage.xml -targetdir:artifacts/report
