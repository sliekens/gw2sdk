using GuildWars2.Guilds;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Guilds;

[ServiceDataSource]
public class GuildById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestGuild guild = TestConfiguration.TestGuild;
        (Guild actual, MessageContext context) = await sut.Guilds.GetGuildById(guild.Id, null, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(guild.Id);
        await Assert.That(actual.Level).IsNull();
        await Assert.That(actual.MessageOfTheDay).IsNull();
        await Assert.That(actual.Influence).IsNull();
        await Assert.That(actual.Aetherium).IsNull();
        await Assert.That(actual.Resonance).IsNull();
        await Assert.That(actual.Favor).IsNull();
        await Assert.That(actual.MemberCapacity).IsNull();
        await Assert.That(actual.MemberCount).IsNull();
    }

    [Test]
    public async Task Can_be_found_when_authenticated()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        TestGuild guild = TestConfiguration.TestGuild;
        (Guild actual, MessageContext context) = await sut.Guilds.GetGuildById(guild.Id, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(guild.Id);
        await Assert.That(actual.Level).IsNotNull();
        await Assert.That(actual.MessageOfTheDay).IsNotNull();
        await Assert.That(actual.Influence).IsNotNull();
        await Assert.That(actual.Aetherium).IsNotNull();
        await Assert.That(actual.Resonance).IsNotNull();
        await Assert.That(actual.Favor).IsNotNull();
        await Assert.That(actual.MemberCapacity).IsNotNull();
        await Assert.That(actual.MemberCount).IsNotNull();
    }
}
