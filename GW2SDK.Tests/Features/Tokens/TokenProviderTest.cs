﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens;

public class TokenProviderTest
{
    [Fact]
    public async Task It_can_get_the_token_info_for_an_api_key()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

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

    [Fact]
    public async Task It_can_get_the_token_info_for_a_subtoken()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        #region Create a new subtoken

        var subtokenPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

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

        var subtoken = Assert.IsType<SubtokenInfo>(actual.Value);

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
