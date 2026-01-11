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
        using (Assert.Multiple())
        {
            foreach (string language in actual.Languages)
            {
                await Assert.That(["en", "es", "de", "fr", "zh"]).Contains(language);
            }
        }
    }
}
