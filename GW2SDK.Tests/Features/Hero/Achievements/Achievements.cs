using GuildWars2.Chat;
using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Achievements.Rewards;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class Achievements
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using var httpClient =
            new HttpClient(new JsonLinesHttpMessageHandler("Data/achievements.json.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var (actual, context) in sut.Hero.Achievements.GetAchievementsBulk())
        {
            Assert.NotNull(context);
            Assert.True(actual.Id > 0);
            Assert.NotEmpty(actual.Name);
            Assert.NotNull(actual.Description);
            MarkupSyntaxValidator.Validate(actual.Description);
            Assert.NotNull(actual.Requirement);
            MarkupSyntaxValidator.Validate(actual.Requirement);
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

            var chatLinkRoundtrip = AchievementLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
        }
    }
}
