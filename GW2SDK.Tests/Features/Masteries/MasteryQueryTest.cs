using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Masteries;
using GW2SDK.Masteries.Models;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Masteries;

public class MasteryQueryTest
{
    private static class MasteryFact
    {
        public static void Id_is_positive(Mastery actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Name_is_not_empty(Mastery actual) => Assert.NotEmpty(actual.Name);

        public static void Requirement_is_not_null(Mastery actual) =>
            Assert.NotNull(actual.Requirement);

        public static void Order_is_not_negative(Mastery actual) =>
            Assert.InRange(actual.Order, 0, int.MaxValue);

        public static void Background_is_not_empty(Mastery actual) =>
            Assert.NotEmpty(actual.Background);

        public static void Region_is_known(Mastery actual) =>
            Assert.NotEqual(MasteryRegionName.Unknown, actual.Region);
    }

    private static class MasteryLevelFact
    {
        public static void Name_is_not_empty(MasteryLevel actual) => Assert.NotEmpty(actual.Name);

        public static void Description_is_not_empty(MasteryLevel actual) =>
            Assert.NotEmpty(actual.Description);

        public static void Instruction_is_not_empty(MasteryLevel actual) =>
            Assert.NotEmpty(actual.Instruction);

        public static void Icon_is_not_empty(MasteryLevel actual) => Assert.NotEmpty(actual.Icon);

        public static void Costs_points(MasteryLevel actual) =>
            Assert.InRange(actual.PointCost, 1, int.MaxValue);

        public static void Costs_experience(MasteryLevel actual) =>
            Assert.InRange(actual.ExperienceCost, 1, int.MaxValue);
    }

    [Fact]
    public async Task It_can_get_all_masteries()
    {
        await using Composer services = new();
        var sut = services.Resolve<MasteryQuery>();

        var actual = await sut.GetMasteries();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            mastery =>
            {
                MasteryFact.Id_is_positive(mastery);
                MasteryFact.Name_is_not_empty(mastery);
                MasteryFact.Requirement_is_not_null(mastery);
                MasteryFact.Order_is_not_negative(mastery);
                MasteryFact.Background_is_not_empty(mastery);
                MasteryFact.Region_is_known(mastery);
                Assert.All(
                    mastery.Levels,
                    level =>
                    {
                        MasteryLevelFact.Name_is_not_empty(level);
                        MasteryLevelFact.Description_is_not_empty(level);
                        MasteryLevelFact.Instruction_is_not_empty(level);
                        MasteryLevelFact.Icon_is_not_empty(level);
                        MasteryLevelFact.Costs_points(level);
                        MasteryLevelFact.Costs_experience(level);
                    }
                    );
            }
            );
    }

    [Fact]
    public async Task It_can_get_all_mastery_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<MasteryQuery>();

        var actual = await sut.GetMasteriesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_mastery_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MasteryQuery>();

        const int masteryId = 1;

        var actual = await sut.GetMasteryById(masteryId);

        Assert.Equal(masteryId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_masteries_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<MasteryQuery>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetMasteriesByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }
}
