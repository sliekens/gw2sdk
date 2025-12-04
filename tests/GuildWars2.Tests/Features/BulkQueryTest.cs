namespace GuildWars2.Tests.Features;

public class BulkQueryTest
{
    [Test]
    [Arguments(1000, 10)]
    [Arguments(150, 200)]
    public async Task Can_be_cancelled(int resultTotal, int chunkSize)
    {
        // Ensure cancellation works in both scenarios where the query is chunked or not
        using CancellationTokenSource cancellationTokenSource = new();
        static Task<IReadOnlyCollection<StubRecord>> GetChunk(IEnumerable<int> chunk, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IReadOnlyCollection<StubRecord> result = [.. chunk.Select(id => new StubRecord(id))];
            return Task.FromResult(result);
        }

        HashSet<int> index = [.. Enumerable.Range(1, resultTotal)];
        // Cancel after 107 records have been received (arbitrary positive number less than the total)
        const int cutoff = 107;
        int received = 0;
        IAsyncEnumerable<StubRecord> producer = BulkQuery.QueryAsync(index, GetChunk, chunkSize: chunkSize, cancellationToken: cancellationTokenSource.Token);
        OperationCanceledException? reason = await Assert.That(async () =>
        {
            await foreach (StubRecord _ in producer.WithCancellation(cancellationTokenSource.Token))
            {
                if (++received == cutoff)
                {
#if NET
                    await cancellationTokenSource.CancelAsync().ConfigureAwait(false);
#else
                    cancellationTokenSource.Cancel();
#endif
                }
            }
        }).Throws<OperationCanceledException>();
        await Assert.That(reason).IsNotNull()
            .And.Member(r => r.CancellationToken, c => c.IsEqualTo(cancellationTokenSource.Token));
        await Assert.That(received).IsEqualTo(cutoff);
    }

    [Test]
    public async Task Large_queries_are_chunked()
    {
        // Simulate 1000 records
        HashSet<int> index = [.. Enumerable.Range(1, 1000)];
        async Task<IReadOnlyCollection<StubRecord>> GetChunk(IEnumerable<int> chunk, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IReadOnlyCollection<int> chunkList = [.. chunk];
            await Assert.That(chunkList).IsNotSameReferenceAs(index);
            IReadOnlyCollection<StubRecord> result = [.. chunkList.Select(id => new StubRecord(id))];
            return result;
        }

        List<StubRecord> actual = await BulkQuery.QueryAsync(index, GetChunk, cancellationToken: TestContext.Current!.Execution.CancellationToken).ToListAsync(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(index.Count);
        foreach (int id in index)
        {
            await Assert.That(actual).Contains(record => record.Id == id);
        }
        foreach (StubRecord record in actual)
        {
            await Assert.That(index).Contains(record.Id);
        }
    }

    [Test]
    public async Task Small_queries_are_not_chunked()
    {
        // Simulate 100 records
        List<int> index = [.. Enumerable.Range(1, 100)];
        async Task<IReadOnlyCollection<StubRecord>> GetChunk(IEnumerable<int> chunk, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<int> keys = [.. chunk];
            await Assert.That(keys).IsEquivalentTo(index, EqualityComparer<int>.Default);
            IReadOnlyCollection<StubRecord> result = [.. keys.Select(id => new StubRecord(id))];
            return result;
        }

        List<StubRecord> actual = await BulkQuery.QueryAsync(index, GetChunk, cancellationToken: TestContext.Current!.Execution.CancellationToken).ToListAsync(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(index.Count);
        foreach (int id in index)
        {
            await Assert.That(actual).Contains(record => record.Id == id);
        }
        foreach (StubRecord record in actual)
        {
            await Assert.That(index).Contains(record.Id);
        }
    }
}
