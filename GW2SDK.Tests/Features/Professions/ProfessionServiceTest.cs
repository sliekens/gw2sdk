using System;
using System.Threading.Tasks;
using GW2SDK.Professions;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Professions
{
    public class ProfessionServiceTest
    {
        [Fact]
        [Trait("Feature",  "Professions")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_professions()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ProfessionService>();

            var actual = await sut.GetProfessions();

            Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Values.Count);

            foreach (var profession in actual.Values)
            {
                Assert.True(Enum.IsDefined(typeof(ProfessionName), profession.Id), "Enum.IsDefined(profession.Id)");
                Assert.NotEmpty(profession.Name);
                Assert.NotEmpty(profession.Icon);
                Assert.NotEmpty(profession.IconBig);
            }
        }

        [Fact]
        [Trait("Feature",  "Professions")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_profession_names()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ProfessionService>();

            var actual = await sut.GetProfessionNames();

            Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Values.Count);
            Assert.All(actual.Values, name => Assert.True(Enum.IsDefined(typeof(ProfessionName), name), "Enum.IsDefined(name)"));
        }

        [Fact]
        [Trait("Feature",  "Professions")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_profession_by_name()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ProfessionService>();

            const ProfessionName name = ProfessionName.Engineer;

            var actual = await sut.GetProfessionByName(name);

            Assert.Equal(name, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Professions")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_professions_by_name()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ProfessionService>();

            var names = new[] { ProfessionName.Mesmer, ProfessionName.Necromancer, ProfessionName.Revenant };

            var actual = await sut.GetProfessionsByNames(names);

            Assert.Collection(actual.Values,
                first => Assert.Equal(names[0],  first.Id),
                second => Assert.Equal(names[1], second.Id),
                third => Assert.Equal(names[2],  third.Id));
        }

        [Fact]
        [Trait("Feature",  "Professions")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_professions_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ProfessionService>();

            var actual = await sut.GetProfessionsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
