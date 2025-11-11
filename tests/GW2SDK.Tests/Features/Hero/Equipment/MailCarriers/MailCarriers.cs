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
        (HashSet<MailCarrier> actual, MessageContext context) = await sut.Hero.Equipment.MailCarriers.GetMailCarriers(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, mailCarrier =>
        {
            Assert.True(mailCarrier.Id >= 1);
            if (mailCarrier.Flags.Default)
            {
                Assert.Empty(mailCarrier.UnlockItemIds);
            }
            else
            {
                Assert.NotEmpty(mailCarrier.UnlockItemIds);
            }

            Assert.InRange(mailCarrier.Order, 0, 1000);
            Assert.True(mailCarrier.IconUrl is null || mailCarrier.IconUrl.IsAbsoluteUri);
            Assert.NotEmpty(mailCarrier.Name);
            string json = JsonSerializer.Serialize(mailCarrier);
            MailCarrier? roundtrip = JsonSerializer.Deserialize<MailCarrier>(json);
            Assert.Equal(mailCarrier, roundtrip);
        });
    }
}
