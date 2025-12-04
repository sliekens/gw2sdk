using GuildWars2.Chat;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Chat;

public class ItemLinkTest
{
    [Test]
    [Arguments("[&AgEAWgAA]", 23040, 1, null, null, null)]
    [Arguments("[&AvpJUgEA]", 86601, 250, null, null, null)]
    [Arguments("[&AgGoOQHA4AMAAMtlAQA=]", 80296, 1, 992, 91595, null)]
    public async Task Can_marshal_item_links(string chatLink, int itemId, int count, int? skinId, int? suffixItemId, int? secondarySuffixItemId)
    {
        ItemLink sut = ItemLink.Parse(chatLink);
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(chatLink);
        await Assert.That(sut.ItemId).IsEqualTo(itemId);
        await Assert.That(sut.Count).IsEqualTo(count);
        await Assert.That(sut.SkinId).IsEqualTo(skinId);
        if (skinId.HasValue)
        {
            SkinLink? skinLink = sut.GetSkinLink();
            await Assert.That(skinLink?.SkinId).IsEqualTo(skinId);
        }
        else
        {
            await Assert.That(sut.GetSkinLink()).IsNull();
        }

        await Assert.That(sut.SuffixItemId).IsEqualTo(suffixItemId);
        if (suffixItemId.HasValue)
        {
            ItemLink? suffixLink = sut.GetSuffixItemLink();
            await Assert.That(suffixLink?.ItemId).IsEqualTo(suffixItemId);
        }
        else
        {
            await Assert.That(sut.GetSuffixItemLink()).IsNull();
        }

        await Assert.That(sut.SecondarySuffixItemId).IsEqualTo(secondarySuffixItemId);
        if (secondarySuffixItemId.HasValue)
        {
            ItemLink? secondarySuffixLink = sut.GetSecondarySuffixItemLink();
            await Assert.That(secondarySuffixLink?.ItemId).IsEqualTo(secondarySuffixItemId);
        }
        else
        {
            await Assert.That(sut.GetSecondarySuffixItemLink()).IsNull();
        }
    }
}
