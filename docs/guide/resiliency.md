# Resiliency

There exists a wide range of network errors that you must handle. The API might be unreachable, offline for maintenance or simply hang. There is also a sliding rate limit of 600 requests per minute after which the API responds with rate limit errors.

Errors can also occur at the Application Layer level due to breaking changes on the server.

## Use Polly

To combat these issues, make your HttpClient more resilient with automatic retries, timeout and delay policies. See the full guide here: <https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests>

I recommend at least adding some timeouts and automatic retries. Tweak as needed.

[!code-csharp[](../../samples/PollyUsage/Program.cs)]


## MissingMemberBehavior

Another way to add resiliency to your app is to ignore unknown API objects. GW2SDK is locked to a schema version that is known to work, but sometimes breaking schema changes do occur. An example of this happened when they added Jade bots to the game, resulting in new object types in the API that were missing from the GW2SDK object model. The default behavior is to throw an error for unrecognized type names, property names and enum members.

You can suppress these errors by passing `MissingMemberBehavior.Undefined` when fetching data.

``` csharp
var ranks = await sut.Wvw.GetRanks(missingMemberBehavior: MissingMemberBehavior.Undefined);
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
