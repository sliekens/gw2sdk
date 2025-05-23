using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class Files
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Files.GetFiles(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotNull(entry.IconUrl);
                Assert.True(entry.IconUrl!.IsAbsoluteUri);
            }
        );
    }
}
