using GuildWars2.Guilds.Emblems;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemForegrounds
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<EmblemForeground> actual, MessageContext context) = await sut.Guilds.GetEmblemForegrounds(cancellationToken: TestContext.Current!.CancellationToken);
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
