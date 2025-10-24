using GuildWars2.Tests.TestInfrastructure;

using GuildWars2.Wvw.Matches.Scores;


namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchesScores
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<MatchScores> actual, MessageContext context) = await sut.Wvw.GetMatchesScores(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.All(entry.Skirmishes, skirmish =>
            {
                Assert.True(skirmish.Id > 0);
                Assert.All(skirmish.MapScores, score =>
                {
                    Assert.True(score.Kind.IsDefined());
                });
            });
            Assert.All(entry.Maps, map =>
            {
                Assert.True(map.Id > 0);
                Assert.True(map.Kind.IsDefined());
            });
        });
    }
}
