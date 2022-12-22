using System;
using JetBrains.Annotations;

namespace GuildWars2;

/// <summary>Contains a replica (copy) of some value along with contextual information about the value.</summary>
/// <typeparam name="T">The type of the value.</typeparam>
[PublicAPI]
public sealed record Replica<T>
{
    /// <summary>The replicated value.</summary>
    public required T Value { get; init; }

    /// <summary>Gets the date and time when the current value was replicated. This is strictly informational and does not
    /// represent the age of the value or when the value was last modified.</summary>
    public required DateTimeOffset Date { get; init; }

    /// <summary>The date and time after which the value should be considered stale (outdated). It is suggested to update your
    /// cache after this moment. When this value is missing, you may calculate your own expiration by adding a cache duration
    /// to the <see cref="LastModified" /> value.</summary>
    public required DateTimeOffset? Expires { get; init; }

    /// <summary>The date and time that indicate when the current value was updated. You may use this value to calculate an
    /// absolute expiration for your cache by adding a cache duration (max-age) of your choice. When this value is missing, you
    /// may use the <see cref="Date" /> value instead for the calculation.</summary>
    public required DateTimeOffset? LastModified { get; init; }

    /// <summary>When the value is a collection, contains context about the collection.</summary>
    public required ResultContext? ResultContext { get; init; }

    /// <summary>When the value is a page, contains context about this page and other pages.</summary>
    public required PageContext? PageContext { get; init; }

    public static implicit operator T(Replica<T> replica) => replica.Value;
}
