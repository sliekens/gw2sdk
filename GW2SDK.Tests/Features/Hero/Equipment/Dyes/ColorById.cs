using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

public class ColorById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (DyeColor actual, MessageContext context) = await sut.Hero.Equipment.Dyes.GetColorById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
