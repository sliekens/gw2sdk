# EditorConfig and GlobalConfig

This document explains how `.editorconfig` and `.globalconfig` files are used
in this project to enforce consistent code style and quality standards.

## Overview

The project uses two types of configuration files to maintain code quality:

- **`.editorconfig`** files control editor behavior and C# code style preferences
- **`.globalconfig`** file configures analyzer diagnostic severity levels

These configuration files work together with the Roslyn compiler, Roslynator
analyzers, and IDE tools to provide real-time feedback and automatic code
formatting.

### Key Difference: Generated Code

The most important distinction between these two file types:

- **`.editorconfig`** applies only to **files on disk** (excludes source
  generated code)
- **`.globalconfig`** applies to **all code** (includes source generated code)

This has important implications for where to configure diagnostic severities:

- **Strict severities** should go in `.editorconfig` if you don't want them to
  apply to generated code
- **Universal severities** should go in `.globalconfig` if they should apply
  everywhere, including generated code

For this reason, many projects prefer to configure most diagnostic severities
in `.editorconfig` to avoid noise from generated code, reserving `.globalconfig`
for truly universal rules.

## File Hierarchy

The project has multiple `.editorconfig` files organized hierarchically:

```text
.
├── .editorconfig               # Root configuration
├── .globalconfig               # Analyzer diagnostic configuration
├── samples/
│   └── .editorconfig          # Sample-specific overrides
├── src/
│   └── GuildWars2/
│       └── .editorconfig      # Main library-specific rules
└── tests/
    └── .editorconfig          # Test-specific overrides
```

### Root .editorconfig

The root `.editorconfig` file at the repository root contains:

