using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Traits;
using Xunit;

namespace GW2SDK.Tests.Features.Traits
{
    public class TraitServiceTest
    {
        private static class TraitFact
        {
            public static void Id_is_positive(Trait actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        public async Task It_can_get_all_traits()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraits();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                trait =>
                {
                    TraitFact.Id_is_positive(trait);
                });
        }

        [Fact]
        public async Task It_can_get_all_trait_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraitsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_trait_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            const int traitId = 214;

            var actual = await sut.GetTraitById(traitId);

            Assert.Equal(traitId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_traits_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var ids = new HashSet<int>
            {
                214,
                221,
                222
            };

            var actual = await sut.GetTraitsByIds(ids);

            Assert.Collection(actual.Values, first => Assert.Equal(214, first.Id), second => Assert.Equal(221, second.Id), third => Assert.Equal(222, third.Id));
        }

        [Fact]
        public async Task It_can_get_traits_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraitsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
