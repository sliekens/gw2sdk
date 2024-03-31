using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class CharacterSummary
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) =
            await sut.Hero.Account.GetCharacterSummary(character.Name, accessToken.Key);

        Assert.Equal(character.Name, actual.Name);
        Assert.Equal(character.Race, actual.Race);
        Assert.Equal(character.Profession, actual.Profession);
        Assert.True(actual.Level > 0);
        Assert.True(actual.Age > TimeSpan.Zero);
        Assert.True(actual.LastModified > DateTimeOffset.MinValue);
        Assert.True(actual.Created > DateTimeOffset.MinValue);
    }
}
