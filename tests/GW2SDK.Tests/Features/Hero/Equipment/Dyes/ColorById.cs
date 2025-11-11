using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

[ServiceDataSource]
public class ColorById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (DyeColor actual, MessageContext context) = await sut.Hero.Equipment.Dyes.GetColorById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
