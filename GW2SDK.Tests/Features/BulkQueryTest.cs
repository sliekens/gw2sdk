namespace GuildWars2.Tests.Features;

public class BulkQueryTest
{
    [Fact]
    public async Task Large_queries_are_chunked()
    {
        // Simulate 1000 records
        var index = Enumerable.Range(1, 1000).ToHashSet();

        Task<IReadOnlyCollection<StubRecord>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            Assert.NotSame(index, chunk);
            IReadOnlyCollection<StubRecord> result = chunk.Select(id => new StubRecord(id)).ToList();
            return Task.FromResult(result);
        }

        var actual = await BulkQuery.QueryAsync(index, GetChunk).ToListAsync();

        Assert.Equal(index.Count, actual.Count);
        Assert.All(index, id => Assert.Contains(actual, record => record.Id == id));
        Assert.All(actual, record => Assert.Contains(record.Id, index));
    }

    [Fact]
    public async Task Small_queries_are_not_chunked()
    {
        // Simulate 100 records
        var index = Enumerable.Range(1, 100).ToHashSet();

        Task<IReadOnlyCollection<StubRecord>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            Assert.Same(index, chunk);
            IReadOnlyCollection<StubRecord> result = chunk.Select(id => new StubRecord(id)).ToList();
            return Task.FromResult(result);
        }

        var actual = await BulkQuery.QueryAsync(index, GetChunk).ToListAsync();

        Assert.Equal(index.Count, actual.Count);
        Assert.All(index, id => Assert.Contains(actual, record => record.Id == id));
        Assert.All(actual, record => Assert.Contains(record.Id, index));
    }

    [Theory]
    [InlineData(1000, 10)]
    [InlineData(150, 200)]
    public async Task Can_be_cancelled(int resultTotal, int chunkSize)
    {
        // Ensuyre cancellation works in both scenarios where the query is chunked or not
        CancellationTokenSource cancellationTokenSource = new();

        Task<IReadOnlyCollection<StubRecord>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            IReadOnlyCollection<StubRecord> result = chunk.Select(id => new StubRecord(id)).ToList();
            return Task.FromResult(result);
        }

        var index = Enumerable.Range(1, resultTotal).ToHashSet();

        // Cancel after 107 records have been received (arbitrary positive number less than the total)
        const int cutoff = 107;
        var received = 0;
        var producer = BulkQuery.QueryAsync(
            index, GetChunk,
            chunkSize: chunkSize,
            cancellationToken: cancellationTokenSource.Token
        );
        var reason = await Assert.ThrowsAsync<OperationCanceledException>(
            async () =>
            {
                await foreach (var _ in producer.WithCancellation(cancellationTokenSource.Token))
                {
                    if (++received == cutoff)
                    {
                        cancellationTokenSource.Cancel();
                    }
                }
            }
        );

        Assert.True(cancellationTokenSource.Token.Equals(reason.CancellationToken));
        Assert.Equal(cutoff, received);
    }
}
