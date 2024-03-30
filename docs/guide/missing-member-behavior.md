# Consider ignoring unexpected JSON structures

Another way to add resiliency to your app is to ignore errors that may occur when the JSON document from an API response does not exactly match the target CLR type.

GW2SDK is locked to a schema version that is known to work, but sometimes breaking schema changes do occur. An example of this happened when they added Jade bots to the game, resulting in new item types in the API that were missing from the GW2SDK object model. The default behavior is to throw an error for unexpected JSON properties and for polymorphic types when the JSON can't be matched to any derived type. This prevents code from continuing with incomplete or incorrect data, which could lead to data corruption.

The downside of this behavior is that it can be brittle. If the API changes, your app can lose functionality. This is a trade-off between correct behavior and availability, it's not possible to achieve both simultaneously. If your code needs to run with high availability, you can suppress these errors by passing `MissingMemberBehavior.Undefined` when fetching data.

``` csharp
var characters = await gw2.Accounts.GetCharacters(
    accessToken: "...",
    missingMemberBehavior: MissingMemberBehavior.Undefined
);
```

This code will ignore any JSON values it doesn't recognize, hence the name _Undefined_ behavior, because the results are unpredictable and may lead to incorrect outcomes. On the other hand, this might allow your app to continue to work without interruptions due to API changes.

`MissingMemberBehavior.Undefined` changes behavior in the following ways:

- Ignore unrecognized JSON properties
- Use the closest super type for polymorphic types when the JSON can't be matched to any derived type
  - E.g. when a new category of gathering tools is added to the game, use the base class `GatheringTool`, derived from `Item`

It does not protect against the following:

- When a required JSON property is removed from the API
- When the type of a JSON value is changed in the API

These categories of problems are not expected to happen because GW2SDK is locked to a schema version. Otherwise, expect an _InvalidOperationException_.
