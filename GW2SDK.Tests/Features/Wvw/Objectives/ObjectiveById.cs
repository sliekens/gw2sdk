using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

public class ObjectiveById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1099-99";

        var (actual, _) = await sut.Wvw.GetObjectiveById(id);

        Assert.Equal(id, actual.Id);
    }
}
