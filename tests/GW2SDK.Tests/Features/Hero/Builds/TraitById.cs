using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class TraitById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 214;
        (Trait actual, MessageContext context) = await sut.Hero.Builds.GetTraitById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
