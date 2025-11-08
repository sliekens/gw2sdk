using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharacterSummary
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Accounts.CharacterSummary actual, _) = await sut.Hero.Account.GetCharacterSummary(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(character.Name, actual.Name);
        Assert.Equal(character.Race, actual.Race);
        Assert.Equal(character.Profession, actual.Profession);
        Assert.True(actual.Level > 0);
        Assert.True(actual.Age > TimeSpan.Zero);
        Assert.True(actual.LastModified > DateTimeOffset.MinValue);
        Assert.True(actual.Created > DateTimeOffset.MinValue);
    }
}
