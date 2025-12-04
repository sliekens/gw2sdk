#!/usr/bin/env bash
set -e

# Ignore exit code 8 (test session ran zero tests)
export TESTINGPLATFORM_EXITCODE_IGNORE=8

# Remove results from previous runs
rm -r artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/* || true

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh --treenode-filter */*/Colors*/*
dotnet test --framework net10.0 \
    --config-file tests/testconfig.json \
    --coverage \
    "$@"

# Remove reports from previous runs
rm -r reports || true

# Generate coverage report
dotnet reportgenerator -reports:artifacts/bin/GuildWars2.Tests/debug_net10.0/TestResults/coverage.xml -targetdir:artifacts/
