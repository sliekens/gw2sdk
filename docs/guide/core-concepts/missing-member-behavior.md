# Handling Unexpected JSON Structures

GW2SDK is locked to a known API schema version, but breaking changes can occur (e.g., new item types like Jade Bots). By default, the library throws errors for unexpected JSON to prevent data corruption.

---

## ⚖️ The Trade-off

| Behavior | Correctness | Availability |
|----------|-------------|---------------|
| **Default** (strict) | ✅ Guaranteed | ❌ May fail on API changes |
| **Undefined** (lenient) | ⚠️ Best-effort | ✅ Resilient to changes |

---

## 🛡️ Enabling Lenient Mode

```csharp
var characters = await gw2.Accounts.GetCharacters(
    accessToken: "...",
    missingMemberBehavior: MissingMemberBehavior.Undefined
);
```

---

## 📋 What `Undefined` Does

| Scenario | Behavior |
|----------|----------|
| Unrecognized JSON properties | Ignored |
| Unknown polymorphic types | Falls back to closest base type |

> **Example:** A new gathering tool category → returns as `GatheringTool` (base class)

---

## ⚠️ Not Protected Against

- Required properties removed from the API
- Changed property types

These are rare since GW2SDK locks to a schema version, but would throw `InvalidOperationException`.
