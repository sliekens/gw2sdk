using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;
using static GW2SDK.ProductName;

namespace GW2SDK.Tests.Features.Achievements.Dailies
{
    public class DailyAchievementServiceTest
    {
        private static class DailyAchievementFact
        {
            public static void Id_is_positive(DailyAchievement actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Min_level_is_between_1_and_80(DailyAchievement actual) =>
                Assert.InRange(actual.Level.Min, 1, 80);

            public static void Max_level_is_between_1_and_80(DailyAchievement actual) =>
                Assert.InRange(actual.Level.Max, 1, 80);
            
            public static void Can_have_a_product_requirement(DailyAchievement actual)
            {
                if (actual.RequiredAccess is object)
                {
                    Assert.Subset(new HashSet<ProductName>
                        {
                            HeartOfThorns,
                            PathOfFire
                        },
                        new HashSet<ProductName> { actual.RequiredAccess.Product });

                    Assert.True(Enum.IsDefined(typeof(AccessCondition), actual.RequiredAccess.Condition));
                }
            }
        }

        [Theory(Skip = "Daily achievements are not working right...")]
        [InlineData(Day.Today)]
        [InlineData(Day.Tomorrow)]
        public async Task It_can_get_get_daily_achievements(Day day)
        {
            await using var services = new Composer();
            var sut = services.Resolve<DailyAchievementService>();

            var actual = await sut.GetDailyAchievements(day);

            Assert.True(actual.HasValue);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Special, DailyAchievementFact.Id_is_positive);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Special, DailyAchievementFact.Min_level_is_between_1_and_80);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Special, DailyAchievementFact.Max_level_is_between_1_and_80);
            Assert.All(actual.Value.Pve, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Pvp, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Wvw, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Fractals, DailyAchievementFact.Can_have_a_product_requirement);
            Assert.All(actual.Value.Special, DailyAchievementFact.Can_have_a_product_requirement);
        }
    }
}
