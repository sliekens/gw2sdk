using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class SeasonById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "09766A86-D88D-4DF2-9385-259E9A8CA583";

        var (actual, _) = await sut.Stories.GetSeasonById(id);

        Assert.Equal(id, actual.Id);
    }
}
