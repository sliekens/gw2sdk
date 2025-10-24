using GuildWars2.Hero.Accounts;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharactersByNames
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        TestCharacter character = TestConfiguration.TestCharacter;

        TestCharacter character2 = TestConfiguration.TestCharacter2;

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<Character> actual, _) = await sut.Hero.Account.GetCharactersByNames([character.Name, character2.Name], accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Collection(actual, entry =>
        {
            Assert.Equal(character.Name, entry.Name);
            Assert.Equal(character.Race, entry.Race);
            Assert.Equal(character.Profession, entry.Profession);
        }, entry =>
        {
            Assert.Equal(character2.Name, entry.Name);
            Assert.Equal(character2.Race, entry.Race);
            Assert.Equal(character2.Profession, entry.Profession);
        });
    }
}
