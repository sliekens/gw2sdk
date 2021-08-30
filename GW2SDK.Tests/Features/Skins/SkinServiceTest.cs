using System;
using System.Threading.Tasks;
using GW2SDK.Skins;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class SkinServiceTest
    {
        [Fact]
        public async Task It_can_get_all_skin_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_skin_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            const int skinId = 1;

            var actual = await sut.GetSkinById(skinId);

            Assert.Equal(skinId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_skins_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetSkinsByIds(ids);

            Assert.Collection(actual.Values, skin => Assert.Equal(1, skin.Id), skin => Assert.Equal(2, skin.Id), skin => Assert.Equal(3, skin.Id));
        }

        [Fact]
        public async Task Skin_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentNullException>("skinIds",
                async () =>
                {
                    await sut.GetSkinsByIds(null);
                });
        }

        [Fact]
        public async Task Skin_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>("skinIds",
                async () =>
                {
                    await sut.GetSkinsByIds(Array.Empty<int>());
                });
        }

        [Fact]
        public async Task It_can_get_skins_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
