using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Masteries;

public class MasteryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Masteries.GetMasteryById(id);

        Assert.Equal(id, actual.Id);
    }
}
