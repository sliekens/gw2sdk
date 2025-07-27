using GuildWars2.Http;

namespace GuildWars2;

/// <summary>Contains message metadata.</summary>
[PublicAPI]
public sealed record MessageContext
{
    /// <summary>Initializes a new instance of the <see cref="MessageContext" /> class.</summary>
    /// <param name="response">The HTTP response message.</param>
    public MessageContext(HttpResponseMessage response)
    {
        Date = response.Headers.Date ?? default;
        Expires = response.Content.Headers.Expires;
        LastModified = response.Content.Headers.LastModified;
        ResultCount = response.Headers.GetResultCount();
        ResultTotal = response.Headers.GetResultTotal();
        PageSize = response.Headers.GetPageSize();
        PageTotal = response.Headers.GetPageTotal();
        Links = response.Headers.GetLink();
    }

    /// <summary>The date and time at which the message was originated.</summary>
    public DateTimeOffset Date { get; }

    /// <summary>The date and time after which the value should be considered stale. It is suggested to update your cache after
    /// this moment. When this value is missing, you may calculate your own expiration by adding a cache duration to the
    /// <see cref="LastModified" /> value.</summary>
    public DateTimeOffset? Expires { get; }

    /// <summary>The date and time that indicate when the value was last modified. You may use this value to calculate an
    /// absolute expiration for your cache by adding a cache duration (max-age) of your choice. When this value is missing, you
    /// may use the <see cref="Date" /> value instead for the calculation.</summary>
    public DateTimeOffset? LastModified { get; }

    /// <summary>When the response is a (sub)set, this value indicates the size of the subset.</summary>
    public int? ResultCount { get; }

    /// <summary>When the response is a (sub)set, this value indicates the total size of the set.</summary>
    public int? ResultTotal { get; }

    /// <summary>When the response is a page, this value indicates the maximum number of items per page.</summary>
    public int? PageSize { get; }

    /// <summary>When the response is a page, this value indicates the total number of pages.</summary>
    public int? PageTotal { get; }

    /// <summary>When the response is a page, this value contains links to related pages.</summary>
    public Link? Links { get; }
}
