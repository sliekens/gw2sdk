using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Tasks;
using JetBrains.Annotations;

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

/// <inheritdoc cref="BulkQuery{TKey,TRecord}" />
[PublicAPI]
public static class BulkQuery
{
    public const int DefaultChunkSize = 200;

    public const int DefaultDegreeOfParalllelism = 20;

    /// <summary>Helps you retrieve all records in a given index by splitting it into smaller chunks before quering. This is
    /// useful when your index contains more than 200 items (the maximum allowed per query).</summary>
    public static async IAsyncEnumerable<TRecord> QueryAsync<TKey, TRecord>(
        IReadOnlyCollection<TKey> index,
        ChunkQuery<TKey, TRecord> chunkQuery,
        int degreeOfParalllelism = DefaultDegreeOfParalllelism,
        int chunkSize = DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        if (index.Count == 0)
        {
            throw new ArgumentException("The index cannot be empty.", nameof(index));
        }

        if (chunkSize is < 1 or > 200)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chunkSize),
                chunkSize,
                "The chunk size must be a number between 1 and 200."
            );
        }

        if (degreeOfParalllelism < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(degreeOfParalllelism),
                degreeOfParalllelism,
                "The degree of paralellism must be at least 1."
            );
        }

        var resultCount = 0;
        var resultTotal = index.Count;

        progress?.Report(new ResultContext(resultTotal, resultCount));

        // PERF: no need to create chunks if index does not exceed the chunk size
        if (index.Count <= chunkSize)
        {
            var result = await chunkQuery(index, cancellationToken).ConfigureAwait(false);
            progress?.Report(new ResultContext(resultTotal, result.Count));
            foreach (var record in result)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return record;
            }

            yield break;
        }

        // Split the index into smaller chunks to avoid hitting the 200 item limit
        var chunks = Chunk(index.ToList(), chunkSize);

        // Proceed with the queries in parallel, but limit the number of concurrent queries
        using SemaphoreSlim limiter = new(degreeOfParalllelism);
        await foreach (var result in chunks.Select(
                async chunk =>
                {
                    await limiter.WaitAsync(cancellationToken).ConfigureAwait(false);
                    return await chunkQuery(chunk, cancellationToken).ConfigureAwait(false);
                }
            )
            .ToList()
            .OrderByCompletion()
            .WithCancellation(cancellationToken))
        {
            resultCount += result.Count;
            progress?.Report(new ResultContext(resultTotal, resultCount));
            foreach (var record in result)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return record;
            }

            limiter.Release();
        }

        static IEnumerable<List<TKey>> Chunk(List<TKey> index, int size)
        {
            for (var offset = 0; offset < index.Count; offset += size)
            {
                yield return index.GetRange(offset, Math.Min(size, index.Count - offset));
            }
        }
    }
}
