using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationCategoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, context) = await sut.Pve.Home.GetDecorationCategoryById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
