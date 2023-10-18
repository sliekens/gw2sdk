using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildBank
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = Composer.Resolve<TestGuildLeader>();

        var actual = await sut.Guilds.GetGuildBank(guildLeader.Id, guildLeader.Token);

        Assert.NotNull(actual.Value);
    }
}
