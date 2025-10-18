#!/usr/bin/env bash
set -e

# Remove results from previous runs
rm -r artifacts/bin/GW2SDK.Tests/debug_net10.0/TestResults/* || true

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh -- --filter-class *Colors
dotnet run --project tests/GW2SDK.Tests --framework net10.0 -- \
    --coverage \
    --coverage-settings tests/coverage.settings \
    --coverage-output coverage.xml \
    "$@"

# Remove reports from previous runs
rm -r reports || true

# Generate coverage report
dotnet reportgenerator -reports:artifacts/bin/GW2SDK.Tests/debug_net10.0/TestResults/coverage.xml -targetdir:artifacts/
