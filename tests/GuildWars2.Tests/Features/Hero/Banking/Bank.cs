using GuildWars2.Chat;
using GuildWars2.Hero.Inventories;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class Bank(Gw2Client sut)
{
    [Test]
    public async Task Contents_can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Banking.Bank actual, _) = await sut.Hero.Bank.GetBank(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Items).IsNotEmpty();
        await Assert.That(actual.Items.Count % 30).IsEqualTo(0);
        foreach (ItemSlot? slot in actual.Items)
        {
            if (slot is not null)
            {
                await Assert.That(slot.Id > 0).IsTrue();
                await Assert.That(slot.Count > 0).IsTrue();
                ItemLink chatLink = slot.GetChatLink();
                await Assert.That(chatLink.Count).IsEqualTo(slot.Count);
                await Assert.That(chatLink.SkinId).IsEqualTo(slot.SkinId);
                await Assert.That(chatLink.SuffixItemId).IsEqualTo(slot.SuffixItemId);
                await Assert.That(chatLink.SecondarySuffixItemId).IsEqualTo(slot.SecondarySuffixItemId);
            }
        }
    }
}
