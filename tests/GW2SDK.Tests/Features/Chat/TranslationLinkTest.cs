using GuildWars2.Chat;

using Assert = TUnit.Assertions.Assert;

namespace GuildWars2.Tests.Features.Chat;

public class TranslationLinkTest
{
    [Test]
    [Arguments("[&AyMBAAA=]", 291)]
    [Arguments("[&A/IGAAA=]", 1778)]
    [Arguments("[&A/xSAAA=]", 21244)]
    public async Task Can_marshal_translation_links(string chatLink, int translationId)
    {
        TranslationLink sut = TranslationLink.Parse(chatLink);
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(chatLink);
        await Assert.That(sut.TranslationId).IsEqualTo(translationId);
    }
}
