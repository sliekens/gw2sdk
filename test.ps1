# Remove results from previous runs
Remove-Item -ErrorAction Ignore -Recurse GW2SDK.Tests/TestResults/*

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh --filter Colors
dotnet run --project GW2SDK.Tests --framework net9.0 -- `
    --coverage `
    --coverage-settings GW2SDK.Tests/coverage.settings `
    --coverage-output coverage.xml `
    @args

# Remove reports from previous runs
Remove-Item -ErrorAction Ignore -Recurse reports

# Generate coverage report
dotnet reportgenerator -reports:GW2SDK.Tests/bin/Debug/net9.0/TestResults/coverage.xml -targetdir:reports
