using System;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Traits;
using Xunit;

namespace GW2SDK.Tests.Features.Traits
{
    public class TraitServiceTest
    {
        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_traits()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraits();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_trait_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraitsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_trait_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            const int traitId = 214;

            var actual = await sut.GetTraitById(traitId);

            Assert.Equal(traitId, actual.Id);
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_traits_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var ids = new[]
            {
                214,
                221,
                222
            };

            var actual = await sut.GetTraitsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(214, first.Id), second => Assert.Equal(221, second.Id), third => Assert.Equal(222, third.Id));
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Unit")]
        public async Task Trait_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            await Assert.ThrowsAsync<ArgumentNullException>("traitIds",
                async () =>
                {
                    await sut.GetTraitsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Unit")]
        public async Task Trait_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            await Assert.ThrowsAsync<ArgumentException>("traitIds",
                async () =>
                {
                    await sut.GetTraitsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature", "Traits")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_traits_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TraitService>();

            var actual = await sut.GetTraitsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
