using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>A method that takes a set of keys and looks up the corresponding records, comparable to SELECT * WHERE id IN
/// (key1, key2. keyN).</summary>
/// <typeparam name="TKey">The type of the record's key.</typeparam>
/// <typeparam name="TRecord">The type of the records returned.</typeparam>
/// <param name="keys">The keys of the records to query.</param>
/// <param name="token">Provides cancellation support.</param>
/// <returns>The set of records found.</returns>
[PublicAPI]

// ReSharper disable once TypeParameterCanBeVariant // it's a lie
public delegate Task<IReplicaSet<TRecord>> InQuery<TKey, TRecord>(
#if NET
    IReadOnlySet<TKey> keys,
#else
    IReadOnlyCollection<TKey> keys,
#endif
    CancellationToken token = default
);

/// <inheritdoc cref="SplitQuery{TKey,TRecord}" />
[PublicAPI]
public static class SplitQuery
{
    public static SplitQuery<TKey, TRecord> Create<TKey, TRecord>(
        InQuery<TKey, TRecord> query,
        IProgress<ICollectionContext>? progress,
        int maxConcurrency = 20
    )
    {
        return new SplitQuery<TKey, TRecord>(query, progress, maxConcurrency);
    }

    public static SplitQuery<TKey, TRecord> Create<TKey, TRecord>(InQuery<TKey, TRecord> query, int maxConcurrency = 20)
    {
        return new SplitQuery<TKey, TRecord>(query, default, maxConcurrency);
    }
}

/// <summary>Helps you retrieve all records in a given index by splitting it into smaller chunks before quering. This is
/// useful when your index contains more than 200 items (the maximum allowed per query).</summary>
[PublicAPI]
public sealed class SplitQuery<TKey, TRecord>
{
    private readonly int maxConcurrency;

    private readonly IProgress<ICollectionContext>? progress;

    private readonly InQuery<TKey, TRecord> query;

    internal SplitQuery(
        InQuery<TKey, TRecord> query,
        IProgress<ICollectionContext>? progress,
        int maxConcurrency
    )
    {
        this.query = query;
        this.progress = progress;
        this.maxConcurrency = Math.Max(1, maxConcurrency);
    }

    public async IAsyncEnumerable<TRecord> QueryAsync(
#if NET
        IReadOnlySet<TKey> index,
#else
        IReadOnlyCollection<TKey> index,
#endif
        int bufferSize = 200,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var resultTotal = index.Count;
        if (resultTotal == 0)
        {
            throw new ArgumentException("The index cannot be empty.", nameof(index));
        }

        if (bufferSize is < 1 or > 200)
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSize),
                bufferSize,
                "The buffer size must be a number between 1 and 200");
        }

        ReportProgress(resultTotal, 0);

        // PERF: no need to split if index is small enough
        if (index.Count <= bufferSize)
        {
            var result = await query(index, cancellationToken)
                .ConfigureAwait(false);
            ReportProgress(resultTotal, result.Context.ResultCount);
            foreach (var record in result)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return record;
            }
        }
        else
        {
            using SemaphoreSlim throttler = new(maxConcurrency);

            var resultCount = 0;

            var batches = SplitIndex(index.ToList(), bufferSize);
            var inflight = batches.Select(next =>
                    Throttled(() => query(next, cancellationToken), throttler, cancellationToken))
                .ToList();

            while (inflight.Count != 0)
            {
                var done = await Task.WhenAny(inflight)
                    .ConfigureAwait(false);

                inflight.Remove(done);

                var result = await done.ConfigureAwait(false);
                resultCount += result.Context.ResultCount;
                ReportProgress(resultTotal, resultCount);
                foreach (var record in result)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return record;
                }
            }
        }
    }

    private static async Task<T> Throttled<T>(
        Func<Task<T>> taskFactory,
        SemaphoreSlim throttler,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await throttler.WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            return await taskFactory()
                .ConfigureAwait(false);
        }
        finally
        {
            throttler.Release();
        }
    }

    private static IEnumerable<HashSet<TKey>> SplitIndex(List<TKey> indices, int bufferSize)
    {
        for (var offset = 0; offset < indices.Count; offset += bufferSize)
        {
            var subset = indices.GetRange(offset, Math.Min(bufferSize, indices.Count - offset));
            yield return new HashSet<TKey>(subset);
        }
    }

    private void ReportProgress(int resultTotal, int resultCount)
    {
        progress?.Report(new CollectionContext(resultTotal, resultCount));
    }
}