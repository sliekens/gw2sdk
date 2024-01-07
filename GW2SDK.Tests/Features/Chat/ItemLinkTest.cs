using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class ItemLinkTest
{
    [Theory]
    [InlineData("[&AgEAWgAA]", 23040, 1, null, null, null)]
    [InlineData("[&AvpJUgEA]", 86601, 250, null, null, null)]
    [InlineData("[&AgGoOQHA4AMAAMtlAQA=]", 80296, 1, 992, 91595, null)]
    public void Can_marshal_item_links(
        string chatLink,
        int itemId,
        int count,
        int? skinId,
        int? suffixItemId,
        int? secondarySuffixItemId
    )
    {
        var sut = ItemLink.Parse(chatLink);

        var actual = sut.ToString();

        Assert.Equal(chatLink, actual);
        Assert.Equal(itemId, sut.ItemId);
        Assert.Equal(count, sut.Count);
        Assert.Equal(skinId, sut.SkinId);
        Assert.Equal(suffixItemId, sut.SuffixItemId);
        Assert.Equal(secondarySuffixItemId, sut.SecondarySuffixItemId);
    }
}
