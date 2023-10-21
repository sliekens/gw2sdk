using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Colors;

public class Colors
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColors();

        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            color =>
            {
                color.Base_rgb_contains_red_green_blue();
            }
        );
    }
}
