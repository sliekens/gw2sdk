using System;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Professions;

public class ProfessionsQueryTest
{
    [Fact]
    public async Task Professions_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessions();

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
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessionNames();

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
        var sut = services.Resolve<Gw2Client>();

        const ProfessionName name = ProfessionName.Engineer;

        var actual = await sut.Professions.GetProfessionByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Professions_can_be_filtered_by_name()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        ProfessionName[] names =
        {
            ProfessionName.Mesmer,
            ProfessionName.Necromancer,
            ProfessionName.Revenant
        };

        var actual = await sut.Professions.GetProfessionsByNames(names);

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
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessionsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}
