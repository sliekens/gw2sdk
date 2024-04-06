using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();
        var guild = Composer.Resolve<TestGuild>();

        var (actual, context) = await sut.Guilds.GetGuildById(guild.Id, accessToken.Key);

        Assert.NotNull(context);
        Assert.Equal(guild.Id, actual.Id);
    }
}
