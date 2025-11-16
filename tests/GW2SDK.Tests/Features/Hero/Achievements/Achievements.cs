using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Achievements.Rewards;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class Achievements
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/achievements.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((Achievement actual, MessageContext context) in sut.Hero.Achievements.GetAchievementsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual)
                .Member(a => a.Id, id => id.IsGreaterThan(0))
                .And.Member(a => a.Name, name => name.IsNotEmpty())
                .And.Member(a => a.Description, desc => desc.IsNotNull());
            MarkupSyntaxValidator.Validate(actual.Description);
            await Assert.That(actual)
                .Member(a => a.Requirement, req => req.IsNotNull())
                .And.Member(a => a.LockedText, locked => locked.IsNotNull())
                .And.Member(a => a.Flags, flags => flags.IsNotNull())
                .And.Member(a => a.Tiers, tiers => tiers.IsNotEmpty().And.DoesNotContain(item => item == null));
            MarkupSyntaxValidator.Validate(actual.Requirement);
            await Assert.That(actual.Flags.Other).IsEmpty();
            if (actual.Rewards is not null)
            {
                foreach (AchievementReward reward in actual.Rewards)
                {
                    await Assert.That(reward).IsNotNull();
                    switch (reward)
                    {
                        case MasteryPointReward masteryPointReward:
                            await Assert.That(masteryPointReward.Id).IsGreaterThan(0);
                            await Assert.That(masteryPointReward.Region.IsDefined()).IsTrue();
                            break;
                        case CoinsReward coinsReward:
                            await Assert.That(coinsReward.Coins).IsGreaterThan(Coin.Zero);
                            break;
                        case ItemReward itemReward:
                            await Assert.That(itemReward)
                                .Member(r => r.Id, id => id.IsGreaterThan(0))
                                .And.Member(r => r.Count, count => count.IsGreaterThan(0));
                            break;
                        case TitleReward titleReward:
                            await Assert.That(titleReward.Id).IsGreaterThan(0);
                            break;
                        default:
                            throw new TUnit.Assertions.Exceptions.AssertionException($"Unexpected reward type: {reward.GetType().Name}");
                    }
                }
            }

            if (actual.Bits is not null)
            {
                await Assert.That(actual.Bits).DoesNotContain(item => item == null);
            }

            if (actual.Flags.Repeatable && actual.Tiers.All(tier => tier.Points == 0))
            {
                await Assert.That(actual.PointCap).IsEqualTo(-1);
            }

            AchievementLink chatLink = actual.GetChatLink();
            await Assert.That(chatLink.AchievementId).IsEqualTo(actual.Id);
            AchievementLink chatLinkRoundtrip = AchievementLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
#if NET
            string json = JsonSerializer.Serialize(actual, Common.TestJsonContext.Default.Achievement);
            Achievement? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Achievement);
#else
            string json = JsonSerializer.Serialize(actual);
            Achievement? roundTrip = JsonSerializer.Deserialize<Achievement>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(actual);
        }
    }
}
