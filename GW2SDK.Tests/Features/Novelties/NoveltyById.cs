using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Novelties;

public class NoveltyById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int noveltyId = 1;

        var actual = await sut.Novelties.GetNoveltyById(noveltyId);

        Assert.Equal(noveltyId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_icon();
        actual.Value.Has_slot();
        actual.Value.Has_unlock_items();
    }
}
