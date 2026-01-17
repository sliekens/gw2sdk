using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class CharactersByNames(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        TestCharacter character2 = TestConfiguration.TestCharacter2;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<Character> actual, _) = await sut.Hero.Account.GetCharactersByNames([character.Name, character2.Name], accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).Count().IsEqualTo(2);

        Character first = actual.Single(c => c.Name == character.Name);
        await Assert.That(first)
            .Member(entry => entry.Race, race => race.IsEqualTo(character.Race))
            .And.Member(entry => entry.Profession, profession => profession.IsEqualTo(character.Profession));

        Character second = actual.Single(c => c.Name == character2.Name);
        await Assert.That(second)
            .Member(entry => entry.Race, race => race.IsEqualTo(character2.Race))
            .And.Member(entry => entry.Profession, profession => profession.IsEqualTo(character2.Profession));
    }
}
