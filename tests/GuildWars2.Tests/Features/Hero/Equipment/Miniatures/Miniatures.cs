using System.Text.Json;

using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

[ServiceDataSource]
public class Miniatures(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Miniature> actual, MessageContext context) = await sut.Hero.Equipment.Miniatures.GetMiniatures(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (Miniature entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();

                if (entry.IconUrl is not null)
                {
                    await Assert.That(entry.IconUrl.IsAbsoluteUri).IsTrue();
                }

                await Assert.That(entry.Order).IsGreaterThanOrEqualTo(0);
                await Assert.That(entry.ItemId).IsGreaterThanOrEqualTo(0);
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Miniature);
                Miniature? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Miniature);
#else
                string json = JsonSerializer.Serialize(entry);
                Miniature? roundtrip = JsonSerializer.Deserialize<Miniature>(json);
#endif
                await Assert.That(entry).IsEqualTo(roundtrip);
            }
        }
    }
}
