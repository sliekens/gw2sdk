using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GW2SDK.Tests.Features
{
    public class SplitQueryTest
    {
        [Fact]
        public async Task It_can_split_queries_into_buffers()
        {
            // Simulate 1000 records
            var index = Enumerable.Range(1, 1000)
                .ToHashSet();
            var records = index.Select(id => new StubRecord(id))
                .ToList();

            const int bufferSize = 10;
            var sut = SplitQuery.Create<int, StubRecord>((keys, ct) =>
            {
                var found = records.Where(record => keys.Contains(record.Id))
                    .ToHashSet();
                return Task.FromResult((IReplicaSet<StubRecord>)new StubReplica(found));
            });

            var producer = sut.QueryAsync(index, bufferSize);
            var actual = await (
                from record in producer
                where record.HasValue
                select record.Value).ToListAsync();

            Assert.Equal(index.Count, actual.Count);
            Assert.All(index, id => actual.Any(record => record.Id == id));
            Assert.All(actual,
                record =>
                {
                    index.Contains(record.Id);
                });
        }

        [Fact]
        public async Task It_can_skip_buffering_if_the_index_is_small_enough()
        {
            // Simulate 100 records
            var index = Enumerable.Range(1, 1000)
                .ToHashSet();
            var records = index.Select(id => new StubRecord(id))
                .ToList();

            var sut = SplitQuery.Create<int, StubRecord>((keys, ct) =>
            {
                var found = records.Where(record => keys.Contains(record.Id))
                    .ToHashSet();
                return Task.FromResult((IReplicaSet<StubRecord>)new StubReplica(found));
            });

            var producer = sut.QueryAsync(index);
            var actual = await (
                from record in producer
                where record.HasValue
                select record.Value).ToListAsync();

            Assert.Equal(index.Count, actual.Count);
            Assert.All(index, id => actual.Any(record => record.Id == id));
            Assert.All(actual,
                record =>
                {
                    index.Contains(record.Id);
                });
        }
    }

    internal sealed record StubRecord(int Id);

    internal sealed class StubReplica : IReplicaSet<StubRecord>
    {
        public StubReplica(
#if NET
            IReadOnlySet<StubRecord> values
#else
                IReadOnlyCollection<StubRecord> values
#endif
        )
        {
            HasValues = true;
            Values = values;
            Context = new CollectionContext(values.Count, values.Count);
        }

        public DateTimeOffset Date { get; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValues { get; }

#if NET
        public IReadOnlySet<StubRecord> Values { get; }
#else
        public IReadOnlyCollection<StubRecord> Values { get; }
#endif

        public ICollectionContext Context { get; }
    }
}
