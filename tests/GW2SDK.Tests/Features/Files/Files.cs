using GuildWars2.Files;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Files;

[ServiceDataSource]
public class Files(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Asset> actual, MessageContext context) = await sut.Files.GetFiles(cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
