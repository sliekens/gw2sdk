# Resiliency

There exists a wide category of network errors that you should handle in your application.

GW2SDK by default does not handle any network errors, because it is a complex topic and the solution depends on your application's needs.

## Type of errors

In addition to the rate limit errors already discussed, expect other errors to occur as well:

- DNS errors
- Connection timeouts or interruptions
- API offline for maintenance
- API offline to prevent spoilers for new content
- API offline due to other reasons
- API returning internal server errors
- API returning unexpected data like HTML or XML instead of JSON

Errors may also occur when deserializing the JSON (unmarshaling) to objects, which is explained in a bit more detail at the end.

## Use Polly

To counter network problems, make your HttpClient more resilient with automatic retries, timeout and delay policies using Polly.

See the full guide here: <https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests>

I recommend at least adding some timeouts and automatic retries. Tweak as needed.

[!code-csharp[](../../samples/PollyUsage/Program.cs)]

## Consider ignoring unexpected JSON structures

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

## Additional resources

- [Build resilient HTTP apps: Key development patterns](https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience)
- [Use IHttpClientFactory to implement resilient HTTP requests](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests) (outdated code, but the concepts are still valid)
