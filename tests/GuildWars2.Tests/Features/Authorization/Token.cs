using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Authorization;

[ServiceDataSource]
public class Token(Gw2Client sut)
{
    [Test]
    public async Task Token_has_info()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        ApiKeyInfo apiKey = await Assert.That(actual).IsTypeOf<ApiKeyInfo>()
            .And.Member(key => key.Id, id => id.IsNotEmpty())
            .And.Member(key => key.Name, name => name.IsNotEmpty())
            .And.IsNotNull();
#pragma warning disable IL2026 // https://github.com/thomhurst/TUnit/issues/3851
        await Assert.That(apiKey.Permissions).IsEquivalentTo(TokenInfo.AllPermissions, comparer: EqualityComparer<Extensible<Permission>>.Default);
#pragma warning restore IL2026
    }
}
