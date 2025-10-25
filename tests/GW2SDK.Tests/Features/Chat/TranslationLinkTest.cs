using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class TranslationLinkTest
{
    [Test]
    [Arguments("[&AyMBAAA=]", 291)]
    [Arguments("[&A/IGAAA=]", 1778)]
    [Arguments("[&A/xSAAA=]", 21244)]
    public void Can_marshal_translation_links(string chatLink, int translationId)
    {
        TranslationLink sut = TranslationLink.Parse(chatLink);
        string actual = sut.ToString();
        Assert.Equal(chatLink, actual);
        Assert.Equal(translationId, sut.TranslationId);
    }
}
