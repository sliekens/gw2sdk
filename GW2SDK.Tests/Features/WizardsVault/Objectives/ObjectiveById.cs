using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.WizardsVault;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class ObjectiveById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.WizardsVault.GetObjectiveById(id);

        Assert.Equal(id, actual.Id);
        Assert.NotEmpty(actual.Title);
        Assert.True(Enum.IsDefined(typeof(ObjectiveTrack), actual.Track));
        Assert.True(actual.Acclaim > 0);
    }
}
