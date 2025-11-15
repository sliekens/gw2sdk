# TUnit test projects

This page documents how to add a new TUnit-based test project to the solution. It assumes the global solution setup (central package management, polyfills, directory build props/targets) is already in place.

## When to create a new test project

Create another test project when:

- You need to isolate tests for a distinct concern (e.g. performance profiling, integration vs generator tests).
- You want different target frameworks or runtime identifiers.
- You need different dependencies/analyzers without polluting the main test project.

All test projects MUST live under the `tests/` directory, sibling to `GW2SDK.Tests`. Name them with the `GW2SDK.` prefix for clarity (e.g. `GW2SDK.LoadTests`).

## Quick start (template approach)

If you have not already installed TUnit templates locally, they may already be installed historically. If not, you can still use the manual approach below. The template approach creates sample tests you can delete.

```bash
# Install or update TUnit templates
dotnet new install TUnit.Templates
dotnet new TUnit --name GW2SDK.LoadTests --output tests/GW2SDK.LoadTests
```

Then add the project to the solution and remove sample tests you do not need:

```bash
dotnet sln gw2sdk.slnx add tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj
```

## Manual setup (preferred for consistency)

1. Create the project (console template; TUnit replaces the entry point):
   ```bash
   dotnet new console --name GW2SDK.LoadTests --output tests/GW2SDK.LoadTests
   ```
2. Remove the generated `Program.cs` – TUnit will handle test host startup:
   ```bash
   rm tests/GW2SDK.LoadTests/Program.cs
   ```
3. Add the project to the solution:
   ```bash
   dotnet sln gw2sdk.slnx add tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj
   ```
   Add a project reference to the main SDK:
   ```bash
   dotnet add tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj reference src/GW2SDK/GW2SDK.csproj
   ```
4. Add package reference to TUnit (the version is centrally managed; no explicit version here if Directory.Packages.props provides it):
   ```bash
   dotnet add tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj package TUnit
   ```
5. (Optional) Add any additional packages required for the test category:
   ```bash
   dotnet add tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj package Microsoft.Testing.Extensions.CodeCoverage
   ```
6. Add a first test file:
   ```bash
   cat > tests/GW2SDK.LoadTests/SampleTests.cs <<'EOF'
   namespace GuildWars2.Tests.Load;

   public class SampleTests
   {
       [Test]
       public async Task Sanity()
       {
           await Assert.That(true).IsTrue();
       }
   }
   EOF
   ```
7. Run the tests:
   ```bash
   dotnet run --project tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj -- --filter Sanity
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
    <RootNamespace>GuildWars2.Tests.Load</RootNamespace>
    <!-- Avoid automatic polyfill injection if managing manually -->
    <EnableTUnitPolyfills>false</EnableTUnitPolyfills>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TUnit" />
    <!-- Code coverage, reports (optional) -->
    <PackageReference Include="Microsoft.Testing.Extensions.CodeCoverage" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="../../src/GW2SDK/GW2SDK.csproj" />
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
- Use conditional `ItemGroup` for framework-specific references as demonstrated in `GW2SDK.Tests`.
 - Use conditional `ItemGroup` for framework-specific references (e.g. `System.Linq.Async`) and add `Polyfill` when targeting .NET Framework to ensure language feature parity.

## Running tests

From repository root:

```bash
dotnet run --project tests/GW2SDK.LoadTests -- --filter Load
dotnet run --project tests/GW2SDK.LoadTests --configuration Release --coverage
dotnet run --project tests/GW2SDK.LoadTests --configuration Release --report-trx
dotnet run --project tests/GW2SDK.LoadTests --configuration Release --coverage --report-trx
```

## Adding to the solution file manually

If editing `gw2sdk.slnx` directly (XML format), add a `<Project Path="tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj" />` under the `/tests/` folder entry. Example snippet:

```xml
<Folder Name="/tests/">
  <Project Path="tests/GW2SDK.Tests.Generators/GW2SDK.Tests.Generators.csproj" />
  <Project Path="tests/GW2SDK.Tests/GW2SDK.Tests.csproj" />
  <Project Path="tests/GW2SDK.LoadTests/GW2SDK.LoadTests.csproj" />
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
| Namespace collisions | Forgot `RootNamespace` | Add `<RootNamespace>` matching folder purpose |

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
