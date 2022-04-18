using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Skills;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Skills;

public class SkillQueryTest
{
    [Fact]
    public async Task It_can_get_all_skills()
    {
        await using Composer services = new();
        var sut = services.Resolve<SkillQuery>();

        var actual = await sut.GetSkills();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_all_skill_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<SkillQuery>();

        var actual = await sut.GetSkillsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_skill_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<SkillQuery>();

        const int skillId = 61533;

        var actual = await sut.GetSkillById(skillId);

        Assert.Equal(skillId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_skills_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<SkillQuery>();

        HashSet<int> ids = new()
        {
            1110,
            12693,
            39222
        };

        var actual = await sut.GetSkillsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
            );
    }

    [Fact]
    public async Task It_can_get_skills_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<SkillQuery>();

        var actual = await sut.GetSkillsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
