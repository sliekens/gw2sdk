using GW2SDK.Features.Achievements.Categories;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Achievements.Categories.Fixtures;
using GW2SDK.Tests.Shared;
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
        [Trait("Feature",    "Achievement.Categories")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldHaveNoMissingMembers()
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
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.NotNull(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Description_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.NotNull(actual.Description);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Order_ShouldNotBeNegative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.InRange(actual.Order, 0, int.MaxValue);
                });
        }
        
        [Fact]
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Icon_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.NotNull(actual.Icon);
                });
        }
        
        [Fact]
        [Trait("Feature",  "Achievement.Categories")]
        [Trait("Category", "Integration")]
        public void Achievements_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementCategory>(json, settings);
                    Assert.NotNull(actual.Achievements);
                });
        }
    }
}
