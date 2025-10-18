# Remove results from previous runs
Remove-Item -ErrorAction Ignore -Recurse artifacts/bin/GW2SDK.Tests/debug_net10.0/TestResults/*

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.ps1 -- --filter-class *Colors
dotnet run --project tests/GW2SDK.Tests --framework net10.0 -- `
    --coverage `
    --coverage-settings tests/coverage.settings `
    --coverage-output coverage.xml `
    @args

# Remove reports from previous runs
Remove-Item -ErrorAction Ignore -Recurse reports

# Generate coverage report
dotnet reportgenerator -reports:artifacts/bin/GW2SDK.Tests/debug_net10.0/TestResults/coverage.xml -targetdir:artifacts/report
