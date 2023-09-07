using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Specializations;

public class SpecializationsQueryTest
{
    [Fact]
    public async Task Specializations_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Specializations.GetSpecializations();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            specialization =>
            {
                specialization.Id_is_positive();
                specialization.Name_is_not_empty();
                specialization.It_has_minor_traits();
                specialization.It_has_major_traits();
                specialization.Icon_is_not_empty();
                specialization.Background_is_not_empty();
                specialization.Big_profession_icon_is_not_null();
                specialization.Profession_icon_is_not_null();
            }
        );
    }

    [Fact]
    public async Task Specializations_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Specializations.GetSpecializationsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_specialization_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int specializationId = 1;

        var actual = await sut.Specializations.GetSpecializationById(specializationId);

        Assert.Equal(specializationId, actual.Value.Id);
    }

    [Fact]
    public async Task Specializations_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Specializations.GetSpecializationsByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }
}
