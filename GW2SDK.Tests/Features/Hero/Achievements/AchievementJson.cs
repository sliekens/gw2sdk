using System.Text.Json;
using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Achievements.Rewards;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementJson(AchievementFixture fixture) : IClassFixture<AchievementFixture>
{
    [Fact]
    public void Achievements_can_be_created_from_json() =>
        Assert.All(
            fixture.Achievements,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetAchievement(MissingMemberBehavior.Error);

                Assert.True(actual.Id > 0);
                Assert.NotEmpty(actual.Name);
                Assert.NotNull(actual.Description);
                Assert.NotNull(actual.Requirement);
                Assert.NotNull(actual.LockedText);
                Assert.NotNull(actual.Flags);
                Assert.Empty(actual.Flags.Other);
                Assert.NotEmpty(actual.Tiers);
                Assert.DoesNotContain(null, actual.Tiers);
                if (actual.Rewards is not null)
                {
                    Assert.All(
                        actual.Rewards,
                        reward =>
                        {
                            Assert.NotNull(reward);
                            switch (reward)
                            {
                                case MasteryPointReward masteryPointReward:
                                    Assert.True(masteryPointReward.Id > 0);
                                    Assert.True(masteryPointReward.Region.IsDefined());
                                    break;
                                case CoinsReward coinsReward:
                                    Assert.True(coinsReward.Coins > Coin.Zero);
                                    break;
                                case ItemReward itemReward:
                                    Assert.True(itemReward.Id > 0);
                                    Assert.True(itemReward.Count > 0);
                                    break;
                                case TitleReward titleReward:
                                    Assert.True(titleReward.Id > 0);
                                    break;
                            }
                        }
                    );
                }

                if (actual.Bits is not null)
                {
                    Assert.DoesNotContain(null, actual.Bits);
                }

                if (actual.Flags.Repeatable && actual.Tiers.All(tier => tier.Points == 0))
                {
                    Assert.Equal(-1, actual.PointCap);
                }

                var chatLink = actual.GetChatLink();
                Assert.Equal(actual.Id, chatLink.AchievementId);
            }
        );
}
