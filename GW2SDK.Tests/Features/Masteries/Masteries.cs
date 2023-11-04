using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Masteries;

public class Masteries
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Masteries.GetMasteries();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            mastery =>
            {
                mastery.Id_is_positive();
                mastery.Name_is_not_empty();
                mastery.Requirement_is_not_null();
                mastery.Order_is_not_negative();
                mastery.Background_is_not_empty();
                mastery.Region_is_known();
                Assert.All(
                    mastery.Levels,
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
