﻿using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Regions;

public class Regions
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Can_be_enumerated(int continentId, int floorId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetRegions(continentId, floorId);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_maps();
                foreach (var (mapId, map) in entry.Maps)
                {
                    // TODO: complete validation
                    Assert.Equal(mapId, map.Id);
                    foreach (var skillChallenge in map.SkillChallenges)
                    {
                        // BUG(?): Cantha (id 37) does not have skill challenge ids
                        if (entry.Id == 37)
                        {
                            Assert.Empty(skillChallenge.Id);
                        }
                        else
                        {
                            Assert.NotEmpty(skillChallenge.Id);
                        }
                    }
                }
            }
        );
    }
}