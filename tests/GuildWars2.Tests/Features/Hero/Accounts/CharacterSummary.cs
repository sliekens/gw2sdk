using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class CharacterSummary(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Accounts.CharacterSummary actual, _) = await sut.Hero.Account.GetCharacterSummary(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Name).IsEqualTo(character.Name);
        await Assert.That(actual.Race).IsEqualTo(character.Race);
        await Assert.That(actual.Profession).IsEqualTo(character.Profession);
        await Assert.That(actual.Level > 0).IsTrue();
        await Assert.That(actual.Age > TimeSpan.Zero).IsTrue();
        await Assert.That(actual.LastModified > DateTimeOffset.MinValue).IsTrue();
        await Assert.That(actual.Created > DateTimeOffset.MinValue).IsTrue();
    }
}
