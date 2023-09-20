using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tokens;

namespace GuildWars2.Tests.Features.Tokens;

public class TokenInfo
{
    [Fact]
    public async Task Token_has_info()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.TokenProvider.GetTokenInfo(accessToken.Key);

        var apiKey = Assert.IsType<ApiKeyInfo>(actual.Value);

        Assert.NotEmpty(apiKey.Id);

        // Your API key must be named GW2SDK-Full to pass this test
        // This is not intended to improve account security, only to prevent key abuse
        // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
        // If this key leaks to the outside world, it can't be (ab)used to login with GW2BLTC.com or similar sites
        Assert.Equal("GW2SDK-Full", apiKey.Name);

        var expectedPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

        Assert.Equal(expectedPermissions, apiKey.Permissions.ToHashSet());
    }
}
