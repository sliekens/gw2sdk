using GuildWars2.Guilds;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestGuild guild = TestConfiguration.TestGuild;

        (Guild actual, MessageContext context) = await sut.Guilds.GetGuildById(
            guild.Id,
            null,
            cancellationToken: TestContext.Current.CancellationToken
        );

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
        ApiKey accessToken = TestConfiguration.ApiKey;
        TestGuild guild = TestConfiguration.TestGuild;

        (Guild actual, MessageContext context) = await sut.Guilds.GetGuildById(
            guild.Id,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

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
