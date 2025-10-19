# Spec-driven development

## Purpose

Specifications describe WHAT a feature should achieve and WHY it matters—never HOW to implement it. They:

1. Improve shared understanding before code is written
2. Provide measurable success criteria
3. Enable incremental, reviewable progress via user stories
4. Reduce rework by resolving ambiguity early

## Where specs live

All specs are stored in the `specs/` folder at the repository root. Each spec has its own directory:

```
 spec/NNN-short-name/
   spec.md
   checklists/
     requirements.md
```

`NNN` is a zero-padded sequential number assigned when the spec is created (e.g., `001`, `002`).

## Creating a new spec

Open a GitHub Copilot Agent session.

1. Run `/speckit.specify <plain-language feature description>`
   - Define what you want to build (requirements and user stories)
   - Focus on WHAT and WHY (problem, users, outcomes).
2. Run `/speckit.clarify` to resolve ambiguities (`[NEEDS CLARIFICATION: <specific question>]`).
    - Optionally edit the generated `spec.md` (fill User Scenarios, Requirements, Success Criteria, Edge Cases, Assumptions).
    - Optionally run `/speckit.checklist` to generate custom quality checklists
4. Run `/speckit.plan <technical requirements>` when you're ready to derive an implementation plan, after clarifications pass and checklist is satisfied.
5. Run `/speckit.tasks`
    - Generate actionable task lists for implementation
    - Optionally run `/speckit.analyze` to check consistency and coverage across artifacts
6. Run `/speckit.implement` 
    - Execute all tasks to build the feature according to the plan

> Important: Do NOT run `.specify/scripts` directly; they are an internal implementation detail executed by the agent layer.

## Writing good specs

Keep specs concise:
- User stories are independently testable vertical slices (P1, P2, P3…)
- Functional requirements are observable via inspection or behavior
- Success criteria are measurable, technology-agnostic outcomes
- Avoid code, APIs, class names, or infrastructure details

Bad: "Add Redis cache to speed up lookups"  
Good: "95% of lookups complete in under 300 ms for users performing typical inventory searches"

## Acceptance & adoption

A spec is considered ready for planning when:
- All mandatory sections are complete
- Checklist passes with no unchecked items
- Clarification markers are resolved (or explicitly queued for stakeholder input)

## Adapting to change

When requirements evolve:
1. Update the existing spec instead of creating a new one (unless scope is unrelated)
2. Note rationale in PR summary
3. Preserve historical success criteria (append new metrics rather than rewriting fulfilled ones)
4. Re-run checklist and ensure it still passes

## Edge cases & collisions

- Two new specs created with the same next number: Rebase, rename directory & branch to the next available number
- Missing mandatory section in review: Reviewer blocks until added

## See also

- [[Home]]
- [[Features]]
- Existing examples in `specs/`
- [[Spec kit cheatsheet]]
