﻿using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Regions;

public class RegionById
{
    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 0, 3)]
    public async Task Can_be_found(int continentId, int floorId, int regionId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetRegionById(continentId, floorId, regionId);

        Assert.Equal(regionId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_maps();
        foreach (var (mapId, map) in actual.Value.Maps)
        {
            // TODO: complete validation
            Assert.Equal(mapId, map.Id);
            foreach (var skillChallenge in map.SkillChallenges)
            {
                // BUG(?): Cantha (id 37) does not have skill challenge ids
                if (regionId == 37)
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
}
