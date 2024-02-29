using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Masteries;

public class MasteryTracks
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Masteries.GetMasteryTracks();

        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            mastery =>
            {
                mastery.Id_is_positive();
                mastery.Name_is_not_empty();
                mastery.Requirement_is_not_null();
                mastery.Order_is_not_negative();
                mastery.Background_is_not_empty();
                mastery.Region_is_known();
                Assert.All(
                    mastery.Masteries,
                    level =>
                    {
                        level.Name_is_not_empty();
                        level.Description_is_not_empty();
                        level.Instruction_is_not_empty();
                        level.Icon_is_not_empty();
                        level.Costs_points();
                        level.Costs_experience();
                    }
                );
            }
        );
    }
}
