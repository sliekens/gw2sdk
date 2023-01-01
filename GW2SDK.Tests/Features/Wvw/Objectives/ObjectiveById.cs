using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

public class ObjectiveById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string objectiveId = "1099-99";

        var actual = await sut.Wvw.GetObjectiveById(objectiveId);

        Assert.Equal(objectiveId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_sector_id();
        actual.Value.Has_map_id();
        actual.Value.Has_chat_link();
    }
}
