# Release Process

This project uses a combination of **MinVer**, **Package Validation**, and **PublicApiAnalyzers** to manage versioning and ensure API stability.

## Release Plan

The release process follows a specific order to ensure that the package validation and public API tracking remain correct across versions.

### Phase 1: Pre-Release (Finalizing Content)

1.  **New APIs**: Ensure all new public members are tracked in `PublicAPI.Unshipped.txt`.
    *   Command: `./src/GuildWars2/PublicAPI/Fix-RS0016.ps1`
2.  **API Removals**: Run the cleanup script to prune [PublicAPI.Shipped.txt](src/GuildWars2/PublicAPI) of entries that are no longer present.
    *   Command: `./src/GuildWars2/PublicAPI/Fix-RS0017.ps1`
3.  **Validate Compatibility**: Run `dotnet pack`.
    *   If **Package Validation** reports breaking changes against the current `PackageValidationBaselineVersion`:
        *   **Fix** unintentional breaks.
        *   **Suppress** intentional breaks by adding them to `CompatibilitySuppressions.xml`.
            *   Command: `dotnet pack /p:ApiCompatGenerateSuppressionFile=true`

### Phase 2: Release Preparation (Consolidation)

4.  **Consolidate Public API**: Merge the work-in-progress API into the shipped baseline.
    *   Command: `./src/GuildWars2/PublicAPI/merge_public_api.sh`
    *   *This moves entries from `Unshipped.txt` to `Shipped.txt` and clears the `Unshipped.txt` files.*
5.  **Bumping Version Floor**: If this release moves to a new Major or Minor version (e.g., 3.0 &rarr; 3.1):
    *   Update `<MinVerMinimumMajorMinor>` in [src/GuildWars2/GuildWars2.csproj](src/GuildWars2/GuildWars2.csproj).
6.  **Commit**: Commit these changes with a message like `Release prep v3.x.y`.

### Phase 3: The Release

7.  **Git Tag**: Create the tag that **MinVer** will use to calculate the package version.
    *   Example: `git tag v3.0.0`
8.  **Push**: `git push --tags`. CI/CD pipeline should take over to `dotnet pack` and publish to NuGet.

### Phase 4: Post-Release (New Development Cycle)

9.  **Update Compatibility Baseline**: Update `<PackageValidationBaselineVersion>` in [src/GuildWars2/GuildWars2.csproj](src/GuildWars2/GuildWars2.csproj) to the version just released.
10. **Clear Suppressions**: Delete `CompatibilitySuppressions.xml`.
11. **Commit**: Commit these as `Prepare for next development cycle`.
