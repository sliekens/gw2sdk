using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Authorization;

public class SubtokenInfo
{
    [Fact]
    public async Task Subtoken_has_info()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        #region Create a new subtoken

        var subtokenPermissions = new HashSet<Permission>();
        foreach (Permission permission in Enum.GetValues(typeof(Permission)))
        {
            subtokenPermissions.Add(permission);
        }

		// API uses 1 second precision
        var notBefore = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
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
            urls
        );

        #endregion

        // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
        // I guess this is a clock synchronization problem, because adding a delay works
        await Task.Delay(3000);

        var (actual, _) = await sut.Tokens.GetTokenInfo(createdSubtoken.Subtoken);

        var subtoken = Assert.IsType<GuildWars2.Authorization.SubtokenInfo>(actual);

        Assert.NotEmpty(subtoken.Id);

        // Your API key must be named GW2SDK-Full to pass this test
        // This is not intended to improve account security, only to prevent key abuse
        // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
        // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
        Assert.Equal("GW2SDK-Full", subtoken.Name);

        Assert.True(subtokenPermissions.SetEquals(subtoken.Permissions));

		// Allow 5 seconds clock skew
        Assert.InRange(subtoken.IssuedAt, notBefore.AddSeconds(-5), context.Date);

        Assert.Equal(expiresAt, subtoken.ExpiresAt);

        Assert.Equal(
            urls,
            subtoken.Urls?.Select(url => Uri.UnescapeDataString(url.ToString())).ToList()
        );
    }
}
