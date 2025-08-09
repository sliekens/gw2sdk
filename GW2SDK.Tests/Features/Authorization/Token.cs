using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Authorization;

public class Token
{
    [Fact]
    public async Task Token_has_info()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        ApiKeyInfo apiKey = Assert.IsType<ApiKeyInfo>(actual);

        Assert.NotEmpty(apiKey.Id);
        Assert.NotEmpty(apiKey.Name);

        HashSet<Extensible<Permission>> expectedPermissions = [.. TokenInfo.AllPermissions];
        Assert.Equal(
            expectedPermissions,
            [.. apiKey.Permissions.Select(p => p.ToEnum() ?? default)]
        );
    }
}
