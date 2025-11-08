using GuildWars2.Logos;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Logos;

public class Logos
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Logo> actual, MessageContext context) = await sut.Logos.GetLogos(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotNull(entry.Url);
            Assert.True(entry.Url.IsAbsoluteUri);
        });
    }
}
