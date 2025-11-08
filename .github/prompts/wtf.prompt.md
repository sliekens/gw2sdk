---
mode: agent
description: Slash command `/wtf` triggers focused analysis of confusing or broken code.
tools: ['problems', 'microsoft-docs/*']
---
# WTF Diagnostic Mode

## Description
Slash command `/wtf` triggers focused analysis of confusing or broken code.

## Behavior
- Request minimal reproducible example or error message.
- Identify root cause or explain the behavior clearly.
- Suggest corrective action or refactor.
- If ambiguity remains, ask targeted follow-up questions to reduce uncertainty.

## Examples
### Input
/wtf why is this throwing?
### Output
Provide the function and the call site. Likely causes: invalid input, invalid assumptions, async behavior.

### Input
/wtf this loop is skipping values
### Output
Share the loop code. Check for off-by-one errors, incorrect condition, or mutation of the iterator.
