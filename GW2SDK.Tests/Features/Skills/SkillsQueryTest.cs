using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Skills;

public class SkillsQueryTest
{
    [Fact]
    public async Task Skills_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Skills.GetSkills();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task Skills_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Skills.GetSkillsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_skill_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 61533;

        var actual = await sut.Skills.GetSkillById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Skills_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1110,
            12693,
            39222
        };

        var actual = await sut.Skills.GetSkillsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Skills_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Skills.GetSkillsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
