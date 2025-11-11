using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

[ServiceDataSource]
public class SkiffSkinById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 410;
        (SkiffSkin actual, MessageContext context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkinById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
