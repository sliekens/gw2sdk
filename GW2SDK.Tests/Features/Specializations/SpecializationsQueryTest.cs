using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Specializations;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Specializations;

public class SpecializationsQueryTest
{
    private static class SpecializationFact
    {
        public static void Id_is_positive(Specialization actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Name_is_not_empty(Specialization actual) => Assert.NotEmpty(actual.Name);

        public static void It_has_minor_traits(Specialization actual) =>
            Assert.NotEmpty(actual.MinorTraits);

        public static void It_has_major_traits(Specialization actual) =>
            Assert.NotEmpty(actual.MajorTraits);

        public static void Icon_is_not_empty(Specialization actual) => Assert.NotEmpty(actual.Icon);

        public static void Background_is_not_empty(Specialization actual) =>
            Assert.NotEmpty(actual.Icon);

        public static void Profession_icon_is_not_null(Specialization actual) =>
            Assert.NotNull(actual.ProfessionIcon);

        public static void Big_profession_icon_is_not_null(Specialization actual) =>
            Assert.NotNull(actual.ProfessionIconBig);
    }

    [Fact]
    public async Task Specializations_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Specializations.GetSpecializations();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            specialization =>
            {
                SpecializationFact.Id_is_positive(specialization);
                SpecializationFact.Name_is_not_empty(specialization);
                SpecializationFact.It_has_minor_traits(specialization);
                SpecializationFact.It_has_major_traits(specialization);
                SpecializationFact.Icon_is_not_empty(specialization);
                SpecializationFact.Background_is_not_empty(specialization);
                SpecializationFact.Big_profession_icon_is_not_null(specialization);
                SpecializationFact.Profession_icon_is_not_null(specialization);
            }
        );
    }

    [Fact]
    public async Task Specializations_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Specializations.GetSpecializationsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_specialization_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int specializationId = 1;

        var actual = await sut.Specializations.GetSpecializationById(specializationId);

        Assert.Equal(specializationId, actual.Value.Id);
    }

    [Fact]
    public async Task Specializations_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Specializations.GetSpecializationsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }
}
