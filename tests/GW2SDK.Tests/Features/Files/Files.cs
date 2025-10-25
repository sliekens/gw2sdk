using GuildWars2.Files;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class Files
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Asset> actual, MessageContext context) = await sut.Files.GetFiles(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotNull(entry.IconUrl);
            Assert.True(entry.IconUrl!.IsAbsoluteUri);
        });
    }
}
