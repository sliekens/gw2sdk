﻿using System.Runtime.CompilerServices;
using GuildWars2.Tasks;

namespace GuildWars2;

/// <summary>Provides a method to retrieve bulk data from the Guild Wars 2 API.</summary>
[PublicAPI]
public static class BulkQuery
{
    /// <summary>The default chunk size is 200. This is the maximum number of keys that can be retrieved in a single query.</summary>
    public const int DefaultChunkSize = 200;

    /// <summary>The default degree of parallelism is 20. This is the maximum number of concurrent queries that will be
    /// executed in a bulk.</summary>
    public const int DefaultDegreeOfParallelism = 20;

    /// <summary>Retrieves bulk data from the Guild Wars 2 API. <br /> Given a large collection of keys, this method will the
    /// collection into smaller chunks and query the API for each chunk. The chunk size can optionally be specified, otherwise
    /// the maximum chunk size (200) is used. <br /> Chunks are queried in parallel by default. The results are streamed as
    /// they become available, ordered by completion. The degree of parallelism can optionally be specified (default: 20). You
    /// can optionally specify a progress object to receive progress updates.</summary>
    /// <typeparam name="TKey">The data type of the keys.</typeparam>
    /// <typeparam name="TValue">The data type of the values.</typeparam>
    /// <param name="keys">The keys for which to retrieve the values.</param>
    /// <param name="bulkRequest">The method that takes a set of keys and retrieves the corresponding values. The method must
    /// be thread-safe. The method must not return null, duplicate values, values that do not correspond to the keys. The
    /// method may return values out of order or return fewer values than the number of keys.</param>
    /// <param name="degreeOfParallelism">The maximum number of concurrent requests.</param>
    /// <param name="chunkSize">The maximum number of keys to retrieve in a single request.</param>
    /// <param name="progress">An optional progress object to receive progress updates: result count and result total.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The collection of values found.</returns>
    /// <exception cref="ArgumentException"> Thrown when the keys collection is empty. </exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="chunkSize" /> is less than 1 or greater than
    /// 200, or when <paramref name="degreeOfParallelism" /> is less than 1.</exception>
    public static async IAsyncEnumerable<TValue> QueryAsync<TKey, TValue>(
        IReadOnlyCollection<TKey> keys,
        BulkRequest<TKey, TValue> bulkRequest,
        int degreeOfParallelism = DefaultDegreeOfParallelism,
        int chunkSize = DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        if (keys.Count == 0)
        {
            throw new ArgumentException("The keys collection cannot be empty.", nameof(keys));
        }

        if (chunkSize is < 1 or > 200)
        {
            throw new ArgumentOutOfRangeException(
                nameof(chunkSize),
                chunkSize,
                "The chunk size must be a number between 1 and 200."
            );
        }

        if (degreeOfParallelism < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(degreeOfParallelism),
                degreeOfParallelism,
                "The degree of parallelism must be at least 1."
            );
        }

        var resultCount = 0;
        var resultTotal = keys.Count;

        progress?.Report(new BulkProgress(resultTotal, resultCount));

        // PERF: no need to create chunks if keys do not exceed the chunk size
        if (keys.Count <= chunkSize)
        {
            var result = await bulkRequest(keys, cancellationToken).ConfigureAwait(false);
            foreach (var value in result)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return value;
                progress?.Report(new BulkProgress(resultTotal, ++resultCount));
            }

            yield break;
        }

        // Split the index into smaller chunks to avoid hitting the 200 item limit
        var chunks = Chunk(keys.ToList(), chunkSize);

        // Proceed with the queries in parallel, but limit the number of concurrent queries
        using SemaphoreSlim limiter = new(degreeOfParallelism);
        await foreach (var result in chunks.Select(
                async chunk =>
                {
                    await limiter.WaitAsync(cancellationToken).ConfigureAwait(false);
                    return await bulkRequest(chunk, cancellationToken).ConfigureAwait(false);
                }
            )
            .ToList()
            .OrderByCompletion()
            .WithCancellation(cancellationToken))
        {
            foreach (var record in result)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return record;
                progress?.Report(new BulkProgress(resultTotal, ++resultCount));
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
