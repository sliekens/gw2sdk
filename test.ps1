# Remove results from previous runs
Remove-Item -ErrorAction Ignore -Recurse GW2SDK.Tests/TestResults/*

# Run tests with coverage, additional arguments are passed on to dotnet test
# e.g. ./test.sh --filter Colors
# e.g. ./test.sh --framework net9.0
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings $args

# Remove reports from previous runs
Remove-Item -ErrorAction Ignore -Recurse reports

# Generate coverage report
dotnet reportgenerator -reports:*/TestResults/*/coverage.cobertura.xml -targetdir:reports
