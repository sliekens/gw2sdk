using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class LegendById
{
    [Theory]
    [InlineData("Legend1")]
    [InlineData("Legend2")]
    [InlineData("Legend3")]
    public async Task Can_be_found(string id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetLegendById(id, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
