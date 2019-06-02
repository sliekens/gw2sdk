using System.Linq;
using GW2SDK.Features.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using GW2SDK.Tests.Shared;
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
        public void AllMembers_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            Assert.All(_fixture.Db.Achievements,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Achievement>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.InRange(actual.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotEmpty(actual.Name));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Description_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotNull(actual.Description));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Description_CouldBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Description == "");
            Assert.Contains(achievements, actual => actual.Description != "");
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Requirement_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotNull(actual.Requirement));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Requirement_CouldBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Description.Length == 0);
            Assert.Contains(achievements, actual => actual.Description.Length != 0);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void LockedText_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotNull(actual.LockedText));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void LockedText_CouldBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.LockedText.Length == 0);
            Assert.Contains(achievements, actual => actual.LockedText.Length != 0);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Flags_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotEmpty(actual.Flags));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Tiers_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.NotEmpty(actual.Tiers));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Tiers_ShouldNotContainNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements, actual => Assert.DoesNotContain(null, actual.Tiers));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_CouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Rewards is null);
            Assert.Contains(achievements, actual => actual.Rewards is object);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_WhenNotNull_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
                actual =>
                {
                    if (actual.Rewards is object)
                    {
                        Assert.NotEmpty(actual.Rewards);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_WhenNotNull_ShouldNotContainNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
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
        public void Rewards_OfTypeTitleReward_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var titleRewards = achievements.Where(achievement => achievement.Rewards is object)
                                           .SelectMany(achievement => achievement.Rewards.OfType<TitleReward>());

            Assert.All(titleRewards,
                actual =>
                {
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_OfTypeMasteryReward_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var masteryRewards = achievements.Where(achievement => achievement.Rewards is object)
                                             .SelectMany(achievement => achievement.Rewards.OfType<MasteryReward>());

            Assert.All(masteryRewards,
                actual =>
                {
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_OfTypeItemReward_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var itemRewards = achievements.Where(achievement => achievement.Rewards is object)
                                          .SelectMany(achievement => achievement.Rewards.OfType<ItemReward>());

            Assert.All(itemRewards,
                actual =>
                {
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_OfTypeItemReward_Count_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var itemRewards = achievements.Where(achievement => achievement.Rewards is object)
                                          .SelectMany(achievement => achievement.Rewards.OfType<ItemReward>());

            Assert.All(itemRewards,
                actual =>
                {
                    Assert.InRange(actual.Count, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Rewards_OfTypeCoinsReward_Count_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var itemRewards = achievements.Where(achievement => achievement.Rewards is object)
                                          .SelectMany(achievement => achievement.Rewards.OfType<CoinsReward>());

            Assert.All(itemRewards,
                actual =>
                {
                    Assert.InRange(actual.Count, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_CouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Bits is null);
            Assert.Contains(achievements, actual => actual.Bits is object);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_WhenNotNull_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
                actual =>
                {
                    if (actual.Bits is object)
                    {
                        Assert.NotEmpty(actual.Bits);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_WhenNotNull_ShouldNotContainNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
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
        public void Bits_OfTypeAchievementTextBit_Text_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var textBits = achievements.Where(achievement => achievement.Bits is object)
                                       .SelectMany(achievement => achievement.Bits.OfType<AchievementTextBit>());

            Assert.All(textBits, actual => Assert.NotNull(actual.Text));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_OfTypeAchievementMinipetBit_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var textBits = achievements.Where(achievement => achievement.Bits is object)
                                       .SelectMany(achievement => achievement.Bits.OfType<AchievementMinipetBit>());

            Assert.All(textBits, actual => Assert.InRange(actual.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_OfTypeAchievementItemBit_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var textBits = achievements.Where(achievement => achievement.Bits is object)
                                       .SelectMany(achievement => achievement.Bits.OfType<AchievementItemBit>());

            Assert.All(textBits, actual => Assert.InRange(actual.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Bits_OfTypeAchievementSkinBit_Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            var textBits = achievements.Where(achievement => achievement.Bits is object)
                                       .SelectMany(achievement => achievement.Bits.OfType<AchievementSkinBit>());

            Assert.All(textBits, actual => Assert.InRange(actual.Id, 1, int.MaxValue));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Prerequisites_CouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Prerequisites is null);
            Assert.Contains(achievements, actual => actual.Prerequisites is object);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Prerequisites_WhenNotNull_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
                actual =>
                {
                    if (actual.Prerequisites is object)
                    {
                        Assert.NotEmpty(actual.Prerequisites);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void PointCap_CouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => !actual.PointCap.HasValue);
            Assert.Contains(achievements, actual => actual.PointCap.HasValue);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void PointCap_WhenNotNull_ShouldBePositiveOrMinusOne()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
                actual =>
                {
                    if (actual.PointCap.HasValue && actual.PointCap.Value != -1)
                    {
                        Assert.InRange(actual.PointCap.Value, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Icon_CouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.Contains(achievements, actual => actual.Icon is null);
            Assert.Contains(achievements, actual => actual.Icon is object);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Icon_WhenNotNull_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievements = _fixture.Db.Achievements.Select(json => JsonConvert.DeserializeObject<Achievement>(json, settings)).ToList();

            Assert.All(achievements,
                actual =>
                {
                    if (actual.Icon is object)
                    {
                        Assert.NotEmpty(actual.Icon);
                    }
                });
        }
    }
}
