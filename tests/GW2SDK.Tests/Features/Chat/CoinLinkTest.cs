using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class CoinLinkTest
{
    [Test]
    [Arguments("[&AQAAAAA=]", 0)]
    [Arguments("[&AQEAAAA=]", 1)]
    [Arguments("[&AdsnAAA=]", 10203)]
    public void Can_marshal_coin_links(string chatLink, int amount)
    {
        Coin coins = new(amount);
        CoinLink sut = CoinLink.Parse(chatLink);
        string actual = sut.ToString();
        Assert.Equal(chatLink, actual);
        Assert.Equal(coins, sut.Coins);
    }
}
