using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

[ServiceDataSource]
public class GliderSkinById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 58;
        (GliderSkin actual, MessageContext context) = await sut.Hero.Equipment.Gliders.GetGliderSkinById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
