using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class CoinLinkTest
{
    [Theory]
    [InlineData("[&AQAAAAA=]", 0)]
    [InlineData("[&AQEAAAA=]", 1)]
    [InlineData("[&AdsnAAA=]", 10203)]
    public void Can_marshal_coin_links(string chatLink, Coin coins)
    {
        var sut = CoinLink.Parse(chatLink);

        var actual = sut.ToString();

        Assert.Equal(chatLink, actual);
        Assert.Equal(coins, sut.Coins);
    }
}
