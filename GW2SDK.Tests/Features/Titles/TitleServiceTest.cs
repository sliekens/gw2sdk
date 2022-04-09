using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Titles;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Titles
{
    public class TitleServiceTest
    {
        private static class TitleFact
        {
            public static void Id_is_positive(Title actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(Title actual) => Assert.NotEmpty(actual.Name);

            public static void Can_be_unlocked_by_achievements_or_achievement_points(Title actual)
            {
                if (actual.AchievementPointsRequired.HasValue)
                {
                    Assert.InRange(actual.AchievementPointsRequired.Value, 1, 100000);
                }
                else
                {
                    Assert.NotEmpty(actual.Achievements!);
                }
            }
        }

        [Fact]
        public async Task It_can_get_all_titles()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitles();

            Assert.Equal(actual.Context.ResultTotal, actual.Count);
            Assert.All(actual,
                title =>
                {
                    TitleFact.Id_is_positive(title);
                    TitleFact.Name_is_not_empty(title);
                    TitleFact.Can_be_unlocked_by_achievements_or_achievement_points(title);
                });
        }

        [Fact]
        public async Task It_can_get_all_title_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitlesIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Count);
        }

        [Fact]
        public async Task It_can_get_a_title_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TitleService>();

            const int titleId = 1;

            var actual = await sut.GetTitleById(titleId);

            Assert.Equal(titleId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_titles_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TitleService>();

            var ids = new HashSet<int> { 1, 2, 3 };

            var actual = await sut.GetTitlesByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        public async Task It_can_get_titles_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TitleService>();

            var actual = await sut.GetTitlesByPage(0, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
