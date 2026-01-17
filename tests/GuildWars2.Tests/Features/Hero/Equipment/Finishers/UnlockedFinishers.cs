using System.Text.Json;

using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

[ServiceDataSource]
public class UnlockedFinishers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<UnlockedFinisher> actual, _) = await sut.Hero.Equipment.Finishers.GetUnlockedFinishers(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (UnlockedFinisher entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            if (entry.Permanent)
            {
                await Assert.That(entry.Quantity).IsNull();
            }
            else
            {
                await Assert.That(entry.Quantity >= 0).IsTrue();
            }

            string json;
            UnlockedFinisher? roundTrip;
#if NET
            json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.UnlockedFinisher);
            roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.UnlockedFinisher);
#else
            json = JsonSerializer.Serialize(entry);
            roundTrip = JsonSerializer.Deserialize<UnlockedFinisher>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(entry);
        }
    }
}
