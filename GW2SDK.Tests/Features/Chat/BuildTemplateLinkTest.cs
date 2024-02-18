using GuildWars2.Chat;
using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Chat;

public class BuildTemplateLinkTest
{
    [Theory]
    [InlineData("[&DQMGOyYvRh4qDyoPhgCGABoblQEQGwcBCRuJAQAAAAAAAAAAAAAAAAAAAAACVQAzAAA=]", ProfessionName.Engineer)]
    // [InlineData("[&DQQeOSA/Nzp5AAAApQEAALUAAACsAQAALhYAACcAAAAAAAAAAAAAAAAAAAA=]", ProfessionName.Ranger)]
    [InlineData("[&DQQAAAAAAAB5AHkAAAAAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAAAAAAAAAAABIwAA]", ProfessionName.Ranger)]
    [InlineData("[&DQkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADawA1AAUAAA==]", ProfessionName.Revenant)]
    [InlineData("[&DQkOHQMmPyrcEdwRBhIGEisSKxLUEdQRyhHKEQUEBAIrEtQRBhIGEisS1BEDawA1AAUAAA==]", ProfessionName.Revenant)]
    public async Task Can_marshal_build_template_links(string chatLink, ProfessionName professionName)
    {
        var gw2 = Composer.Resolve<Gw2Client>();

        var sut = BuildTemplateLink.Parse(chatLink);
        var actual = sut.ToString();
        var build = await sut.GetBuild(gw2);

        Assert.Equal(professionName, build.Profession);
        Assert.Equal(professionName, sut.Profession);
        Assert.Equal(chatLink, actual);
    }
}
