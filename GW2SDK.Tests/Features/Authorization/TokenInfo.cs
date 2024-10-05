using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Authorization;

public class TokenInfo
{
    [Fact]
    public async Task Token_has_info()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Tokens.GetTokenInfo(accessToken.Key);

        var apiKey = Assert.IsType<ApiKeyInfo>(actual);

        Assert.NotEmpty(apiKey.Id);
        Assert.NotEmpty(apiKey.Name);

        var expectedPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

        Assert.Equal(
            expectedPermissions,
            apiKey.Permissions.Select(p => p.ToEnum().GetValueOrDefault()).ToHashSet()
        );
    }
}
