namespace GuildWars2;

/// <summary>A method that takes a set of keys and looks up the corresponding records, comparable to SELECT * WHERE id IN
/// (key1, key2. keyN).</summary>
/// <typeparam name="TKey">The type of the record's key.</typeparam>
/// <typeparam name="TRecord">The type of the records returned.</typeparam>
/// <param name="chunk">The keys of the records to query.</param>
/// <param name="token">Provides cancellation support.</param>
/// <returns>The set of records found.</returns>
[PublicAPI]

// ReSharper disable once TypeParameterCanBeVariant // it's a lie
public delegate Task<IReadOnlyCollection<TRecord>> ChunkQuery<TKey, TRecord>(
    IReadOnlyCollection<TKey> chunk,
    CancellationToken token = default
);
