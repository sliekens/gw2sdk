using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class Bank(Gw2Client sut)
{
    [Test]
    public async Task Contents_can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Banking.Bank actual, _) = await sut.Hero.Bank.GetBank(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual.Items);
        Assert.Equal(0, actual.Items.Count % 30);
        Assert.All(actual.Items, slot =>
        {
            if (slot is not null)
            {
                Assert.True(slot.Id > 0);
                Assert.True(slot.Count > 0);
                ItemLink chatLink = slot.GetChatLink();
                Assert.Equal(slot.Count, chatLink.Count);
                Assert.Equal(slot.SkinId, chatLink.SkinId);
                Assert.Equal(slot.SuffixItemId, chatLink.SuffixItemId);
                Assert.Equal(slot.SecondarySuffixItemId, chatLink.SecondarySuffixItemId);
            }
        });
    }
}
