using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemForegroundsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) =
            await sut.Guilds.GetEmblemForegroundsIndex(TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
