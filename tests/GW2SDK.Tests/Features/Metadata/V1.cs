using GuildWars2.Metadata;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

public class V1
{
    [Test]
    public async Task Has_api_metadata()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (ApiVersion actual, _) = await sut.Metadata.GetApiVersion("v1", cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.All(actual.Languages, language =>
        {
            Assert.Contains(language, new[] { "en", "es", "de", "fr", "zh" });
        });
    }
}
