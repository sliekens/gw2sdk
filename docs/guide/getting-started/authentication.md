# Authentication

Many API methods require an **access token** to retrieve account-specific data like characters, bank, inventory, wallet, and unlocked skins.

```csharp
var characters = await gw2.Accounts.GetCharacters(
    accessToken: "API key or subtoken"
);
```

---

## 🔑 Getting an API Key

1. Log in at <https://account.arena.net/applications>
2. Click **New Key**
3. Select the permissions you need
4. Store the key securely (use app secrets, **never** source control)

---

## 🎟️ Working with Tokens

Use `TokenProvider` to:

- **Validate** tokens and inspect their permissions
- **Create subtokens** with fewer permissions or shorter TTL

> [!TIP]
> **Plugin architecture?** Request a full-access master token from the user, then create scoped subtokens for each plugin with only the permissions it needs.
