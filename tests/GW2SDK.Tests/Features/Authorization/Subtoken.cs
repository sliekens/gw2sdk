using GuildWars2.Authorization;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Authorization;

public class Subtoken
{

    [Test]

    public async Task Subtoken_has_info(CancellationToken cancellationToken)
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;
        #region Create a new subtoken
#if NET
        HashSet<Permission> subtokenPermissions = [.. Enum.GetValues<Permission>().Where(p => p != Permission.None)];
#else

        HashSet<Permission> subtokenPermissions = [.. Enum.GetValues(typeof(Permission)).Cast<Permission>().Where(p => p != Permission.None)];
#endif
        // API uses 1 second precision

        DateTimeOffset notBefore = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        DateTimeOffset expiresAt = notBefore.AddDays(1);

        List<Uri> urls = [new("/v2/tokeninfo", UriKind.Relative), new("/v2/account", UriKind.Relative), new("/v2/characters/My Cool Character", UriKind.Relative)];

        (CreatedSubtoken createdSubtoken, MessageContext context) = await sut.Tokens.CreateSubtoken(accessToken.Key, subtokenPermissions, expiresAt, urls, cancellationToken: cancellationToken);
        #endregion
        // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
        // I guess this is a clock synchronization problem, because adding a delay works

        await Task.Delay(3000, cancellationToken);

        (TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(createdSubtoken.Subtoken, cancellationToken: cancellationToken);

        SubtokenInfo subtoken = Assert.IsType<SubtokenInfo>(actual);

        Assert.NotEmpty(subtoken.Id);

        Assert.NotEmpty(subtoken.Name);

        Assert.True(subtokenPermissions.SetEquals(subtoken.Permissions.Select(p => p.ToEnum() ?? default)));
        // Allow 5 seconds clock skew

        Assert.InRange(subtoken.IssuedAt, notBefore.AddSeconds(-5), context.Date);

        Assert.Equal(expiresAt, subtoken.ExpiresAt);

        Assert.Equal([.. urls.Select(u => Uri.UnescapeDataString(u.ToString()))], subtoken.Urls?.Select(url => Uri.UnescapeDataString(url.ToString())).ToList());
    }
}
