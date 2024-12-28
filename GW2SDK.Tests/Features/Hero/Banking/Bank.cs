using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Banking;

public class Bank
{
    [Fact]
    public async Task Contents_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Bank.GetBank(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual.Items);
        Assert.Equal(0, actual.Items.Count % 30);

        Assert.All(
            actual.Items,
            slot =>
            {
                if (slot is not null)
                {
                    Assert.True(slot.Id > 0);
                    Assert.True(slot.Count > 0);
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
