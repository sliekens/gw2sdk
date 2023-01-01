using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Accounts;

public class CharacterNames
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharactersIndex(accessToken.Key);

        var expected = Composer.Resolve<TestCharacterName>().Name;

        Assert.Contains(expected, actual.Value);
    }
}
