using GuildWars2.Hero.Masteries;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Masteries;

public class MasteryTracks
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Masteries.GetMasteryTracks();

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            mastery =>
            {
                Assert.True(mastery.Id > 0);
                Assert.NotEmpty(mastery.Name);
                Assert.NotNull(mastery.Requirement);
                Assert.True(mastery.Order >= 0);
                Assert.NotEmpty(mastery.BackgroundHref);
                Assert.True(mastery.Region.IsDefined());
                Assert.NotEqual(MasteryRegionName.Unknown, mastery.Region);
                Assert.All(
                    mastery.Masteries,
                    level =>
                    {
                        Assert.NotEmpty(level.Name);
                        Assert.NotEmpty(level.Description);
                        MarkupSyntaxValidator.Validate(level.Description);
                        Assert.NotEmpty(level.Instruction);
                        MarkupSyntaxValidator.Validate(level.Instruction);
                        Assert.NotEmpty(level.IconHref);
                        Assert.True(level.PointCost > 0);
                        Assert.True(level.ExperienceCost > 0);
                    }
                );
            }
        );
    }
}
