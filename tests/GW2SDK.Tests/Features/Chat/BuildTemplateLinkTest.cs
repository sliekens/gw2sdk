using GuildWars2.Chat;
using GuildWars2.Hero;
using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Chat;

[ServiceDataSource]
public class BuildTemplateLinkTest(Gw2Client gw2)
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
        BuildTemplateLink sut = BuildTemplateLink.Parse(chatLink);
        string actual = sut.ToString();
        Build build = await sut.GetBuild(gw2, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(build.Profession).IsEqualTo(professionName);
        await Assert.That(sut.Profession).IsEqualTo(professionName);
        await Assert.That(actual).IsEqualTo(chatLink);
        BuildTemplateLink chatLinkRoundtrip = BuildTemplateLink.Parse(sut.ToString());
        await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink);
    }
}
