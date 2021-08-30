using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GW2SDK.Quaggans;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Quaggans
{
    public class QuagganServiceTest
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        private static class QuagganFact
        {
            internal static void Id_is_not_empty(QuagganRef actual) => Assert.NotEmpty(actual.Id);

            internal static void Quaggan_has_picture(QuagganRef actual) => Assert.True(Uri.IsWellFormedUriString(actual.PictureHref, UriKind.Absolute));

            internal static void Validate(QuagganRef actual)
            {
                Id_is_not_empty(actual);
                Quaggan_has_picture(actual);
            }
        }

        [Fact]
        public async Task It_can_get_all_quaggans()
        {
            await using var services = new Composer();
            var sut = services.Resolve<QuagganService>();

            var actual = await sut.GetQuaggans();

            Assert.True(actual.HasValues);
            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                quaggan =>
                {
                    QuagganFact.Validate(quaggan);
                });
        }

        [Fact]
        public async Task It_can_get_all_quaggan_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<QuagganService>();

            var actual = await sut.GetQuaggansIndex();

            Assert.True(actual.HasValues);
            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_quaggan_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<QuagganService>();

            const string quagganId = "present";

            var actual = await sut.GetQuagganById(quagganId);

            Assert.True(actual.HasValue);
            QuagganFact.Validate(actual.Value);
            Assert.Equal(quagganId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_quaggans_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<QuagganService>();

            var ids = new[]
            {
                "404",
                "aloha",
                "attack"
            };

            var actual = await sut.GetQuaggansByIds(ids);
            
            Assert.True(actual.HasValues);
            Assert.All(actual.Values, QuagganFact.Validate);
            Assert.Collection(actual.Values,
                first => Assert.Equal(ids[0], first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2], third.Id));
        }

        [Fact]
        public async Task It_can_get_quaggans_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<QuagganService>();

            var actual = await sut.GetQuaggansByPage(0, 3);
            
            Assert.True(actual.HasValues);
            Assert.All(actual.Values, QuagganFact.Validate);
            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
