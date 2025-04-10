#!/usr/bin/env bash
set -ex

# Remove results from previous runs
rm -r GW2SDK.Tests/TestResults/* || true

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh --filter Colors
dotnet run --project GW2SDK.Tests --framework net9.0 -- \
    --coverage \
    --coverage-settings GW2SDK.Tests/coverage.settings \
    --coverage-output-format xml \
    --coverage-output coverage.xml \
    "$@"

# Remove reports from previous runs
rm -r reports || true

# Generate coverage report
dotnet reportgenerator -reports:GW2SDK.Tests/bin/Debug/net9.0/TestResults/coverage.xml -targetdir:reports
