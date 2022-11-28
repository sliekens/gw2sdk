﻿using System.Threading.Tasks;
using GuildWars2.Tests.Features.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class Floors
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_enumerated(int continentId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetFloors(continentId);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_texture_dimensions();
                entry.Has_regions();
                foreach (var (regionId, region) in entry.Regions)
                {
                    Assert.Equal(regionId, region.Id);
                    region.Has_name();
                    region.Has_maps();

                    // TODO: complete validation
                    foreach (var (mapId, map) in region.Maps)
                    {
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
        );
    }
}
