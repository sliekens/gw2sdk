# TUnit test projects

This page documents how to add a new TUnit-based test project to the solution. It assumes the global solution setup (central package management, polyfills, directory build props/targets) is already in place.

## When to create a new test project

Create another test project when:

- You need to isolate tests for a distinct concern (e.g. performance profiling, integration vs generator tests).
- You want different target frameworks or runtime identifiers.
- You need different dependencies/analyzers without polluting the main test project.

All test projects MUST live under the `tests/` directory, sibling to `GuildWars2.Tests`. Name them with the `GuildWars2.` prefix for clarity (e.g. `GuildWars2.LoadTests`).

## Quick start (template)

Install the TUnit project templates if they are not already present locally. The template scaffolds a test project with example tests you can discard.

```bash
# Install or update TUnit templates
dotnet new install TUnit.Templates
dotnet new TUnit --name GuildWars2.LoadTests --output tests/GuildWars2.LoadTests
```

The generated `.csproj` will include a concrete version for `TUnit` (for example `<PackageReference Include="TUnit" Version="1.1.10" />`). Delete the `Version` attribute so Central Package Management (CPM) supplies it. The line should look like:

```xml
<PackageReference Include="TUnit" />
```

Then add the project to the solution and remove sample tests you do not need:

```bash
dotnet sln gw2sdk.slnx add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj
```

## Manual setup (preferred for consistency)

1. Create the project (console template; TUnit replaces the entry point):
   ```bash
   dotnet new console --name GuildWars2.LoadTests --output tests/GuildWars2.LoadTests
   ```
2. Remove the generated `Program.cs` – TUnit will handle test host startup:
   ```bash
   rm tests/GuildWars2.LoadTests/Program.cs
   ```
3. Add the project to the solution:
   ```bash
   dotnet sln gw2sdk.slnx add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj
   ```
   Add a project reference to the main SDK:
   ```bash
   dotnet add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj reference src/GuildWars2/GuildWars2.csproj
   ```
   Add a project reference to the common test infrastructure:
   ```bash
   dotnet add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj reference tests/GuildWars2.Tests.Common/GuildWars2.Tests.Common.csproj
   ```
4. Add package reference to TUnit (the version is centrally managed; no explicit version here if Directory.Packages.props provides it):
   ```bash
   dotnet add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj package TUnit
   ```
5. (Optional) Add any additional packages required for the test category:
   ```bash
   dotnet add tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj package Microsoft.Testing.Extensions.CodeCoverage
   ```
6. Add a first test file:
   ```bash
   cat > tests/GuildWars2.LoadTests/SampleTests.cs <<'EOF'
   namespace GuildWars2.Tests.Load;
   
   public class SampleTests
   {
       [Test]
       public async Task Sanity()
       {
           bool value = true;
           await Assert.That(value).IsTrue();
       }
   }
   EOF
   ```
7. Run the tests:
   ```bash
   dotnet run --project tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj
   ```

## Project file guidelines

Minimal project file (multi-targeting optional). For most new test projects, target the latest .NET plus older framework if coverage required:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>net10.0;net481</TargetFrameworks>
    <OutputType>Exe</OutputType>
   <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <!-- Avoid automatic polyfill injection if managing manually -->
    <EnableTUnitPolyfills>false</EnableTUnitPolyfills>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TUnit" />
    <!-- Code coverage, reports (optional) -->
    <PackageReference Include="Microsoft.Testing.Extensions.CodeCoverage" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="../../src/GuildWars2/GuildWars2.csproj" />
  </ItemGroup>
  <!-- .NET Framework specific references can be added conditionally -->
  <ItemGroup Condition="'$(TargetFrameworkIdentifier)' == '.NETFramework'">
    <PackageReference Include="System.Linq.Async" />
    <PackageReference Include="Polyfill">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

Notes:

- Keep `OutputType` as `Exe`; TUnit uses a modern test host (do NOT add `Microsoft.NET.Test.Sdk`).
- Do not add Coverlet packages (`coverlet.collector`, `coverlet.msbuild`). Use `Microsoft.Testing.Extensions.CodeCoverage`.
- Use conditional `ItemGroup` for framework-specific references as demonstrated in `GuildWars2.Tests`.
 - Use conditional `ItemGroup` for framework-specific references (e.g. `System.Linq.Async`) and add `Polyfill` when targeting .NET Framework to ensure language feature parity.

## Running tests

From repository root:

```bash
dotnet run --project tests/GuildWars2.LoadTests -- --filter Load
dotnet run --project tests/GuildWars2.LoadTests --configuration Release --coverage
dotnet run --project tests/GuildWars2.LoadTests --configuration Release --report-trx
dotnet run --project tests/GuildWars2.LoadTests --configuration Release --coverage --report-trx
```

## Adding to the solution file manually

If editing `gw2sdk.slnx` directly (XML format), add a `<Project Path="tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj" />` under the `/tests/` folder entry. Example snippet:

```xml
<Folder Name="/tests/">
  <Project Path="tests/GuildWars2.Tests.Generators/GuildWars2.Tests.Generators.csproj" />
  <Project Path="tests/GuildWars2.Tests/GuildWars2.Tests.csproj" />
  <Project Path="tests/GuildWars2.LoadTests/GuildWars2.LoadTests.csproj" />
</Folder>
```

Prefer the CLI (`dotnet sln add`) to avoid ordering mistakes.

## Historical initial setup

The solution already configures central package management (`Directory.Packages.props`) and shared build settings (`Directory.Build.props` / `Directory.Build.targets`). Polyfill and analyzer policies are managed centrally. Refer to `solution-layout.md` for broader environment details.

## Checklist for new test projects

- [ ] Directory under `tests/` with correct name.
- [ ] Project file multi-targets as needed (`net10.0;net481` etc.).
- [ ] References to main SDK project(s).
- [ ] `TUnit` package reference present (no version, versions are managed centrally).
- [ ] Optional coverage/report extension packages added.
- [ ] No `Microsoft.NET.Test.Sdk` or Coverlet packages.
- [ ] Added to solution via CLI or manual XML edit.
- [ ] First test executes successfully.

## Troubleshooting

| Symptom | Cause | Fix |
|---------|-------|-----|
| Tests not discovered | Added `Microsoft.NET.Test.Sdk` | Remove the package; ensure `OutputType` is `Exe` |
| Coverage missing | Used Coverlet packages | Remove Coverlet; run with `--coverage` flag |
| Polyfill type conflicts | Overlapping polyfill providers | Set `<EnableTUnitPolyfills>false</EnableTUnitPolyfills>` and add a `Polyfill` package reference |

## Example minimal test

```csharp
namespace GuildWars2.Tests.Load;

public class LoadSmoke
{
    [Test]
    public async Task Works()
    {
        await Assert.That(true).IsTrue();
    }
}
```
