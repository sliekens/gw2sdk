using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Accounts;

public class CharacterByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var characterName = Composer.Resolve<TestCharacterName>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharacterByName(characterName.Name, accessToken.Key);

        Assert.Equal(characterName.Name, actual.Value.Name);
    }
}
