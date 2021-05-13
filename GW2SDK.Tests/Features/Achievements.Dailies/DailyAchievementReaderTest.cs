using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Achievements.Dailies.Fixtures;
using Xunit;
using static GW2SDK.ProductName;

namespace GW2SDK.Tests.Features.Achievements.Dailies
{
    public class DailyAchievementReaderTest : IClassFixture<DailyAchievementFixture>
    {
        private readonly DailyAchievementFixture _fixture;

        public DailyAchievementReaderTest(DailyAchievementFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(Day.Today)]
        [InlineData(Day.Tomorrow)]
        public void Daily_achievement_group_can_be_created_from_json(Day day)
        {
            var sut = new DailyAchievementReader();

            var json = day switch
            {
                Day.Today => _fixture.Today,
                Day.Tomorrow => _fixture.Tomorrow,
                _ => throw new ArgumentOutOfRangeException(nameof(day), day, null)
            };

            using var document = JsonDocument.Parse(json);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            Assert.NotNull(actual);
            Assert.All(actual.Pve, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Pvp, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Wvw, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Fractals, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Special, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Pve, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Pvp, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Wvw, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Fractals, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Special, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Pve, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Pvp, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Wvw, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Fractals, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Special, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Pve, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Pvp, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Wvw, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Fractals, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Special, DailyAchievementFact.Can_have_a_product_requirement);
        }

        private static class DailyAchievementFact
        {
            public static void Id_is_positive(DailyAchievement actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Min_level_is_between_1_and_80(DailyAchievement actual) => Assert.InRange(actual.Level.Min, 1, 80);

            public static void Max_level_is_between_1_and_80(DailyAchievement actual) => Assert.InRange(actual.Level.Max, 1, 80);

            public static void Can_have_a_product_requirement(DailyAchievement actual)
            {
                if (actual.RequiredAccess is object)
                {
                    Assert.Subset(new HashSet<ProductName> { HeartOfThorns, PathOfFire },
                        new HashSet<ProductName> { actual.RequiredAccess.Product });

                    Assert.True(Enum.IsDefined(typeof(AccessCondition), actual.RequiredAccess.Condition));
                }
            }
        }
    }
}
