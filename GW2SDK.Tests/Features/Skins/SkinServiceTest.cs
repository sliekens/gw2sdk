using System;
using System.Threading.Tasks;
using GW2SDK.Features.Skins;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class SkinServiceTest
    {
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinById_ShouldReturnThatSkin()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            const int skinId = 1;

            var actual = await sut.GetSkinById(skinId);

            Assert.Equal(skinId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public async Task GetSkinsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentNullException>("skinIds",
                async () =>
                {
                    await sut.GetSkinsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public async Task GetSkinsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>("skinIds",
                async () =>
                {
                    await sut.GetSkinsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinsByIds_ShouldReturnThoseSkins()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetSkinsByIds(ids);

            Assert.Collection(actual, skin => Assert.Equal(1, skin.Id), skin => Assert.Equal(2, skin.Id), skin => Assert.Equal(3, skin.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetSkinsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetSkinsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task GetSkinsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
