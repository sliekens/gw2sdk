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
        actual.Has_name();
        actual.Has_sector_id();
        actual.Has_map_id();
        actual.Has_chat_link();
    }
}
