using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildStorage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = Composer.Resolve<TestGuildLeader>();

        var actual = await sut.Guilds.GetGuildStorage(guildLeader.Id, guildLeader.Token);

        Assert.NotNull(actual.Value);
    }
}
