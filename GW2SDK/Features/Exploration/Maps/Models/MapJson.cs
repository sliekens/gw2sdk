using System.Drawing;
using System.Text.Json;
using GuildWars2.Exploration.Adventures;
using GuildWars2.Exploration.GodShrines;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.MasteryPoints;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Sectors;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
public static class MapJson
{
    public static Map GetMap(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<int> minLevel = new("min_level");
        RequiredMember<int> maxLevel = new("max_level");
        RequiredMember<int> defaultFloor = new("default_floor");
        OptionalMember<Point> labelCoordinates = new("label_coord");
        RequiredMember<Rectangle> mapRectangle = new("map_rect");
        RequiredMember<Rectangle> continentRectangle = new("continent_rect");
        RequiredMember<Dictionary<int, PointOfInterest>> pointsOfInterest =
            new("points_of_interest");
        OptionalMember<GodShrine> godShrines = new("god_shrines");
        RequiredMember<Dictionary<int, Heart>> tasks = new("tasks");
        RequiredMember<HeroChallenge> skillChallenges = new("skill_challenges");
        RequiredMember<Dictionary<int, Sector>> sectors = new("sectors");
        RequiredMember<Adventure> adventures = new("adventures");
        RequiredMember<int> id = new("id");
        RequiredMember<MasteryPoint> masteryPoints = new("mastery_points");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(minLevel.Name))
            {
                minLevel.Value = member.Value;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel.Value = member.Value;
            }
            else if (member.NameEquals(defaultFloor.Name))
            {
                defaultFloor.Value = member.Value;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates.Value = member.Value;
            }
            else if (member.NameEquals(mapRectangle.Name))
            {
                mapRectangle.Value = member.Value;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle.Value = member.Value;
            }
            else if (member.NameEquals(pointsOfInterest.Name))
            {
                pointsOfInterest.Value = member.Value;
            }
            else if (member.NameEquals(godShrines.Name))
            {
                godShrines.Value = member.Value;
            }
            else if (member.NameEquals(tasks.Name))
            {
                tasks.Value = member.Value;
            }
            else if (member.NameEquals(skillChallenges.Name))
            {
                skillChallenges.Value = member.Value;
            }
            else if (member.NameEquals(sectors.Name))
            {
                sectors.Value = member.Value;
            }
            else if (member.NameEquals(adventures.Name))
            {
                adventures.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(masteryPoints.Name))
            {
                masteryPoints.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Map
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            MinLevel = minLevel.GetValue(),
            MaxLevel = maxLevel.GetValue(),
            DefaultFloor = defaultFloor.GetValue(),
            LabelCoordinates =
                labelCoordinates.Select(value => value.GetCoordinate(missingMemberBehavior)),
            MapRectangle = mapRectangle.Select(value => value.GetMapRectangle(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetContinentRectangle(missingMemberBehavior)),
            PointsOfInterest = pointsOfInterest.Select(
                value => value.GetMap(entry => entry.GetPointOfInterest(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            GodShrines = godShrines.SelectMany(value => value.GetGodShrine(missingMemberBehavior)),
            Hearts = tasks.Select(
                value => value.GetMap(entry => entry.GetHeart(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            HeroChallenges = skillChallenges.SelectMany(value => value.GetHeroChallenge(missingMemberBehavior)),
            Sectors = sectors.Select(
                value => value.GetMap(entry => entry.GetSector(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            Adventures = adventures.SelectMany(value => value.GetAdventure(missingMemberBehavior)),
            MasteryPoints =
                masteryPoints.SelectMany(item => item.GetMasteryPoint(missingMemberBehavior))
        };
    }
}
