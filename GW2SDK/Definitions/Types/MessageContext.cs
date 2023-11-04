using GuildWars2.Http;

namespace GuildWars2;

/// <summary>Contains message metadata.</summary>
[PublicAPI]
public sealed record MessageContext
{
    public MessageContext(HttpResponseMessage response)
    {
        Date = response.Headers.Date.GetValueOrDefault();
        Expires = response.Content.Headers.Expires;
        LastModified = response.Content.Headers.LastModified;
        ResultContext = response.Headers.GetResultContext();
        PageContext = response.Headers.GetPageContext();
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

    /// <summary>When the value is a collection, contains context about the collection.</summary>
    public ResultContext? ResultContext { get; }

    /// <summary>When the value is a page, contains context about this page and other pages.</summary>
    public PageContext? PageContext { get; }
}
