using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items.Stats;

public class AttributeCombinationById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 559;

        var (actual, context) = await sut.Items.GetAttributeCombinationById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
