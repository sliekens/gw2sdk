# AGENTS.md

## .NET SDK pinning policy (standing order)

The SDK version is pinned, never relaxed:

- Do NOT add `rollForward`, remove `global.json`, or otherwise unpin the SDK to make a local build work.
- To change the SDK version: install it locally first, then bump **all** pins together so they stay aligned.

### SDK version locations (keep in sync when bumping)

| Location | Role |
| --- | --- |
| `global.json` | Authoritative pin for all local builds (`dotnet --version` resolves here first) |
| `.devcontainer/base/devcontainer.json` | `ghcr.io/devcontainers/features/dotnet` feature version; builds the `ghcr.io/sliekens/gw2sdk/devcontainer` image |
| `.github/workflows/ci.yml`, `release.yml`, `gh-pages.yml`, `dependabot-lockfile.yml` | No explicit version. They run inside the devcontainer image above, which is built by `devcontainer.yml` (including the `native-aot` job in `ci.yml`). `.github/scripts/ensure-pinned-sdk.sh` installs the exact `global.json` SDK into the job when that image does not have it yet |

The image is still the SDK those workflows use. It is rebuilt on the same push that changes `.devcontainer/**`, so the first run can start before the new image is published. The ensure script covers that gap without relaxing the pin. Bumping `global.json` alone does NOT change the image, and a later run still needs the image pin to match.

Also check: `wiki/PC-requirements.md` mentions the required .NET major version (prose only).

### Analyzer notes

`Directory.Build.props` sets `TreatWarningsAsErrors=true`; `.globalconfig` raises most style rules to warning.
Newer SDKs ship newer analyzers that can flag code the pinned CI SDK does not. When a new analyzer
diagnostic has no valid fix (e.g. IDE0028 on constructors wrapping `ImmutableDictionary`, or on
comparer-based `HashSet` initializers where a collection expression would silently drop the comparer),
suppress at the site or project level with a justification instead of rewriting — see existing
`[SuppressMessage("Style", "IDE0028", ...)]` precedents and the `NoWarn` in
`tests/GuildWars2.ArchitectureTests/GuildWars2.ArchitectureTests.csproj`.

## Build / test

```pwsh
dotnet build gw2sdk.slnx   # solution format is .slnx
./test.ps1                 # runs net11.0 tests with coverage; needs `dotnet tool restore` once
```

Native AOT consumer test (`tests/GuildWars2.AotTests`, not run by `dotnet test`):

```pwsh
./aot-test.ps1             # or ./aot-test.sh; runs the checks with the JIT, then publishes with Native AOT and runs the binary
```

It needs the Native AOT prerequisites (clang and zlib1g-dev on Linux; the `nativeaot` devcontainer feature installs them).
Collection element types that must work under Native AOT are rooted by `ImmutableValueCollectionConverterGenerator`
in `src/GuildWars2.Generators`; each rooted value type adds about 190 KB to consumers' AOT binaries.

Coverage XML lands at `artifacts/bin/GuildWars2.Tests/debug_net11.0/TestResults/coverage.xml`
(test.ps1 passes an absolute `--coverage-output` so this holds locally and in CI);
generate the HTML report from there if needed.
