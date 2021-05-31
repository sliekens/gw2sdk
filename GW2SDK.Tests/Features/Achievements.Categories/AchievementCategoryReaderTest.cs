using System.Text.Json;
using GW2SDK.Achievements.Categories;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Achievements.Categories.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class AchievementCategoryReaderTest : IClassFixture<AchievementCategoryFixture>
    {
        public AchievementCategoryReaderTest(AchievementCategoryFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementCategoryFixture _fixture;

        private static class AchievementCategoryFact
        {
            public static void Id_is_positive(AchievementCategory actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
           
            public static void Order_is_not_negative(AchievementCategory actual) => Assert.InRange(actual.Order, 0, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Achievements.Categories")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_categories_can_be_created_from_json()
        {
            var sut = new AchievementCategoryReader();

            AssertEx.ForEach(_fixture.AchievementCategories,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);
                    
                    AchievementCategoryFact.Id_is_positive(actual);
                    AchievementCategoryFact.Order_is_not_negative(actual);
                });
        }
    }
}
