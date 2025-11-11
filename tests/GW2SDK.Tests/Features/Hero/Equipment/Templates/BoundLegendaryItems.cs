using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class BoundLegendaryItems(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<BoundLegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetBoundLegendaryItems(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
