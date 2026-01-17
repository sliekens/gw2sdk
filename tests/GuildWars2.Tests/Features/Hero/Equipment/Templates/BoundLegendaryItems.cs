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
        (IImmutableValueSet<BoundLegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetBoundLegendaryItems(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual).IsNotEmpty();
        foreach (BoundLegendaryItem entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry.Count > 0).IsTrue();
#if NET
            string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.BoundLegendaryItem);
            BoundLegendaryItem? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.BoundLegendaryItem);
#else
            string json = JsonSerializer.Serialize(entry);
            BoundLegendaryItem? roundtrip = JsonSerializer.Deserialize<BoundLegendaryItem>(json);
#endif
            await Assert.That(roundtrip).IsEqualTo(entry);
        }
    }
}
