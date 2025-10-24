using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class BoundLegendaryItems
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<BoundLegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetBoundLegendaryItems(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.NotEmpty(actual);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.True(entry.Count > 0);
            string json = JsonSerializer.Serialize(entry);
            BoundLegendaryItem? roundtrip = JsonSerializer.Deserialize<BoundLegendaryItem>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
