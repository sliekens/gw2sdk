using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emotes;

public class UnlockedEmotes
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Emotes.GetUnlockedEmotes(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
