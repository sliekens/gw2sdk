using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Masteries;

public class MasteryQueryTest
{
    [Fact]
    public async Task Masteries_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Masteries.GetMasteries();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
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

    [Fact]
    public async Task Masteries_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Masteries.GetMasteriesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_mastery_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int masteryId = 1;

        var actual = await sut.Masteries.GetMasteryById(masteryId);

        Assert.Equal(masteryId, actual.Value.Id);
    }

    [Fact]
    public async Task Masteries_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Masteries.GetMasteriesByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Mastery_progress_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Masteries.GetMasteryProgress(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
            progress =>
            {
                Assert.True(progress.Id > 0);
                Assert.True(progress.Level > 0);
            }
        );
    }
}
