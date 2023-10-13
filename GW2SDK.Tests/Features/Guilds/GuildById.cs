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

        var actual = await sut.Guilds.GetGuildById(guild.Id, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.Equal(guild.Id, actual.Value.Id);
        Assert.Equal(guild.Name, actual.Value.Name);
        Assert.Equal(guild.Tag, actual.Value.Tag);
    }
}
