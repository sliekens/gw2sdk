﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GuildWars2.Tests.Features;

public class SplitQueryTest
{
    [Theory]
    [InlineData(1000, 10)]
    [InlineData(150, 200)]
    public async Task It_respects_cancellation_requested(int resultTotal, int bufferSize)
    {
        // Check if cancellation works in both scenarios where the buffer size is smaller or larger than the index
        // because the implementation is slightly optimized for the case where buffering is not needed
        CancellationTokenSource cancellationTokenSource = new();

        var index = Enumerable.Range(1, resultTotal).ToHashSet();
        var records = index.Select(id => new StubRecord(id)).ToList();

        var sut = SplitQuery.Create<int, StubRecord>(
            (range, _) =>
            {
                var found = records.Where(record => range.Contains(record.Id)).ToHashSet();
                return Task.FromResult((IReadOnlyCollection<StubRecord>)found);
            },
            bufferSize
        );

        var received = 0;
        var producer = sut.QueryAsync(index, cancellationToken: cancellationTokenSource.Token);
        var reason = await Assert.ThrowsAsync<OperationCanceledException>(
            async () =>
            {
                await foreach (var _ in producer.WithCancellation(cancellationTokenSource.Token))
                {
                    if (++received == 107)
                    {
                        cancellationTokenSource.Cancel();
                    }
                }
            }
        );

        Assert.True(cancellationTokenSource.Token.Equals(reason.CancellationToken));
        Assert.Equal(107, received);
    }

    [Fact]
    public async Task It_can_split_queries_into_buffers()
    {
        // Simulate 1000 records
        var index = Enumerable.Range(1, 1000).ToHashSet();
        var records = index.Select(id => new StubRecord(id)).ToList();

        const int bufferSize = 10;
        var sut = SplitQuery.Create<int, StubRecord>(
            (range, _) =>
            {
                var found = records.Where(record => range.Contains(record.Id)).ToHashSet();
                return Task.FromResult((IReadOnlyCollection<StubRecord>)found);
            }
        );

        var actual = await sut.QueryAsync(index, bufferSize).ToListAsync();

        Assert.Equal(index.Count, actual.Count);
        Assert.All(index, id => Assert.Contains(actual, record => record.Id == id));
        Assert.All(
            actual,
            record =>
            {
                Assert.Contains(record.Id, index);
            }
        );
    }

    [Fact]
    public async Task It_can_skip_buffering_if_the_index_is_small_enough()
    {
        // Simulate 100 records
        var index = Enumerable.Range(1, 100).ToHashSet();
        var records = index.Select(id => new StubRecord(id)).ToList();

        var sut = SplitQuery.Create<int, StubRecord>(
            (range, _) =>
            {
                var found = records.Where(record => range.Contains(record.Id)).ToHashSet();
                return Task.FromResult((IReadOnlyCollection<StubRecord>)found);
            }
        );

        var actual = await sut.QueryAsync(index).ToListAsync();

        Assert.Equal(index.Count, actual.Count);
        Assert.All(index, id => Assert.Contains(actual, record => record.Id == id));
        Assert.All(
            actual,
            record =>
            {
                Assert.Contains(record.Id, index);
            }
        );
    }
}

internal sealed record StubRecord(int Id);
