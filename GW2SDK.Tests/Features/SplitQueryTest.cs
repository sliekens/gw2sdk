using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Quaggans;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features
{
    public class SplitQueryTest
    {
        private class KeepLastProgress : IProgress<ICollectionContext>
        {
            public ICollectionContext Last { get; private set; }

            public void Report(ICollectionContext value)
            {
                Last = value;
            }
        }

        [Fact]
        public async Task It_can_split_queries_into_buffers()
        {
            await using var services = new Composer();
            var quagganService = services.Resolve<QuagganService>();

            var bufferSize = 10;
            var progressSpy = new KeepLastProgress();
            var sut = SplitQuery.Create<string, QuagganRef>(async (indices, _) =>
                    await quagganService.GetQuaggansByIds(indices),
                progressSpy);

            var index = await quagganService.GetQuaggansIndex();

            var actual = new List<QuagganRef>();
            await foreach (var quaggan in sut.QueryAsync(index.Values, bufferSize))
            {
                if (quaggan.HasValue)
                {
                    actual.Add(quaggan.Value);
                }
            }

            Assert.Equal(index.Values.Count, progressSpy.Last.ResultTotal);
            Assert.Equal(index.Values.Count, progressSpy.Last.ResultCount);
            Assert.Equal(index.Values.Count, actual.Count);
            Assert.All(actual,
                quaggan =>
                {
                    index.Values.Contains(quaggan.Id);
                });
        }

        [Fact]
        public async Task It_can_skip_buffering_if_the_index_is_small_enough()
        {
            await using var services = new Composer();
            var quagganService = services.Resolve<QuagganService>();

            // Less than the total amount of quaggans
            var bufferSize = 200;
            var progressSpy = new KeepLastProgress();
            var sut = SplitQuery.Create<string, QuagganRef>(async (indices, _) =>
                    await quagganService.GetQuaggansByIds(indices),
                progressSpy);

            var index = await quagganService.GetQuaggansIndex();

            var actual = new List<QuagganRef>();
            await foreach (var quaggan in sut.QueryAsync(index.Values, bufferSize))
            {
                if (quaggan.HasValue)
                {
                    actual.Add(quaggan.Value);
                }
            }

            Assert.Equal(index.Values.Count, progressSpy.Last.ResultTotal);
            Assert.Equal(index.Values.Count, progressSpy.Last.ResultCount);
            Assert.Equal(index.Values.Count, actual.Count);
            Assert.All(actual,
                quaggan =>
                {
                    index.Values.Contains(quaggan.Id);
                });
        }
    }
}
