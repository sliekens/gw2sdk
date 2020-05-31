﻿using System;
using System.Threading.Tasks;
using GW2SDK.Skins;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    public class SkinServiceTest
    {
        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task Get_all_skin_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task Get_a_skin_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            const int skinId = 1;

            var actual = await sut.GetSkinById(skinId);

            Assert.Equal(skinId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task Get_skins_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetSkinsByIds(ids);

            Assert.Collection(actual, skin => Assert.Equal(1, skin.Id), skin => Assert.Equal(2, skin.Id), skin => Assert.Equal(3, skin.Id));
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public async Task Skin_ids_cannot_be_null()
        {
            await using var services = new Container();
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
        public async Task Skin_ids_cannot_be_empty()
        {
            await using var services = new Container();
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
        public async Task Get_skins_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            var actual = await sut.GetSkinsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetSkinsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<SkinService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetSkinsByPage(1, -3));
        }
    }
}
