using GuildWars2.Metadata;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Metadata;

[ServiceDataSource]
public class V1(Gw2Client sut)
{
    [Test]
    public async Task Has_api_metadata()
    {
        (ApiVersion actual, _) = await sut.Metadata.GetApiVersion("v1", cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.All(actual.Languages, language =>
        {
            Assert.Contains(language, new[] { "en", "es", "de", "fr", "zh" });
        });
    }
}
