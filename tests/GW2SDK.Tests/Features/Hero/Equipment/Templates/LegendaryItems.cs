using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class LegendaryItems(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<LegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetLegendaryItems(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.True(entry.MaxCount > 0);
            string json = JsonSerializer.Serialize(entry);
            LegendaryItem? roundtrip = JsonSerializer.Deserialize<LegendaryItem>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
