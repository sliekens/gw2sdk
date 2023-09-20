﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Tokens;

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

        var expiresAt = DateTimeOffset.UtcNow.AddDays(1);

        List<string> urls = new()
        {
            "/v2/tokeninfo",
            "/v2/account",
            "/v2/characters/My Cool Character"
        };

        var createdSubtoken = await sut.TokenProvider.CreateSubtoken(
            accessToken.Key,
            subtokenPermissions,
            expiresAt,
            urls
        );

        #endregion

        // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
        // I guess this is a clock synchronization problem, because adding a delay works
        await Task.Delay(3000);

        var actual = await sut.TokenProvider.GetTokenInfo(createdSubtoken.Value.Subtoken);

        var subtoken = Assert.IsType<GuildWars2.Tokens.SubtokenInfo>(actual.Value);

        Assert.NotEmpty(subtoken.Id);

        // Your API key must be named GW2SDK-Full to pass this test
        // This is not intended to improve account security, only to prevent key abuse
        // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
        // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
        Assert.Equal("GW2SDK-Full", subtoken.Name);

        Assert.True(subtokenPermissions.SetEquals(subtoken.Permissions));

        AssertEx.Equal(createdSubtoken.Date, subtoken.IssuedAt, TimeSpan.FromSeconds(1));

        // Truncate milliseconds: API uses 1 second precision
        var expectedExpiry = DateTimeOffset.FromUnixTimeSeconds(expiresAt.ToUnixTimeSeconds());
        Assert.Equal(expectedExpiry, subtoken.ExpiresAt);

        Assert.Equal(
            urls,
            subtoken.Urls?.Select(url => Uri.UnescapeDataString(url.ToString())).ToList()
        );
    }
}
