using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.Objectives;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class Objectives(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Objective> actual, MessageContext context) = await sut.WizardsVault.GetObjectives(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
