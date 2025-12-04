# TargetFrameworkIdentifier

The MSBuild `TargetFrameworkIdentifier` is the canonical string that identifies the .NET implementation family for a given Target Framework Moniker (TFM). Instead of parsing raw `$(TargetFramework)` text (like checking if it starts with `netcoreapp`), use the MSBuild property function:

```xml
$([MSBuild]::GetTargetFrameworkIdentifier('$(TargetFramework)'))
```

This is future-proof across naming changes (for example, the .NET 5 shift from `netcoreappX.Y` to `netX.Y`).

## Why use TargetFrameworkIdentifier?

- Avoid brittle string parsing of `TargetFramework`.
- Works uniformly for multi-targeting (`TargetFrameworks`).
- Enables clear conditional logic for references, properties, and packing.
- Separates framework family (identifier) from version (`GetTargetFrameworkVersion`) and platform (`GetTargetPlatformIdentifier`).

Example migration (from deprecated pattern):

```xml
<!-- Old (brittle) -->
<PropertyGroup Condition="$(TargetFramework.StartsWith('netcoreapp'))">
  <IsModern>true</IsModern>
</PropertyGroup>

<!-- Recommended -->
<PropertyGroup Condition="'$([MSBuild]::GetTargetFrameworkIdentifier($(TargetFramework)))' == '.NETCoreApp'">
  <IsModern>true</IsModern>
</PropertyGroup>
```

(Source: TargetFramework name change in .NET 5.)

## Common Identifier Values

| TargetFrameworkIdentifier | Typical TFMs (examples) | Notes |
| ------------------------- | ----------------------- | ----- |
| `.NETCoreApp`             | `net5.0`, `net6.0`, `net8.0`, `net9.0`, `net10.0`, legacy `netcoreapp3.1` | Modern .NET (including .NET Core + .NET 5+) |
| `.NETStandard`            | `netstandard2.0`, `netstandard2.1` | API unification surface |
| `.NETFramework` / `.NET Framework` | `net48`, `net481`, `net462`, `net35` | Full framework; some APIs only here |
| `Silverlight`             | `sl4`, `sl5` | Deprecated ecosystem |
| `WindowsPhone` / `WindowsPhoneApp` | `wp8`, `wp81`, `wpa81` | Deprecated |
| `NETMicroFramework`       | `netmf` | Embedded scenarios |
| `NETPortable`             | (profile-based portable class libraries; obsolete) | Historical; replaced by .NET Standard |
| `.NETNanoFramework`       | `netnano1.0` | Specialized micro device framework |

Notes:
- Modern OS-specific TFMs (e.g. `net8.0-windows10.0.22621.0`) still yield `.NETCoreApp`; the `windows` part is obtained via `GetTargetPlatformIdentifier`.
- Some APIs (e.g. `ToolLocationHelper`) still refer to identifiers `.NETFramework` or `.NET Framework` for legacy resolution logic.

## Parsing Functions (MSBuild Property Functions)

| Function | Purpose |
| -------- | ------- |
| `GetTargetFrameworkIdentifier(string targetFramework)` | Returns identifier (e.g. `.NETCoreApp`). |
| `GetTargetFrameworkVersion(string targetFramework, int versionPartCount = 2)` | Returns version (e.g. `8.0`). |
| `GetTargetPlatformIdentifier(string targetFramework)` | Returns platform (e.g. `windows`, `ios`, `android`). |
| `GetTargetPlatformVersion(string targetFramework, int versionPartCount = 2)` | Returns platform version (e.g. `15.0`). |
| `IsTargetFrameworkCompatible(string targetFrameworkTarget, string targetFrameworkCandidate)` | Compatibility evaluation between TFMs. |
| `FilterTargetFrameworks(string incoming, string filter)` | Reduces a list of TFMs to a filtered subset. |

Example output (from docs):
```
Value1 = .NETCoreApp
Value2 = 5.0
Value3 = windows
Value4 = 7.0
...
```

## Practical Usage Examples

### Conditional References
```xml
<ItemGroup Condition="'$([MSBuild]::GetTargetFrameworkIdentifier($(TargetFramework)))' == '.NETFramework'">
  <Reference Include="System.Configuration" />
</ItemGroup>
```

### Multi-target Property Customization
```xml
<PropertyGroup Condition="'$([MSBuild]::GetTargetFrameworkIdentifier($(TargetFramework)))' == '.NETStandard'">
  <CanUseReflectionEmit>false</CanUseReflectionEmit>
</PropertyGroup>
```

