using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

public class V1
{
    [Fact]
    public async Task Has_api_metadata()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Metadata.GetApiVersion("v1", cancellationToken: TestContext.Current.CancellationToken);

        Assert.All(
            actual.Languages,
            language =>
            {
                Assert.Contains(
                    language,
                    new[]
                    {
                        "en",
                        "es",
                        "de",
                        "fr",
                        "zh"
                    }
                );
            }
        );
    }
}
