using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class TraitById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 214;

        var (actual, _) = await sut.Builds.GetTraitById(id);

        Assert.Equal(id, actual.Id);
    }
}
