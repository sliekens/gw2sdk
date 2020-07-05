using System;
using System.Threading.Tasks;
using GW2SDK.Titles;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Titles
{
    public class TitleServiceTest
    {
        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_titles()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitles();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_title_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitlesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_title_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            const int titleId = 1;

            var actual = await sut.GetTitleById(titleId);

            Assert.Equal(titleId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_titles_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetTitlesByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Unit")]
        public async Task Title_ids_cannot_be_null()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            await Assert.ThrowsAsync<ArgumentNullException>("titleIds",
                async () =>
                {
                    await sut.GetTitlesByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Unit")]
        public async Task Title_ids_cannot_be_empty()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            await Assert.ThrowsAsync<ArgumentException>("titleIds",
                async () =>
                {
                    await sut.GetTitlesByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_titles_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitlesByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetTitlesByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Titles")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<TitleService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetTitlesByPage(1, -3));
        }
    }
}
