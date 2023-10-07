#!/usr/bin/env bash
set -e

# Remove results from previous runs
rm -r GW2SDK.Tests/TestResults/* || true

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh --filter Colors
# e.g. ./test.sh --framework net7.0
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings "$@"

# Remove reports from previous runs
rm -r reports || true

# Generate coverage report
dotnet reportgenerator -reports:*/TestResults/*/coverage.cobertura.xml -targetdir:reports