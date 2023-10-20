
The pattern for implementing an endpoint is always the same.

1. Add records that model the shape of the API data
2. Add a JSON converter for each record
3. Add one or more `IHttpRequest` where you encapsulate the sending and processing of the API request
4. Add a query class which acts as a facade
5. Add your facade class as a public property to the `Gw2Client` class

Finally you should also add some integration tests.

A good code example for studying is the Quaggans endpoint (in /GW2SDK/Features/Quaggans).

## Example record

``` csharp
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Quaggans;

// The record is sealed because it is not intended for inheritance
// The [DataTransferObject] attribute is used to enforce this pattern of sealed, immutable DTOs
// The [PublicAPI] attribute tells ReSharper not to log a warning for unused code
[PublicAPI, DataTransferObject]
public sealed record Quaggan
{
    // Every property is required to require an assignment (even if the property is nullable)
    public required string Id { get; init; }

    // Every property is also immutable (init-only) to discourage modifying data you don't own
    public required string PictureHref { get; init; }
}
```

## Example JSON converter

``` csharp
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quaggans;

[PublicAPI]
public static class QuagganJson
{
    // The JSON conversion is a static extension method for JsonElement
    // The conversion is crafted by hand for performance and to compensate
    // for the lack of MissingMemberHandling in System.Text.Json
    public static Quaggan GetQuaggan(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // Define all the members of the JSON data using RequiredMember
        RequiredMember id = "id";
        RequiredMember url = "url";

        // You can also use OptionalMember or NullableMember (for value types)
        OptionalMember optional = "optional-field"; // default(T) when missing
        NullableMember nullable = "nullable-field"; // Nullable<T> when missing

        // Iterate over all the properties of the JSON object
        foreach (var member in json.EnumerateObject())
        {
            // Compare each JSON property name to the names above
            // and copy the value when there is a match
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(url.Name))
            {
                url = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                // Throw an error for unexpected JSON properties
                // (unless MissingMemberBehavior is set to Undefined)
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        // Create an instance of your record with all the values collected from the JsonElement
        // The Map() method can throw an InvalidOperationException when the RequiredMember is missing 
        return new Quaggan
        {
            Id = id.Map(value => value.GetStringRequired()),
            PictureHref = url.Map(value => value.GetStringRequired())
        };
    }
}
```

## Example HTTP request

``` csharp
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Quaggans;

[PublicAPI]
public sealed class QuagganByIdRequest : IHttpRequest<Replica<Quaggan>>
{
    // A static template from which you can create HttpRequestMessages
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/quaggans")
    {
        // Enable Gzip compression
        AcceptEncoding = "gzip"
    };

    public QuagganByIdRequest(string quagganId)
    {
        QuagganId = quagganId;
    }
    
    // The query string argument to use for this request
    public string QuagganId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Quaggan>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        // Create a new template from the one above with the final request configuration
        var request = Template with
        {
            // QueryBuilder makes it easy to add query string arguments to the template
            // You must use the recommended schema version, it is enforced by automated tests
            Arguments = new QueryBuilder
            {
                { "id", QuagganId },
                { "v", SchemaVersion.Recommended }
            }
        };

        // You can pass a Template to HttpClient.SendAsync and
        // it gets implicitly converted to HttpRequestMessage
        // Always use HttpCompletionOption.ResponseHeadersRead for better performance
        using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        // A helper method ensures that there is a usable result (no errors)
        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        // Another helper method creates a JsonDocument from the response content
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        
        // Create and return a Replica<T>
        // A Replica<T> is a container for your record that contains additional response headers
        return new Replica<Quaggan>
        {
            Value = json.RootElement.GetQuaggan(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
```

## Example query class

``` csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Quaggans;

[PublicAPI]
public sealed class QuaggansQuery
{
    private readonly HttpClient http;

    public QuaggansQuery(HttpClient http)
    {
        // Ensure HttpClient is not null (defensive programming)
        this.http = http ?? throw new ArgumentNullException(nameof(http));

        // Ensure a base address is set
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    // Create a user-friendly method for every endpoint
    // Always add cancellation support
    // Only support MissingMemberBehavior when it makes sense
    public Task<Replica<HashSet<Quaggan>>> GetQuaggans(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetQuaggansIndex(
        CancellationToken cancellationToken = default
    )
    {
        QuaggansIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    // This is the example from before
    public Task<Replica<Quaggan>> GetQuagganById(
        string quagganId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuagganByIdRequest request = new(quagganId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Quaggan>>> GetQuaggansByIds(
        IReadOnlyCollection<string> quagganIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByIdsRequest request =
            new(quagganIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Quaggan>>> GetQuaggansByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
```

## Example Gw2Client

Add your new query class to the existing Gw2Client class.

``` diff
using System;
using System.Net.Http;
// ...
+ using GuildWars2.Quaggans;
using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public sealed class Gw2Client
{
    private readonly HttpClient httpClient;

    public Gw2Client(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    // ...

+    public QuaggansQuery Quaggans => new(httpClient);
}
```
