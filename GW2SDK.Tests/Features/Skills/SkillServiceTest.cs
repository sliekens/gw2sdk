using System;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Skills;
using Xunit;

namespace GW2SDK.Tests.Features.Skills
{
    public class SkillServiceTest
    {
        [Fact]
        public async Task It_can_get_all_skills()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkillService>();

            var actual = await sut.GetSkills();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_all_skill_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkillService>();

            var actual = await sut.GetSkillsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_skill_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkillService>();

            const int skillId = 61533;

            var actual = await sut.GetSkillById(skillId);

            Assert.Equal(skillId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_skills_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkillService>();

            var ids = new[]
            {
                1110,
                12693,
                39222
            };

            var actual = await sut.GetSkillsByIds(ids);

             Assert.Collection(
                actual.Values,
                first => Assert.Equal(ids[0], first.Id), 
                second => Assert.Equal(ids[1], second.Id), 
                third => Assert.Equal(ids[2], third.Id));
        }

        [Fact]
        public async Task It_can_get_skills_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SkillService>();

            var actual = await sut.GetSkillsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