### Guarding Platform-Specific Code
```csharp
#if NETFRAMEWORK
    Console.WriteLine("Full framework path logic");
#elif NETSTANDARD2_1_OR_GREATER
    Console.WriteLine("Portable logic");
#elif NET8_0_OR_GREATER
    Console.WriteLine(".NET 8+ logic");
#endif
```
(Preprocessor symbols are documented separately; they differ from identifiers.)

### Filtering a Target List
```xml
<PropertyGroup>
  <TargetFrameworks>net6.0;net7.0;netstandard2.0</TargetFrameworks>
  <Filtered>$([MSBuild]::FilterTargetFrameworks($(TargetFrameworks), 'net7.0;netstandard2.0'))</Filtered>
</PropertyGroup>
```

### Avoiding Raw Prefix Checks (Recommended Pattern)
```xml
<Target Name="DoModernStuff"
        Condition="'$([MSBuild]::GetTargetFrameworkIdentifier($(TargetFramework)))' == '.NETCoreApp' 
                   And $([MSBuild]::GetTargetFrameworkVersion($(TargetFramework))) >= 8.0">
  <!-- Modern-only tasks -->
</Target>
```

### Using ToolLocationHelper (Programmatic Lookup)
APIs like `ToolLocationHelper.GetPathToReferenceAssemblies(identifier, version, profile, ...)` rely on correct identifier strings (`.NETFramework`, `.NETCoreApp`, etc.). For legacy chaining logic (e.g. versions 2.0–4.0 of .NET Framework), passing `.NETFramework` is required.

## Compatibility Considerations

| Symptom | Cause | Fix |
| ------- | ----- | --- |
| Condition never matches after upgrading to .NET 5+ | Using `StartsWith('netcoreapp')` | Switch to `GetTargetFrameworkIdentifier` comparison. |
| Reference missing only on one target | Conditional group used raw text parsing | Replace with identifier + version functions. |
| Platform APIs unavailable | Platform part encoded in TFM not parsed | Use `GetTargetPlatformIdentifier` / `GetTargetPlatformVersion`. |

## Deprecated / Historical Identifiers

Some identifiers (e.g. `NETPortable`, older DNX / ASP.NET Core preview identities) are no longer active. Migrate packages to modern TFMs (`netstandard2.0+`, `net6.0+`). See deprecated TFM mappings in framework docs.

## Troubleshooting

- Always treat identifier, version, and platform as orthogonal dimensions.
- Prefer `IsTargetFrameworkCompatible` over manual numeric comparisons across identifiers.

## Source Links (Microsoft Learn)

- TargetFramework name change: https://learn.microsoft.com/en-us/dotnet/core/compatibility/sdk/5.0/targetframework-name-change
- Property functions (identifier/version/platform parsing): https://learn.microsoft.com/en-us/visualstudio/msbuild/property-functions
- Target frameworks (supported TFMs & OS-specific patterns): https://learn.microsoft.com/en-us/dotnet/standard/frameworks
- MSBuild target framework & platform overview: https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-target-framework-and-target-platform
- Multitargeting overview: https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-multitargeting-overview
- ToolLocationHelper APIs: https://learn.microsoft.com/en-us/dotnet/api/microsoft.build.utilities.toollocationhelper.getpathtoreferenceassemblies
- SignFile / ResolveManifestFiles (property exposure): https://learn.microsoft.com/en-us/dotnet/api/microsoft.build.tasks.signfile.targetframeworkidentifier / https://learn.microsoft.com/en-us/dotnet/api/microsoft.build.tasks.resolvemanifestfiles.targetframeworkidentifier

## Quick Reference Snippets

Get identifier:
```xml
$([MSBuild]::GetTargetFrameworkIdentifier($(TargetFramework)))
```

Get framework version (major.minor):
```xml
$([MSBuild]::GetTargetFrameworkVersion($(TargetFramework), 2))
```

Get platform identifier (if OS-specific TFM):
```xml
$([MSBuild]::GetTargetPlatformIdentifier($(TargetFramework)))
```

Check compatibility:
```xml
$([MSBuild]::IsTargetFrameworkCompatible('net8.0', 'net6.0'))
```

## Suggested Further Reading

- Platform compatibility analyzer.
- Preprocessor symbols vs identifiers.
- Multi-target NuGet packaging guidance.

---

_Last updated: 2025-11-11_
