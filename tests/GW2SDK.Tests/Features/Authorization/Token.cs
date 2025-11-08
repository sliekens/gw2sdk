using GuildWars2.Authorization;
using GuildWars2.Tests.TestInfrastructure;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Authorization;

public class Token
{
    [Test]
    public async Task Token_has_info()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (TokenInfo actual, _) = await sut.Tokens.GetTokenInfo(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        ApiKeyInfo apiKey = await Assert.That(actual).IsTypeOf<ApiKeyInfo>()
            .And.Member(key => key.Id, id => id.IsNotEmpty())
            .And.Member(key => key.Name, name => name.IsNotEmpty())
            .And.IsNotNull();
        await Assert.That(apiKey.Permissions).IsEquivalentTo(TokenInfo.AllPermissions);
    }
}
