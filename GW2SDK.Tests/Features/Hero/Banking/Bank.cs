using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Banking;

public class Bank
{
    [Fact]
    public async Task Contents_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Bank.GetBank(accessToken.Key);

        actual.Not_empty();
        actual.Has_multiple_of_30_slots();

        Assert.All(
            actual.Items,
            slot =>
            {
                if (slot is not null)
                {
                    slot.Has_id();
                    slot.Has_count();
                    var chatLink = slot.GetChatLink();
                    Assert.Equal(slot.Count, chatLink.Count);
                    Assert.Equal(slot.SkinId, chatLink.SkinId);
                    Assert.Equal(slot.SuffixItemId, chatLink.SuffixItemId);
                    Assert.Equal(slot.SecondarySuffixItemId, chatLink.SecondarySuffixItemId);
                }
            }
        );
    }
}
