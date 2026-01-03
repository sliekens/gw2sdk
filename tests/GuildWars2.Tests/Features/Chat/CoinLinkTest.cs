using GuildWars2.Chat;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Chat;

public class CoinLinkTest
{
    [Test]
    [Arguments("[&AQAAAAA=]", 0)]
    [Arguments("[&AQEAAAA=]", 1)]
    [Arguments("[&AdsnAAA=]", 10203)]
    public async Task Can_marshal_coin_links(string chatLink, int amount)
    {
        Coin coins = new(amount);
        CoinLink sut = CoinLink.Parse(chatLink);
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(chatLink);
        await Assert.That(sut.Coins).IsEqualTo(coins);
    }
}