- **Universal settings** for all file types (indentation style)
- **File-type specific settings** for XML, JSON, YAML files
- **C# coding conventions** that apply to the entire solution
  - Indentation and spacing (4 spaces for C#)
  - Using directive organization
  - Code style preferences (var usage, this. qualifiers, etc.)
  - Expression preferences (pattern matching, null propagation, etc.)
  - Naming conventions
  - File header templates
  - New line preferences

The root file is marked with `root = true` to prevent searching for
configuration files in parent directories.

### Subdirectory .editorconfig Files

Subdirectory `.editorconfig` files inherit from the root and can override
specific rules for their context:

- **`src/GuildWars2/.editorconfig`** - Contains rules specific to the main
  library code, such as stricter requirements for public APIs
- **`tests/.editorconfig`** - Relaxes certain rules for test code (e.g., allows
  longer lines, relaxes naming conventions for test methods)
- **`samples/.editorconfig`** - May relax rules for sample/demo code

## .globalconfig

The `.globalconfig` file is used exclusively for configuring diagnostic
severities (warnings, errors, suggestions) for code analyzers. Unlike
`.editorconfig`, there is typically only one `.globalconfig` file at the
repository root.

**Important:** `.globalconfig` applies to all code, including source generated
code. If you have source generators in your project and don't want strict
diagnostic rules to apply to generated code, consider moving those diagnostic
configurations to `.editorconfig` instead.

### What's in .globalconfig

The `.globalconfig` file contains:

1. **CA (Code Analysis) diagnostics** - Microsoft's code analysis rules
   - Design rules (CA1000-CA1070)
   - Globalization rules (CA1303-CA1311)
   - Performance rules (CA1802-CA1872)
   - Security rules (CA2000-CA5405)
   - And many more...

2. **IDE diagnostics** - Visual Studio/Roslyn IDE suggestions
   - Code style rules (IDE0001-IDE0350)
   - Formatting rules (IDE2000-IDE2006)

3. **Roslynator diagnostics** - Roslynator analyzer rules
   - Formatting rules (RCS0001-RCS0063)
   - Refactoring suggestions (RCS1001-RCS1264)

4. **Roslynator options** - Configuration for Roslynator analyzers
   - `roslynator_max_line_length = 100`
   - `roslynator_accessibility_modifiers = explicit`
   - `roslynator_prefix_field_identifier_with_underscore = true`
   - Many other style preferences

### Diagnostic Severity Levels

Each diagnostic can be configured with one of these severity levels:

- **`error`** - Treated as a build error (blocks compilation)
- **`warning`** - Shown as a warning (does not block compilation by default)
- **`suggestion`** - Shown as an IDE suggestion (faded code)
- **`silent`** - Rule is active but doesn't show in IDE (affects code fixes)
- **`none`** - Rule is completely disabled

## Choosing Between .editorconfig and .globalconfig

When deciding where to configure a diagnostic severity, consider this decision tree:

### Should the rule apply to source generated code?

**NO** (most common case):
- Put the diagnostic in `.editorconfig`
- Source generators may produce code that doesn't follow your project's style
  guidelines
- You typically don't want warnings from generated code cluttering your build
  output
- Examples: naming conventions, code style rules, design rules for public APIs

**YES** (less common):
- Put the diagnostic in `.globalconfig`
- Critical security or correctness rules should apply everywhere
- Examples: security rules (CA5xxx), certain performance rules, null reference
  checks

### Example Scenario: This Project

This project uses source generators (e.g., `GuildWars2.Generators` and
`GuildWars2.Tests.Generators`). The `.globalconfig` contains comprehensive
diagnostic configurations that apply to all code, including generated code.

If you find that source generators are triggering unwanted diagnostics, you have
two options:

1. **Move the diagnostic to `.editorconfig`** - This excludes generated code
   entirely
2. **Suppress in generated code** - Add `[GeneratedCode]` attribute or
   `#pragma warning disable` in the generator

### Diagnostic Configuration Best Practices

1. **Start conservative**: Put new diagnostics in `.editorconfig` first
2. **Test with generators**: Build and check for warnings from generated code
3. **Move if needed**: If a rule truly should apply universally, move it to
   `.globalconfig`
4. **Document exceptions**: If you must disable a rule for generated code, add
   a comment explaining why

## How These Files Are Used

### During Development

When you open a C# file in an IDE (Visual Studio, Visual Studio Code with
C# extension, JetBrains Rider):

1. The IDE reads all applicable `.editorconfig` files from the current directory
   up to the root
2. The IDE reads the `.globalconfig` file
3. The IDE applies formatting rules and shows diagnostics in real-time
4. The IDE can automatically format code according to the rules when you save
   or run "Format Document"

### During Build

When you build the project with `dotnet build`:

1. The Roslyn compiler reads the `.editorconfig` and `.globalconfig` files
2. Code analyzers run and report diagnostics
3. Diagnostics configured as errors will fail the build
4. Diagnostics configured as warnings will be reported but won't fail the build
   (unless `WarningsAsErrors` is enabled)

### In CI/CD

The GitHub Actions workflows build the project with the same configuration,
ensuring that:

- All developers see the same diagnostics
- Code that fails locally will also fail in CI
- The code style is consistent across all contributors

## Key Style Decisions

Some notable style decisions configured in these files:

### Code Style (.editorconfig)

- **Indentation**: 4 spaces for C#, 2 spaces for XML/JSON/YAML
- **Using directives**: System namespaces first, separated by blank line
- **var usage**: Explicit type preferred (configurable per-project)
- **this. qualifier**: Not used (except when necessary)
- **Braces**: Required for multi-line statements
- **Expression bodies**: Preferred for properties, indexers, accessors
- **Pattern matching**: Preferred over type checks and null checks
- **File-scoped namespaces**: Required (C# 10+)
- **Nullable reference types**: Enabled

### Formatting (.globalconfig)

- **Maximum line length**: 100 characters (Roslynator)
- **Field naming**: Prefix private fields with underscore
- **Accessibility modifiers**: Always explicit
- **ConfigureAwait**: Required on all await expressions
- **Async suffix**: Not required (disabled RCS1046)

### Disabled Rules

Some rules are explicitly disabled with rationale:

- **CA1708** - Identifiers should differ by more than case
  - *Disabled because mixing camelCase and PascalCase is common for private
    fields and public properties*
- **RCS1046** - Asynchronous method name should end with 'Async'
  - *Disabled because we don't put return types in method names elsewhere*
- **IDE0058** - Expression value is never used
  - *Disabled because it makes fluent APIs unusable*

## Modifying Configuration

### Adding New Rules

To add a new analyzer rule:

1. Decide whether the rule should apply to generated code:
   - If **NO** (most common): Add to `.editorconfig`
   - If **YES** (universal rules only): Add to `.globalconfig`
2. Add the rule with the desired severity
3. If the rule has options, add them to `.editorconfig`
4. Build the project to verify it works as expected
5. Check that generated code is not causing unwanted diagnostics
6. Commit the files together

Example for `.editorconfig` (excludes generated code):

```ini
[*.cs]
# CA1062: Validate arguments of public methods
dotnet_diagnostic.CA1062.severity = warning
```

Example for `.globalconfig` (includes generated code):

```ini
# CA2007: Consider calling ConfigureAwait on the awaited task
dotnet_diagnostic.CA2007.severity = warning
```

### Overriding Rules for Specific Projects

If a specific project or directory needs different rules:

1. Create a `.editorconfig` file in that directory
2. Add only the rules you want to override (don't copy everything)
3. The nearest `.editorconfig` file takes precedence

Example for tests:

```ini
[*.cs]
# Test methods can have longer names
dotnet_diagnostic.CA1707.severity = none

# Test data can be more complex
dotnet_diagnostic.CA1861.severity = none
```

### Temporarily Disabling Rules

To disable a rule for a specific line or region of code:

```csharp
#pragma warning disable CA1062 // Validate arguments of public methods
public void MyMethod(string value)
{
    // This method intentionally doesn't validate arguments
    Console.WriteLine(value);
}
#pragma warning restore CA1062
```

Or use a `SuppressMessage` attribute:

```csharp
[SuppressMessage("Design", "CA1062:Validate arguments of public methods",
    Justification = "Arguments are validated by the caller")]
public void MyMethod(string value)
{
    Console.WriteLine(value);
}
```

## References

- [EditorConfig documentation](https://editorconfig.org/)
- [EditorConfig for .NET](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#editorconfig)
- [GlobalConfig for .NET](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#global-analyzerconfig)
- [Code analysis rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/)
- [Code style rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/)
- [Roslynator documentation](https://josefpihrt.github.io/docs/roslynator/)
