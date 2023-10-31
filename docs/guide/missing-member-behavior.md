# Consider ignoring unexpected JSON structures

Another way to add resiliency to your app is to ignore errors that may occur when converting unexpected JSON structures to objects.

GW2SDK is locked to a schema version that is known to work, but sometimes breaking schema changes do occur. An example of this happened when they added Jade bots to the game, resulting in new object types in the API that were missing from the GW2SDK object model. The default behavior is to throw an error for unrecognized type names, property names and enum members.

It's also possible that the API returns data that was never observed while developing GW2SDK. This can happen for example with authorized endpoints, where the API returns data that is only available to your account. GW2SDK might not know how to deserialize the data and throw an error.

You can suppress these errors by passing `MissingMemberBehavior.Undefined` when fetching data.

``` csharp
var characters = await gw2.Accounts.GetCharacters(
    accessToken: "...",
    missingMemberBehavior: MissingMemberBehavior.Undefined
);
```

This code will ignore any values it doesn't recognize, hence the name _Undefined_ behavior, because the results are unpredictable. On the other hand, this might allow your app to continue with partial data.

`MissingMemberBehavior.Undefined` changes behavior in the following ways:

- Ignore unrecognized JSON properties
- Use the most derived base type for unrecognized type names
  - E.g. when a new class of gathering tools is added to the game, use the base class `GatheringTool`, derived from `Item`
- Generate an `Int32` value for unrecognized enum members
  - E.g. when a new item rarity _Epic_ is added to the game, the `Item.Rarity` will contain the unnamed value `(Rarity)126655543`
  - The algorithm that generates these values is deterministic

It does not protect against the following:

- When a required property is removed from the API
- When the type of a value is changed in the API

These kinds of problems are not expected to happen because GW2SDK is locked to a schema version. Otherwise, expect an _InvalidOperationException_.
