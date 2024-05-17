using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guild = Composer.Resolve<TestGuild>();

        var (actual, context) = await sut.Guilds.GetGuildById(guild.Id, null);

        Assert.NotNull(context);
        Assert.Equal(guild.Id, actual.Id);
        Assert.Null(actual.Level);
        Assert.Null(actual.MessageOfTheDay);
        Assert.Null(actual.Influence);
        Assert.Null(actual.Aetherium);
        Assert.Null(actual.Resonance);
        Assert.Null(actual.Favor);
        Assert.Null(actual.MemberCapacity);
        Assert.Null(actual.MemberCount);
    }


    [Fact]
    public async Task Can_be_found_when_authenticated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();
        var guild = Composer.Resolve<TestGuild>();

        var (actual, context) = await sut.Guilds.GetGuildById(guild.Id, accessToken.Key);

        Assert.NotNull(context);
        Assert.Equal(guild.Id, actual.Id);
        Assert.NotNull(actual.Level);
        Assert.NotNull(actual.MessageOfTheDay);
        Assert.NotNull(actual.Influence);
        Assert.NotNull(actual.Aetherium);
        Assert.NotNull(actual.Resonance);
        Assert.NotNull(actual.Favor);
        Assert.NotNull(actual.MemberCapacity);
        Assert.NotNull(actual.MemberCount);
    }
}
