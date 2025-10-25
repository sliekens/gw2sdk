using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault.Objectives;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class Objectives
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Objective> actual, MessageContext context) = await sut.WizardsVault.GetObjectives(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, objective =>
        {
            Assert.True(objective.Id > 0);
            Assert.NotEmpty(objective.Title);
            Assert.True(objective.Track.IsDefined());
            Assert.True(objective.RewardAcclaim > 0);
        });
    }
}
