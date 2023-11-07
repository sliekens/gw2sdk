using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Novelties;

public class NoveltyById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Hero.Novelties.GetNoveltyById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_description();
        actual.Has_icon();
        actual.Has_slot();
        actual.Has_unlock_items();
    }
}
