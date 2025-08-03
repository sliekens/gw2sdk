using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Authorization;

public class TokenInfo
{
    [Fact]
    public async Task Token_has_info()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (GuildWars2.Authorization.TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        ApiKeyInfo apiKey = Assert.IsType<ApiKeyInfo>(actual);

        Assert.NotEmpty(apiKey.Id);
        Assert.NotEmpty(apiKey.Name);

#if NET
        HashSet<Permission> expectedPermissions = [.. Enum.GetValues<Permission>()];
#else
        HashSet<Permission> expectedPermissions = [.. Enum.GetValues(typeof(Permission)).Cast<Permission>()];
#endif
        Assert.Equal(
            expectedPermissions,
            [.. apiKey.Permissions.Select(p => p.ToEnum() ?? default)]
        );
    }
}
