using System;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Professions;

public class ProfessionQueryTest
{
    [Fact]
    public async Task Professions_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<ProfessionQuery>();

        var actual = await sut.GetProfessions();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);

        Assert.All(
            actual,
            profession =>
            {
                Assert.True(
                    Enum.IsDefined(typeof(ProfessionName), profession.Id),
                    "Enum.IsDefined(profession.Id)"
                    );
                Assert.NotEmpty(profession.Name);
                Assert.NotEmpty(profession.Icon);
                Assert.NotEmpty(profession.IconBig);
            }
            );
    }

    [Fact]
    public async Task Profession_names_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<ProfessionQuery>();

        var actual = await sut.GetProfessionNames();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Count);
        Assert.All(
            actual,
            name => Assert.True(
                Enum.IsDefined(typeof(ProfessionName), name),
                "Enum.IsDefined(name)"
                )
            );
    }

    [Fact]
    public async Task A_profession_can_be_found_by_name()
    {
        await using Composer services = new();
        var sut = services.Resolve<ProfessionQuery>();

        const ProfessionName name = ProfessionName.Engineer;

        var actual = await sut.GetProfessionByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Professions_can_be_filtered_by_name()
    {
        await using Composer services = new();
        var sut = services.Resolve<ProfessionQuery>();

        ProfessionName[] names =
        {
            ProfessionName.Mesmer,
            ProfessionName.Necromancer,
            ProfessionName.Revenant
        };

        var actual = await sut.GetProfessionsByNames(names);

        Assert.Collection(
            actual,
            first => Assert.Equal(names[0], first.Id),
            second => Assert.Equal(names[1], second.Id),
            third => Assert.Equal(names[2], third.Id)
            );
    }

    [Fact]
    public async Task Professions_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<ProfessionQuery>();

        var actual = await sut.GetProfessionsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
