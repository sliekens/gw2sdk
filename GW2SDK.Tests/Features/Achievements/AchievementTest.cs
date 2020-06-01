using System.Linq;
using GW2SDK.Achievements;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementTest
    {
        public AchievementTest(AchievementFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AchievementFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievements_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.Db.Achievements,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Achievement>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Name_is_never_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements, actual => Assert.NotEmpty(actual.Name));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Description_can_be_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Description == "");
            Assert.Contains(achievements, actual => actual.Description != "");
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Requirement_can_be_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Requirement == "");
            Assert.Contains(achievements, actual => actual.Requirement != "");
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void LockedText_can_be_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.LockedText == "");
            Assert.Contains(achievements, actual => actual.LockedText != "");
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Flags_is_never_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements, actual => Assert.NotEmpty(actual.Flags));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Tiers_is_never_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements, actual => Assert.NotEmpty(actual.Tiers));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Tiers_never_contains_null()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements, actual => Assert.DoesNotContain(null, actual.Tiers));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Subset_of_achievements_has_no_rewards()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToHashSet();

            var withoutRewards = achievements.Where(achievement => achievement.Rewards is null).ToHashSet();

            Assert.ProperSubset(achievements, withoutRewards);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_never_contains_null()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements,
                actual =>
                {
                    if (actual.Rewards is object)
                    {
                        Assert.DoesNotContain(null, actual.Rewards);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Subset_of_achievements_has_bits()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToHashSet();

            var withBits = achievements.Where(achievement => achievement.Bits is object && achievement.Bits.Length != 0).ToHashSet();

            Assert.ProperSubset(achievements, withBits);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_never_contains_null()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            AssertEx.ForEach(achievements,
                actual =>
                {
                    if (actual.Bits is object)
                    {
                        Assert.DoesNotContain(null, actual.Bits);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Subset_of_achievements_has_prerequisites()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToHashSet();

            var withPrerequisites = achievements.Where(achievement => achievement.Prerequisites is object && achievement.Prerequisites.Length != 0).ToHashSet();

            Assert.ProperSubset(achievements, withPrerequisites);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Subset_of_achievements_has_a_point_cap()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToHashSet();

            var withPointCap = achievements.Where(achievement => achievement.PointCap.HasValue).ToHashSet();

            Assert.ProperSubset(achievements, withPointCap);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void PointCap_is_negative_1_for_repeatable_achievements_without_points()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var pointlessRepetition =
                achievements.Where(a => a.Flags.Contains(AchievementFlag.Repeatable) && a.Tiers.All(tier => tier.Points == 0)).ToHashSet();
            AssertEx.ForEach(pointlessRepetition,
                actual =>
                {
                    Assert.Equal(-1, actual.PointCap);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Subset_of_achievements_has_no_icon()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToHashSet();

            var withoutIcon = achievements.Where(achievement => achievement.Icon is null).ToHashSet();

            Assert.ProperSubset(achievements, withoutIcon);
        }
    }
}
