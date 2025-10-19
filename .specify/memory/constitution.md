# GW2SDK Constitution
<!--
Sync Impact Report (generated 2025-10-19T16:09:35.894Z)
Version change: (none) → 1.0.0
Modified principles: (initial creation)
Added sections: Core Principles; Additional Constraints; Development Workflow; Governance
Removed sections: None
Templates:
 - .specify/templates/plan-template.md: ✅ unchanged
 - .specify/templates/spec-template.md: ✅ unchanged
 - .specify/templates/tasks-template.md: ✅ unchanged
Follow-up TODOs: None
-->

## Core Principles

### I. Minimal Dependencies & Portability
The SDK MUST keep external dependencies to the absolute minimum (prefer zero beyond
System.*). Rationale: fewer transitive risks, faster restore, wider TFM support, easier AOT.
Target TFMs: net9.0, net8.0, netstandard2.0, net462. Any new dependency MUST justify
portability impact and AOT safety. If a feature cannot meet portability it MUST be
isolated behind conditional compilation with clear reason comments.

### II. Manual JSON & Explicit Data Contracts
All GW2 API and client data handling MUST use explicit, hand-written parsing or
System.Text.Json low-level APIs (Utf8JsonReader / writer) — NO automatic member mapping
via reflection-based serializers. Models MUST avoid nullable ambivalence: use optional
pattern with explicit presence semantics. Rationale: performance, allocation control,
AOT safety, version resilience.

### III. AOT Safety & Reflection Discipline
Runtime reflection (Type.Get*, Activator.CreateInstance, dynamic code gen) MUST NOT be
used outside source generators. Source generators MUST produce code that is linker/AOT
friendly (no hidden reflection). Rationale: ensures NativeAOT viability and predictable
startup. Any required metadata inspection MUST occur at build-time.

### IV. Testing Strategy: Live Integration Signal + Unit Edge Cases
Each public functionality MUST have exactly one integration test covering its longest
happy path USING the live Guild Wars 2 API or client data where applicable. Integration
tests are intentionally NON-DETERMINISTIC: they MUST exercise real network behavior so a
remote behavioral change causes a failure (serving as an early signal). Do not record
fixtures for these tests. Minimize false positives by asserting only stable invariants
(e.g., schema presence, non-empty collections) and avoiding volatile data (timestamps,
rotating events). Edge/error/boundary conditions MUST be covered by focused unit tests
which remain deterministic. When an upstream change breaks an integration test, triage:
 - If SDK behavior is incorrect → fix code.
 - If upstream contract changed → update parsing & tests.
 - If upstream outage/instability → optionally quarantine with a documented issue proven by a unit test demonstrating the failure condition. Rationale: early detection of real-world
regressions outweighs local determinism; unit tests preserve reliability and speed.

### V. Explicitness & Correctness over Consistency
APIs MUST prefer explicit naming, parameters, and return types over clever abstractions.
Avoid premature generalization. Correct behavior and clarity trump stylistic uniformity.
Controversial patterns ("magic" DI containers, excessive extension methods, ubiquitous
base classes) MUST be avoided unless a concrete problem and rejected alternatives are
documented. Rationale: maintain long-term readability and reduce hidden coupling.

## Additional Constraints

Performance: Favor low allocation, Span/Memory-based processing when it yields material
benefit (measurable p95 improvement or reduced GC pressure). Avoid micro-optimization
that harms clarity.

Platform: Windows-only aspects (e.g., MumbleLink memory-mapped access) MUST be isolated
and guarded with runtime platform checks plus conditional compilation comments.

Error Handling: Public APIs MUST fail fast with descriptive exceptions; internal code
SHOULD use defensive checks. No silent catches unless conversion to a typed error result.

Versioning (Code): Semantic versioning of NuGet packages. MinVer derives pre-release build versions from git history on main; annotated git tags establish released package versions. Breaking API change → MAJOR; additive API surface/feature → MINOR; internal fixes, performance improvements, docs, or non-breaking clarifications → PATCH. Constitution versioning governed separately (see Governance).

## Development Workflow

Direct commits to main: No pull requests. Contributors (including agents) MUST run the full test suite locally before committing and pushing. CI runs on every push to main; its purpose is validation, not discovery of easily catchable failures.

Test Gate: A commit MUST NOT be pushed if any integration or unit test fails locally. Flaky integration tests (Principle IV) are acceptable only when failure is due to upstream change; re-run to confirm before pushing.

Documentation: Architectural and technical complexity SHOULD be documented in the 'wiki' directory in the repository. This directory is periodically synchronized to the GitHub repository wiki to keep external documentation aligned.

Complexity Recording: When introducing notable architectural complexity, add or update a markdown file under wiki/ describing rationale, alternatives considered, and explicit trade-offs.

Quality Gates Removed: Reviewer-focused PR gates are replaced by self-review discipline; contributors are accountable for adherence to principles prior to push.

## Governance

Authority: This constitution supersedes informal style guidance.

Amendment Procedure:
1. Propose change via PR editing this file.
2. Classify version bump (MAJOR/MINOR/PATCH) with rationale.
3. Update Sync Impact Report comment.

Compliance Review: Ad-hoc review by maintainer; contributors self-assess adherence when modifying code touching public APIs.

**Version**: 1.0.0 | **Ratified**: 2025-10-19 | **Last Amended**: 2025-10-19
