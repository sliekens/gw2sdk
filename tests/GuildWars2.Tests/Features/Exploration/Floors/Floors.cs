using System.Drawing;

using GuildWars2.Chat;
using GuildWars2.Exploration.Adventures;
using GuildWars2.Exploration.Floors;
using GuildWars2.Exploration.GodShrines;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.Maps;
using GuildWars2.Exploration.MasteryInsights;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Sectors;
using GuildWars2.Tests.TestInfrastructure.Composition;

using Region = GuildWars2.Exploration.Regions.Region;

namespace GuildWars2.Tests.Features.Exploration.Floors;

[ServiceDataSource]
public class Floors(Gw2Client sut)
{
    [Test]
    [Arguments(1)]
    [Arguments(2)]
    public async Task Can_be_listed(int continentId)
    {
        (HashSet<Floor> actual, MessageContext context) = await sut.Exploration.GetFloors(continentId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context)
            .Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        List<int> floorIds = [.. actual.Select(f => f.Id)];
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (Floor entry in actual)
            {
                await Assert.That(entry)
                    .Member(e => e.TextureDimensions, m => m.IsNotEqualTo(Size.Empty))
                    .And.Member(e => e.ClampedView, m => m.IsNotNull())
                    .And.Member(e => e.Regions, m => m.IsNotNull());
                foreach ((int regionId, Region region) in entry.Regions)
                {
                    await Assert.That(region.Id).IsEqualTo(regionId);
                    // Convergences and Mists Vault region names are empty
                    await Assert.That(region.Name).IsNotNull();
                    await Assert.That(region.Maps).IsNotEmpty();
                    foreach ((int mapId, Map map) in region.Maps)
                    {
                        await Assert.That(map.Id).IsEqualTo(mapId);
                        if (map.Id == 1150)
                        {
                            // Unnamed Salvation Pass (Public) map
                            await Assert.That(map.Name).IsEmpty();
                        }
                        else
                        {
                            await Assert.That(map.Name).IsNotEmpty();
                        }

                        await Assert.That(map)
                            .Member(m => m.MinLevel, m => m.IsGreaterThanOrEqualTo(0))
                            .And.Member(m => m.MaxLevel, m => m.IsGreaterThanOrEqualTo(map.MinLevel));
                        await Assert.That(floorIds).Contains(map.DefaultFloor);
                        foreach ((int poiId, PointOfInterest poi) in map.PointsOfInterest)
                        {
                            await Assert.That(poi)
                                .Member(p => p.Id, m => m.IsGreaterThan(0))
                                .And.Member(p => p.Name, m => m.IsNotNull())
                                .And.Member(p => p.ChatLink, m => m.IsNotEmpty());
                            if (poi is RequiresUnlockPointOfInterest locked)
                            {
                                await Assert.That(locked.IconUrl.IsAbsoluteUri).IsTrue().Because("Icon URL must be absolute to be a valid unlock indicator.");
                            }

                            PointOfInterestLink chatLink = poi.GetChatLink();
                            await Assert.That(chatLink)
                                .Member(c => c.PointOfInterestId, m => m.IsEqualTo(poiId))
                                .And.Member(c => c.ToString(), m => m.IsEqualTo(poi.ChatLink));
                            PointOfInterestLink chatLinkRoundtrip = PointOfInterestLink.Parse(chatLink.ToString());
                            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
                        }

                        foreach ((int heartId, Heart heart) in map.Hearts)
                        {
                            await Assert.That(heart)
                                .Member(h => h.Id, m => m.IsEqualTo(heartId))
                                .And.Member(h => h.Objective, m => m.IsNotEmpty())
                                .And.Member(h => h.Level, m => m.IsBetween(1, 80))
                                .And.Member(h => h.Coordinates, m => m.IsNotEqualTo(PointF.Empty))
                                .And.Member(h => h.Boundaries, m => m.IsNotEmpty());
                            foreach (PointF point in heart.Boundaries)
                            {
                                await Assert.That(point).IsNotEqualTo(PointF.Empty);
                            }
                            await Assert.That(heart.ChatLink).IsNotEmpty();
                        }

                        foreach (HeroChallenge heroChallenge in map.HeroChallenges)
                        {
                            const int cantha = 37;
                            const int castora = 58;
                            if (regionId is cantha or castora)
                            {
                                //https://github.com/gw2-api/issues/issues/35
                                await Assert.That(heroChallenge.Id).IsEmpty();
                            }
                            else
                            {
                                await Assert.That(heroChallenge.Id).IsNotEmpty();
                            }

                            await Assert.That(heroChallenge.Coordinates).IsNotEqualTo(PointF.Empty);
                        }

                        foreach ((int sectorId, Sector sector) in map.Sectors)
                        {
                            await Assert.That(sector)
                                .Member(s => s.Id, m => m.IsEqualTo(sectorId))
                                .And.Member(s => s.Name, m => m.IsNotNull())
                                .And.Member(s => s.Level, m => m.IsBetween(0, 80))
                                .And.Member(s => s.Coordinates, m => m.IsNotEqualTo(PointF.Empty))
                                .And.Member(s => s.Boundaries, m => m.IsNotEmpty());
                            foreach (PointF point in sector.Boundaries)
                            {
                                await Assert.That(point).IsNotEqualTo(PointF.Empty);
                            }
                            await Assert.That(sector.ChatLink).IsNotEmpty();
                        }

                        foreach (Adventure adventure in map.Adventures)
                        {
                            await Assert.That(adventure)
                                .Member(a => a.Id, m => m.IsNotEmpty())
                                .And.Member(a => a.Name, m => m.IsNotEmpty())
                                .And.Member(a => a.Description, m => m.IsNotEmpty())
                                .And.Member(a => a.Coordinates, m => m.IsNotEqualTo(PointF.Empty));
                        }

                        foreach (MasteryInsight masteryInsight in map.MasteryInsights)
                        {
                            await Assert.That(masteryInsight)
                                .Member(m => m.Id, m => m.IsGreaterThan(0))
                                .And.Member(m => m.Region.IsDefined(), m => m.IsTrue().Because($"Region {masteryInsight.Region} must be defined."))
                                .And.Member(m => m.Coordinates, m => m.IsNotEqualTo(PointF.Empty));
                        }

                        foreach (GodShrine godShrine in map.GodShrines ?? [])
                        {
                            await Assert.That(godShrine)
                                .Member(g => g.Id, m => m.IsGreaterThan(0))
                                .And.Member(g => g.PointOfInterestId, m => m.IsGreaterThan(0));
                            await Assert.That(map.PointsOfInterest.Keys).Contains(godShrine.PointOfInterestId);
                            await Assert.That(godShrine)
                                .Member(g => g.Name, m => m.IsNotEmpty())
                                .And.Member(g => g.NameContested, m => m.IsNotEmpty())
                                .And.Member(g => g.IconUrl, m => m.IsNotNull())
                                .And.Member(g => g.IconContestedUrl, m => m.IsNotNull())
                                .And.Member(g => g.Coordinates, m => m.IsNotEqualTo(PointF.Empty));
                            PointOfInterestLink link = godShrine.GetChatLink();
                            await Assert.That(link.PointOfInterestId).IsEqualTo(godShrine.PointOfInterestId);
                        }
                    }
                }
            }
        }
    }
}
