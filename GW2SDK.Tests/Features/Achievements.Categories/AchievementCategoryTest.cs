using GW2SDK.Achievements.Categories;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Achievements.Categories.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    [Collection(nameof(AchievementCategoryDbCollection))]
    public class AchievementCategoryTest
    {
        public AchievementCategoryTest(AchievementCategoryFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AchievementCategoryFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Achievements.Categories")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_categories_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public void Order_is_not_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.InRange(actual.Order, 0, int.MaxValue);
                });
        }
    }
}
