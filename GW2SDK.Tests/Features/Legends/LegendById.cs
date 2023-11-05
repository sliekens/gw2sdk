using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Legends;

public class LegendById
{
    [Theory]
    [InlineData("Legend1")]
    [InlineData("Legend2")]
    [InlineData("Legend3")]
    public async Task Can_be_found(string id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Legends.GetLegendById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_code();
    }
}
