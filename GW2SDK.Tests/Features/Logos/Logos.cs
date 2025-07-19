using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Logos;

public class Logos
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Logos.GetLogos(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotNull(entry.Url);
                Assert.True(entry.Url.IsAbsoluteUri);
            }
        );
    }
}
