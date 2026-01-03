using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Authorization;

[ServiceDataSource]
public class Subtoken(Gw2Client sut)
{
    [Test]
    public async Task Subtoken_has_info()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        #region Create a new subtoken

        // API uses 1 second precision
        DateTimeOffset notBefore = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        DateTimeOffset expiresAt = notBefore.AddDays(1);
        List<Uri> urls =
        [
            new("/v2/tokeninfo", UriKind.Relative),
            new("/v2/account", UriKind.Relative),
            new("/v2/characters/My Cool Character", UriKind.Relative)
        ];
        (CreatedSubtoken createdSubtoken, MessageContext context) = await sut.Tokens.CreateSubtoken(
            accessToken.Key,
            subtoken => subtoken
                .WithPermissions(TokenInfo.AllPermissions)
                .WithAbsoluteExpiration(expiresAt)
                .WithAllowedUrls(urls),
            cancellationToken: TestContext.Current!.Execution.CancellationToken
        );

        #endregion
        // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
        // I guess this is a clock synchronization problem, because adding a delay works
        await Task.Delay(3000, TestContext.Current!.Execution.CancellationToken);
        (TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(createdSubtoken.Subtoken, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        SubtokenInfo? subtoken = await Assert.That(actual).IsTypeOf<SubtokenInfo>();
        await Assert.That(subtoken).IsNotNull()
            .And.Member(subtoken => subtoken.Id, id => id.IsNotEmpty())
            .And.Member(subtoken => subtoken.Name, name => name.IsNotEmpty())
#pragma warning disable IL2026 // https://github.com/thomhurst/TUnit/issues/3851
            .And.Member(subtoken => subtoken.Permissions, permissions => permissions.IsEquivalentTo(TokenInfo.AllPermissions, comparer: EqualityComparer<Extensible<Permission>>.Default))
#pragma warning restore IL2026
            .And.Member(subtoken => subtoken.IssuedAt, issuedAt => issuedAt.IsBetween(notBefore.AddSeconds(-5), context.Date))
            .And.Member(subtoken => subtoken.ExpiresAt, expiry => expiry.IsEqualTo(expiresAt))
#pragma warning disable IL2026 // https://github.com/thomhurst/TUnit/issues/3851
            .And.Member(subtoken => subtoken.Urls!.Select(u => Uri.UnescapeDataString(u.ToString())), subtokenUrls => subtokenUrls.IsEquivalentTo(urls.Select(u => u.ToString()), comparer: EqualityComparer<string>.Default));
#pragma warning restore IL2026
    }
}
