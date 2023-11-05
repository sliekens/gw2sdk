using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Colors;

public class ColorById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Dyes.GetColorById(id);

        Assert.Equal(id, actual.Id);
    }
}
