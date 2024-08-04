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
        if (skinId.HasValue)
        {
            var skinLink = sut.GetSkinLink();
            Assert.Equal(skinId, skinLink?.SkinId);
        }
        else
        {
            Assert.Null(sut.GetSkinLink());
        }

        Assert.Equal(suffixItemId, sut.SuffixItemId);
        if (suffixItemId.HasValue)
        {
            var suffixLink = sut.GetSuffixItemLink();
            Assert.Equal(suffixItemId, suffixLink?.ItemId);
        }
        else
        {
            Assert.Null(sut.GetSuffixItemLink());
        }

        Assert.Equal(secondarySuffixItemId, sut.SecondarySuffixItemId);
        if (secondarySuffixItemId.HasValue)
        {
            var secondarySuffixLink = sut.GetSecondarySuffixItemLink();
            Assert.Equal(secondarySuffixItemId, secondarySuffixLink?.ItemId);
        }
        else
        {
            Assert.Null(sut.GetSecondarySuffixItemLink());
        }
    }
}
