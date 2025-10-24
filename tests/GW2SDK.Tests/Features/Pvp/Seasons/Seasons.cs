using GuildWars2.Pvp.Seasons;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class Seasons
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Season> actual, MessageContext context) = await sut.Pvp.GetSeasons(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Name);
            if (entry.Leaderboards.Legendary is not null)
            {
                Assert.Empty(entry.Leaderboards.Legendary.Settings.Name);
                Assert.NotEmpty(entry.Leaderboards.Legendary.Settings.ScoringId);
                Assert.All(entry.Leaderboards.Legendary.Settings.Tiers, tier =>
                {
                    Assert.Empty(tier.Name);
                    if (tier.Kind.HasValue)
                    {
                        Assert.True(tier.Kind.Value.IsDefined());
                    }
                });
            }

            if (entry.Leaderboards.Guild is not null)
            {
                Assert.Empty(entry.Leaderboards.Guild.Settings.Name);
                Assert.NotEmpty(entry.Leaderboards.Guild.Settings.ScoringId);
                Assert.All(entry.Leaderboards.Guild.Settings.Tiers, tier =>
                {
                    Assert.NotEmpty(tier.Name);
                    if (tier.Kind.HasValue)
                    {
                        Assert.True(tier.Kind.Value.IsDefined());
                    }
                });
            }

            if (entry.Leaderboards.Ladder is not null)
            {
                Assert.Empty(entry.Leaderboards.Ladder.Settings.Name);
                Assert.NotEmpty(entry.Leaderboards.Ladder.Settings.ScoringId);
                Assert.All(entry.Leaderboards.Ladder.Settings.Tiers, tier =>
                {
                    Assert.Empty(tier.Name);
                    if (tier.Kind.HasValue)
                    {
                        Assert.True(tier.Kind.Value.IsDefined());
                    }
                });
            }
        });
    }
}
