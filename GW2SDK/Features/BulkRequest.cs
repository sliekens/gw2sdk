namespace GuildWars2;

/// <summary>A method that takes a set of keys and retrieves the corresponding values.</summary>
/// <typeparam name="TKey">The data type of the key.</typeparam>
/// <typeparam name="TValue">The data type of the values.</typeparam>
/// <param name="keys">The keys for which to retrieve the values.</param>
/// <param name="cancellationToken">A token to cancel the request.</param>
/// <returns>The collection of values found.</returns>
[PublicAPI]
public delegate Task<IReadOnlyCollection<TValue>> BulkRequest<in TKey, TValue>(
    IEnumerable<TKey> keys,
    CancellationToken cancellationToken = default
);
