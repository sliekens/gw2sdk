using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using GW2SDK.Quaggans;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Quaggans;

public class QuagganServiceTest
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    private static class QuagganFact
    {
        internal static void Id_is_not_empty(Quaggan actual)
        {
            Assert.NotEmpty(actual.Id);
        }

        internal static void Quaggan_has_picture(Quaggan actual)
        {
            Assert.True(Uri.IsWellFormedUriString(actual.PictureHref, UriKind.Absolute));
        }

        internal static void Validate(Quaggan actual)
        {
            Id_is_not_empty(actual);
            Quaggan_has_picture(actual);
        }
    }

    [Fact]
    public async Task It_can_get_all_quaggans()
    {
        await using Composer services = new();
        var sut = services.Resolve<QuagganService>();

        var actual = await sut.GetQuaggans();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(actual,
            quaggan =>
            {
                QuagganFact.Validate(quaggan);
            });
    }

    [Fact]
    public async Task It_can_get_all_quaggan_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<QuagganService>();

        var actual = await sut.GetQuaggansIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_quaggan_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<QuagganService>();

        const string quagganId = "present";

        var actual = await sut.GetQuagganById(quagganId);

        QuagganFact.Validate(actual.Value);
        Assert.Equal(quagganId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_quaggans_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<QuagganService>();

        HashSet<string> ids = new()
        {
            "404",
            "aloha",
            "attack"
        };

        var actual = await sut.GetQuaggansByIds(ids);

        Assert.All(actual, QuagganFact.Validate);
        Assert.Collection(actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids));
    }

    [Fact]
    public async Task It_can_get_quaggans_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<QuagganService>();

        var actual = await sut.GetQuaggansByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.All(actual, QuagganFact.Validate);
        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}