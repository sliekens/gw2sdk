using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts;

public class CharacterByName
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var characterName = services.Resolve<TestCharacterName>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharacterByName(characterName.Name, accessToken.Key);

        Assert.Equal(characterName.Name, actual.Value.Name);
    }
}
