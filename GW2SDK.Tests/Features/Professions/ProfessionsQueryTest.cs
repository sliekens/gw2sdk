using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Professions;

public class ProfessionsQueryTest
{
    [Fact]
    public async Task Professions_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessions();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Value.Count);

        Assert.All(
            actual.Value,
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
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessionNames();

        Assert.Equal(Enum.GetNames(typeof(ProfessionName)).Length, actual.Value.Count);
        Assert.All(
            actual.Value,
            name => Assert.True(
                Enum.IsDefined(typeof(ProfessionName), name),
                "Enum.IsDefined(name)"
            )
        );
    }

    [Fact]
    public async Task A_profession_can_be_found_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const ProfessionName name = ProfessionName.Engineer;

        var actual = await sut.Professions.GetProfessionByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Professions_can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<ProfessionName> names = new()
        {
            ProfessionName.Mesmer,
            ProfessionName.Necromancer,
            ProfessionName.Revenant
        };

        var actual = await sut.Professions.GetProfessionsByNames(names);

        Assert.Collection(
            names,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Professions_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Professions.GetProfessionsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
