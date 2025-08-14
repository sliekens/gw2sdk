using GuildWars2.Chat;

namespace GuildWars2.Tests.Features.Chat;

public class TranslationLinkTest
{
    [Theory]
    [InlineData("[&AyMBAAA=]", 291)]
    [InlineData("[&A/IGAAA=]", 1778)]
    [InlineData("[&A/xSAAA=]", 21244)]
    public void Can_marshal_translation_links(string chatLink, int translationId)
    {
        TranslationLink sut = TranslationLink.Parse(chatLink);

        string actual = sut.ToString();

        Assert.Equal(chatLink, actual);
        Assert.Equal(translationId, sut.TranslationId);
    }
}
