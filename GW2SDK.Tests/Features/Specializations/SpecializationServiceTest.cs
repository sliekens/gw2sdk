using System;
using System.Threading.Tasks;
using GW2SDK.Specializations;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Specializations
{
    public class SpecializationServiceTest
    {
        private static class SpecializationFact
        {
            public static void Id_is_positive(Specialization actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(Specialization actual) => Assert.NotEmpty(actual.Name);

            public static void It_has_minor_traits(Specialization actual) => Assert.NotEmpty(actual.MinorTraits);

            public static void It_has_major_traits(Specialization actual) => Assert.NotEmpty(actual.MajorTraits);

            public static void Icon_is_not_empty(Specialization actual) => Assert.NotEmpty(actual.Icon);

            public static void Background_is_not_empty(Specialization actual) => Assert.NotEmpty(actual.Icon);

            public static void Profession_icon_is_not_null(Specialization actual) =>
                Assert.NotNull(actual.ProfessionIcon);

            public static void Big_profession_icon_is_not_null(Specialization actual) =>
                Assert.NotNull(actual.ProfessionIconBig);
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_specializations()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            var actual = await sut.GetSpecializations();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
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
                });
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_specialization_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            var actual = await sut.GetSpecializationsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_specialization_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            const int specializationId = 1;

            var actual = await sut.GetSpecializationById(specializationId);

            Assert.Equal(specializationId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_specializations_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            var ids = new[]
            {
                1,
                2,
                3
            };

            var actual = await sut.GetSpecializationsByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(1, first.Id),
                second => Assert.Equal(2, second.Id),
                third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Unit")]
        public async Task Specialization_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            await Assert.ThrowsAsync<ArgumentNullException>("specializationIds",
                async () =>
                {
                    await sut.GetSpecializationsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature", "Specializations")]
        [Trait("Category", "Unit")]
        public async Task Specialization_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SpecializationService>();

            await Assert.ThrowsAsync<ArgumentException>("specializationIds",
                async () =>
                {
                    await sut.GetSpecializationsByIds(new int[0]);
                });
        }
    }
}
