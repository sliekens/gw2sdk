using GuildWars2.Chat;

using GuildWars2.Hero;

using GuildWars2.Hero.Builds;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Chat;

public class BuildTemplateLinkTest
{

    [Test]

    [Arguments("[&DQMGOyYvRh4qDyoPhgCGABoblQEQGwcBCRuJAQAAAAAAAAAAAAAAAAAAAAACVQAzAAA=]", ProfessionName.Engineer)]

    [Arguments("[&DQQAAAAAAAB5AHkAAAAAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAAAAAAAAAAABIwAA]", ProfessionName.Ranger)]

    [Arguments("[&DQkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADawA1AAUAAA==]", ProfessionName.Revenant)]

    [Arguments("[&DQkOHQMmPyrcEdwRBhIGEisSKxLUEdQRyhHKEQUEBAIrEtQRBhIGEisS1BEDawA1AAUAAA==]", ProfessionName.Revenant)]

    [Arguments("[&DQQAAAAAAAB5AHkAAAAAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAAAAAAAAAAACMwAjAATo9gAAm/YAAN32AABn9wAA]", ProfessionName.Ranger)]

    [Arguments("[&DQMGNyY5RioqDw0bhgCGAAsbBwEOGxobCRuJAQAAAAAAAAAAAAAAAAAAAAACNgAJAQA=]", ProfessionName.Engineer)]

    public async Task Can_marshal_build_template_links(string chatLink, ProfessionName professionName)
    {

        Gw2Client gw2 = Composer.Resolve<Gw2Client>();

        BuildTemplateLink sut = BuildTemplateLink.Parse(chatLink);

        string actual = sut.ToString();

        Build build = await sut.GetBuild(gw2, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(professionName, build.Profession);

        Assert.Equal(professionName, sut.Profession);

        Assert.Equal(chatLink, actual);

        BuildTemplateLink chatLinkRoundtrip = BuildTemplateLink.Parse(sut.ToString());

        Assert.Equal(chatLink, chatLinkRoundtrip.ToString());
    }
}
