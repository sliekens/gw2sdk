﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Traits;
using GW2SDK.Traits.Models;
using Xunit;

namespace GW2SDK.Tests.Features.Traits;

public class TraitQueryTest
{
    private static class TraitFact
    {
        public static void Id_is_positive(Trait actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);
    }

    [Fact]
    public async Task Traits_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<TraitQuery>();

        var actual = await sut.GetTraits();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            trait =>
            {
                TraitFact.Id_is_positive(trait);
            }
            );
    }

    [Fact]
    public async Task Traits_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<TraitQuery>();

        var actual = await sut.GetTraitsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_trait_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TraitQuery>();

        const int traitId = 214;

        var actual = await sut.GetTraitById(traitId);

        Assert.Equal(traitId, actual.Value.Id);
    }

    [Fact]
    public async Task Traits_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TraitQuery>();

        HashSet<int> ids = new()
        {
            214,
            221,
            222
        };

        var actual = await sut.GetTraitsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(214, first.Id),
            second => Assert.Equal(221, second.Id),
            third => Assert.Equal(222, third.Id)
            );
    }

    [Fact]
    public async Task Traits_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<TraitQuery>();

        var actual = await sut.GetTraitsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }
}