using GW2SDK.Achievements.Groups;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Achievements.Groups.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
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
        [Trait("Feature",    "Achievements.Groups")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_groups_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.Db.AchievementGroups,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Groups")]
        [Trait("Category", "Integration")]
        public void Order_is_not_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.AchievementGroups,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<AchievementGroup>(json, settings);
                    Assert.InRange(actual.Order, 0, int.MaxValue);
                });
        }
    }
}
