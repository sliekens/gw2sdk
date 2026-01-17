using System.Text.Json;

using GuildWars2.Hero.Equipment.MailCarriers;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

[ServiceDataSource]
public class MailCarriers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<MailCarrier> actual, MessageContext context) = await sut.Hero.Equipment.MailCarriers.GetMailCarriers(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (MailCarrier mailCarrier in actual)
            {
                await Assert.That(mailCarrier.Id).IsGreaterThanOrEqualTo(1);

                if (mailCarrier.Flags.Default)
                {
                    await Assert.That(mailCarrier.UnlockItemIds).IsEmpty();
                }
                else
                {
                    await Assert.That(mailCarrier.UnlockItemIds).IsNotEmpty();
                }

                await Assert.That(mailCarrier.Order).IsGreaterThanOrEqualTo(0).And.IsLessThanOrEqualTo(1000);

                if (mailCarrier.IconUrl is not null)
                {
                    await Assert.That(mailCarrier.IconUrl.IsAbsoluteUri).IsTrue();
                }

                await Assert.That(mailCarrier.Name).IsNotEmpty();
#if NET
                string json = JsonSerializer.Serialize(mailCarrier, Common.TestJsonContext.Default.MailCarrier);
                MailCarrier? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.MailCarrier);
#else
                string json = JsonSerializer.Serialize(mailCarrier);
                MailCarrier? roundtrip = JsonSerializer.Deserialize<MailCarrier>(json);
#endif
                await Assert.That(mailCarrier).IsEqualTo(roundtrip);
            }
        }
    }
}
