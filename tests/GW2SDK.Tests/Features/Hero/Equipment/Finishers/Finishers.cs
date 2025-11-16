using System.Text.Json;

using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

[ServiceDataSource]
public class Finishers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Finisher> actual, MessageContext context) = await sut.Hero.Equipment.Finishers.GetFinishers(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (Finisher entry in actual)
        {
            await Assert.That(entry.Id).IsGreaterThan(0);
            await Assert.That(entry.LockedText).IsNotNull();
            MarkupSyntaxValidator.Validate(entry.LockedText);
            await Assert.That(entry.UnlockItemIds).IsNotNull();
            foreach (int id in entry.UnlockItemIds)
            {
                await Assert.That(id).IsGreaterThan(0);
            }

            await Assert.That(entry.Order).IsGreaterThanOrEqualTo(0);
            await Assert.That(entry.IconUrl).IsNotNull();
            await Assert.That(entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString()).IsTrue();
            await Assert.That(entry.Name).IsNotEmpty();
            string json;
            Finisher? roundTrip;
#if NET
            json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Finisher);
            roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Finisher);
#else
            json = JsonSerializer.Serialize(entry);
            roundTrip = JsonSerializer.Deserialize<Finisher>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(entry);
        }
    }
}
