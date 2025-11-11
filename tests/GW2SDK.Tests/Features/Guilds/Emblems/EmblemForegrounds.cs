using GuildWars2.Guilds.Emblems;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

[ServiceDataSource]
public class EmblemForegrounds(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<EmblemForeground> actual, MessageContext context) = await sut.Guilds.GetEmblemForegrounds(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, emblem =>
        {
            Assert.True(emblem.Id > 0);
            Assert.NotEmpty(emblem.LayerUrls);
            Assert.All(emblem.LayerUrls, url => Assert.True(url.IsAbsoluteUri));
        });
    }
}
