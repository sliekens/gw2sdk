using System.Drawing;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class Floors
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_listed(int continentId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetFloors(continentId);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);

        var floorIds = actual.Select(f => f.Id).ToList();
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEqual(Size.Empty, entry.TextureDimensions);
                Assert.NotNull(entry.ClampedView);
                Assert.NotNull(entry.Regions);
                foreach (var (regionId, region) in entry.Regions)
                {
                    Assert.Equal(regionId, region.Id);
                    if (continentId == 2 && entry.Id == 60 && regionId == 50)
                    {
                        // Convergences region name is empty
                        Assert.Empty(region.Name);
                    }
                    else
                    {
                        Assert.NotEmpty(region.Name);
                    }

                    Assert.NotEmpty(region.Maps);
                    foreach (var (mapId, map) in region.Maps)
                    {
                        Assert.Equal(mapId, map.Id);
                        Assert.NotEmpty(map.Name);
                        Assert.True(map.MinLevel >= 0);
                        Assert.True(map.MaxLevel >= map.MinLevel);
                        Assert.Contains(map.DefaultFloor, floorIds);
                        foreach (var (poiId, poi) in map.PointsOfInterest)
                        {
                            Assert.True(poi.Id > 0);
                            Assert.NotNull(poi.Name);
                            Assert.NotEmpty(poi.ChatLink);
                            if (poi is RequiresUnlockPointOfInterest locked)
                            {
                                Assert.NotEmpty(locked.IconHref);
                            }

                            var chatLink = poi.GetChatLink();
                            Assert.Equal(poiId, chatLink.PointOfInterestId);
                            Assert.Equal(poi.ChatLink, chatLink.ToString());
                        }

                        foreach (var (heartId, heart) in map.Hearts)
                        {
                            Assert.Equal(heartId, heart.Id);
                            Assert.NotEmpty(heart.Objective);
                            Assert.InRange(heart.Level, 1, 80);
                            Assert.NotEqual(PointF.Empty, heart.Coordinates);
                            Assert.NotEmpty(heart.Boundaries);
                            Assert.All(
                                heart.Boundaries,
                                point =>
                                {
                                    Assert.NotEqual(PointF.Empty, point);
                                }
                            );
                            Assert.NotEmpty(heart.ChatLink);
                        }

                        foreach (var heroChallenge in map.HeroChallenges)
                        {
                            if (regionId == 37)
                            {
                                //https://github.com/gw2-api/issues/issues/35
                                Assert.Empty(heroChallenge.Id);
                            }
                            else
                            {
                                Assert.NotEmpty(heroChallenge.Id);
                            }

                            Assert.NotEqual(PointF.Empty, heroChallenge.Coordinates);
                        }

                        foreach (var (sectorId, sector) in map.Sectors)
                        {
                            Assert.Equal(sectorId, sector.Id);
                            Assert.NotNull(sector.Name);
                            Assert.InRange(sector.Level, 0, 80);
                            Assert.NotEqual(PointF.Empty, sector.Coordinates);
                            Assert.NotEmpty(sector.Boundaries);
                            Assert.All(
                                sector.Boundaries,
                                point =>
                                {
                                    Assert.NotEqual(PointF.Empty, point);
                                }
                            );
                            Assert.NotEmpty(sector.ChatLink);
                        }

                        foreach (var adventure in map.Adventures)
                        {
                            Assert.NotEmpty(adventure.Id);
                            Assert.NotEmpty(adventure.Name);
                            Assert.NotEmpty(adventure.Description);
                            Assert.NotEqual(PointF.Empty, adventure.Coordinates);
                        }

                        foreach (var masteryInsight in map.MasteryInsights)
                        {
                            Assert.True(masteryInsight.Id > 0);
                            Assert.True(masteryInsight.Region.IsDefined());
                            Assert.NotEqual(PointF.Empty, masteryInsight.Coordinates);
                        }

                        foreach (var godShrine in map.GodShrines ?? [])
                        {
                            Assert.True(godShrine.Id > 0);
                            Assert.True(godShrine.PointOfInterestId > 0);
                            Assert.Contains(godShrine.PointOfInterestId, map.PointsOfInterest.Keys);
                            Assert.NotEmpty(godShrine.Name);
                            Assert.NotEmpty(godShrine.NameContested);
                            Assert.NotEmpty(godShrine.IconHref);
                            Assert.NotEmpty(godShrine.IconContestedHref);
                            Assert.NotEqual(PointF.Empty, godShrine.Coordinates);

                            var link = godShrine.GetChatLink();
                            Assert.Equal(godShrine.PointOfInterestId, link.PointOfInterestId);
                        }
                    }
                }
            }
        );
    }
}
