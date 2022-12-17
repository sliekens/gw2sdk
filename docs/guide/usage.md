# Basic usage

The entry point for API access is `GuildWars2.Gw2Client`. From there you can use IntelliSense to discover resources.

The `Gw2Client` has a single dependency on `System.Net.Http.HttpClient` which you must provide from your application code.

A very simple console app might look like this.

<<< @/samples/BasicUsage/Program.cs

Points of interest

- The `IReplica<>` interface returns HTTP response headers like `Date` and `Expires` if present
- Its `Value` property returns the actual API data
