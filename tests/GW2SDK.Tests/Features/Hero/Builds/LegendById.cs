using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class LegendById
{
    [Test]
    [Arguments("Legend1")]
    [Arguments("Legend2")]
    [Arguments("Legend3")]
    public async Task Can_be_found(string id)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (Legend actual, MessageContext context) = await sut.Hero.Builds.GetLegendById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
