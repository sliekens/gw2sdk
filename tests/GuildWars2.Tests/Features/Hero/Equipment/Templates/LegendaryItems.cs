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
        (IImmutableValueSet<LegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetLegendaryItems(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        using (Assert.Multiple())
        {
            foreach (LegendaryItem entry in actual)
            {
                await Assert.That(entry).Member(e => e.Id, id => id.IsGreaterThan(0))
                    .And.Member(e => e.MaxCount, maxCount => maxCount.IsGreaterThan(0));
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.LegendaryItem);
                LegendaryItem? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.LegendaryItem);
#else
                string json = JsonSerializer.Serialize(entry);
                LegendaryItem? roundtrip = JsonSerializer.Deserialize<LegendaryItem>(json);
#endif
                await Assert.That(roundtrip).IsEqualTo(entry);
            }
        }
    }
}
