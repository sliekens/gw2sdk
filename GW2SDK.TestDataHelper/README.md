# Test Data Helper

This utility gets JSON from the GW2 API and writes it to stdout.

Usage example

```sh
dotnet run --out-dir ./data --indented
```

## Why?

Many integration tests depend on real API data. This tool can be used to make an offline copy of that data.

```sh
# Run in GW2SDK.TestDataHelper
dotnet run --out-dir ../GW2SDK.Tests/Data

# Run in GW2SDK.Tests
dotnet test --filter Feature=Colors
```
