# GitHub Copilot Instructions

## Project Focus
Multi-targeted .NET library. Targets: net11.0, net10.0, net9.0, netstandard2.0, net462. Favor modern APIs over legacy APIs.

## Core Rules
- Adapt: ask for clarification; incorporate feedback.
- Remain critical and objective, provide suggestions based in first principles.
- Prefer latest C# features; Pollyfill supplies missing constructs for older TFMs.
- Cross-target: use `#if NET` (net9+, the lowest modern TFM), `#if NET10_0_OR_GREATER` or `#if NET11_0_OR_GREATER` only for APIs missing from an older modern TFM, `#if NETFRAMEWORK` only for APIs absent in .NET Standard. No runtime feature probes.
- Public API stability: additive changes (extension methods) over breaking changes.
- Dependencies: add only when explicitly requested.
- AOT-safe: no dynamic code gen; minimize reflection (none outside generators).
- Analyzers: fix warnings; do not suppress without justification.
- Explicit types everywhere (`var` only when required by the language).
- Target-typed `new()` only when the explicit type appears on the left side.
- Use records only for immutable DTO-like types (avoid when identity/mutability matters).
- Prefer pattern matching over `== null` or `as` + null check.
- Expression-bodied members only for truly trivial members.
- Use `""` and `[]` instead of `null`; do not use `string.Empty` or `Array.Empty<T>()`.
- Keep methods short and single-purpose.
- Avoid per-call / per-iteration allocations; use spans/pooling/stack allocation when it yields a clear benefit.
- Serialization: `System.Text.Json`; follow existing converter patterns.
- XML docs: one-line `<summary>` with standalone first sentence; `<remarks>` only when necessary.
- Tests: xUnit, deterministic, allocation-aware; prefer data-driven theories when clearer.
- No global usings beyond what already exists.
- Do not add meta notes explaining removed/omitted content unless explicitly instructed; keep diffs and documentation focused on current state.

## Reviewing existing code
- Use existing code as style guidance.
- Propose clearer rewrites for suboptimal code.
- Consistency is a desired outcome, not a justification for keeping poor patterns.

## Verifying changes
- Run `dotnet build` to check for compilation or code analysis problems.
- Use `dotnet test` to validate automated tests.
- Tip: use `dotnet <command> --cli-schema` for command documentation.

## Conditional Compilation Examples

Symbols: NET = net9+; NET10_0_OR_GREATER = net10+; NET11_0_OR_GREATER = net11; NETFRAMEWORK = net462+; else = netstandard2.0.

Rules:
- Add branches only when code actually differs (API gap).
- Comment the gap (why).
- Keep regions minimal; remove when obsolete.

Modern vs portable:

```csharp
#if NET
    // Reason: use .NET specific APIs
#else
    // Fallback: portable implementation for .NET Standard
#endif
```

Full split (net11 feature + modern alt + framework-only + portable):

```csharp
#if NET11_0_OR_GREATER
    // Reason: Use net11.0 specific APIs
#elif NET
    // Implementation using APIs available on every modern TFM (net9+)
#elif NETFRAMEWORK
    // Implementation using net462 specific APIs
#else
    // Fallback: portable implementation for .NET Standard
#endif
```
