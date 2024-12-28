using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Authorization;

public class SubtokenInfo
{
    [Fact]
    public async Task Subtoken_has_info()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        #region Create a new subtoken

        var subtokenPermissions = new HashSet<Permission>();
        foreach (Permission permission in Enum.GetValues(typeof(Permission)))
        {
            subtokenPermissions.Add(permission);
        }

        // API uses 1 second precision
        var notBefore =
            DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        var expiresAt = notBefore.AddDays(1);

        List<string> urls =
        [
            "/v2/tokeninfo", "/v2/account",
            "/v2/characters/My Cool Character"
        ];

        var (createdSubtoken, context) = await sut.Tokens.CreateSubtoken(
            accessToken.Key,
            subtokenPermissions,
            expiresAt,
            urls,
            cancellationToken: TestContext.Current.CancellationToken
        );

        #endregion

        // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
        // I guess this is a clock synchronization problem, because adding a delay works
        await Task.Delay(3000, TestContext.Current.CancellationToken);

        var (actual, _) = await sut.Tokens.GetTokenInfo(
            createdSubtoken.Subtoken,
            cancellationToken: TestContext.Current.CancellationToken
        );

        var subtoken = Assert.IsType<GuildWars2.Authorization.SubtokenInfo>(actual);

        Assert.NotEmpty(subtoken.Id);
        Assert.NotEmpty(subtoken.Name);

        Assert.True(
            subtokenPermissions.SetEquals(
                subtoken.Permissions.Select(p => p.ToEnum().GetValueOrDefault())
            )
        );

        // Allow 5 seconds clock skew
        Assert.InRange(subtoken.IssuedAt, notBefore.AddSeconds(-5), context.Date);

        Assert.Equal(expiresAt, subtoken.ExpiresAt);

        Assert.Equal(
            urls,
            subtoken.Urls?.Select(url => Uri.UnescapeDataString(url.ToString())).ToList()
        );
    }
}
