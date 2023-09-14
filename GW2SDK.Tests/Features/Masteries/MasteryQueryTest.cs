using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Masteries;

public class MasteryQueryTest
{
    [Fact]
    public async Task Masteries_can_be_listed()
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

        const int id = 1;

        var actual = await sut.Masteries.GetMasteryById(id);

        Assert.Equal(id, actual.Value.Id);
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
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
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
