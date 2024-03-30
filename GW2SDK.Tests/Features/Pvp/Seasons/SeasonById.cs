using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class SeasonById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "2B2E80D3-0A74-424F-B0EA-E221500B323C";

        var (actual, _) = await sut.Pvp.GetSeasonById(id);

        Assert.Equal(id, actual.Id);
    }
}
