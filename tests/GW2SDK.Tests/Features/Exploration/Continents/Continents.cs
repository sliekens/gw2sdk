using System.Drawing;

using GuildWars2.Exploration.Continents;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Exploration.Continents;

public class Continents
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Continent> actual, MessageContext context) = await sut.Exploration.GetContinents(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.NotEmpty(actual);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotEqual(Size.Empty, entry.ContinentDimensions);
            Assert.True(entry.MinZoom >= 0);
            Assert.True(entry.MaxZoom > entry.MinZoom);
            Assert.NotEmpty(entry.Floors);
        });
    }
}
