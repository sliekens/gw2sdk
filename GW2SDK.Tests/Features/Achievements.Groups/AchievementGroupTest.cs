using System;
using GW2SDK.Features.Achievements.Groups;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Achievements.Groups.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    [Collection(nameof(AchievementGroupDbCollection))]
    public class AchievementGroupTest
    {
        public AchievementGroupTest(AchievementGroupFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AchievementGroupFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Achievement.Groups")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature", "Achievement.Groups")]
        [Trait("Category", "Integration")]
        public void Id_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.NotNull(actual.Id);
                });
        }

        [Fact]
        [Trait("Feature", "Achievement.Groups")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.NotNull(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature", "Achievement.Groups")]
        [Trait("Category", "Integration")]
        public void Description_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.NotNull(actual.Description);
                });
        }

        [Fact]
        [Trait("Feature", "Achievement.Groups")]
        [Trait("Category", "Integration")]
        public void Order_ShouldNotBeNegative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.InRange(actual.Order, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature", "Achievement.Groups")]
        [Trait("Category", "Integration")]
        public void Categories_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementCategories,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.NotNull(actual.Categories);
                });
        }
    }
}
