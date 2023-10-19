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
        RequiredMember name = "name";
        RequiredMember minLevel = "min_level";
        RequiredMember maxLevel = "max_level";
        RequiredMember defaultFloor = "default_floor";
        OptionalMember labelCoordinates = "label_coord";
        RequiredMember mapRectangle = "map_rect";
        RequiredMember continentRectangle = "continent_rect";
        RequiredMember pointsOfInterest =
            "points_of_interest";
        OptionalMember godShrines = "god_shrines";
        RequiredMember tasks = "tasks";
        RequiredMember skillChallenges = "skill_challenges";
        RequiredMember sectors = "sectors";
        RequiredMember adventures = "adventures";
        RequiredMember id = "id";
        RequiredMember masteryPoints = "mastery_points";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(minLevel.Name))
            {
                minLevel = member;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel = member;
            }
            else if (member.NameEquals(defaultFloor.Name))
            {
                defaultFloor = member;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates = member;
            }
            else if (member.NameEquals(mapRectangle.Name))
            {
                mapRectangle = member;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle = member;
            }
            else if (member.NameEquals(pointsOfInterest.Name))
            {
                pointsOfInterest = member;
            }
            else if (member.NameEquals(godShrines.Name))
            {
                godShrines = member;
            }
            else if (member.NameEquals(tasks.Name))
            {
                tasks = member;
            }
            else if (member.NameEquals(skillChallenges.Name))
            {
                skillChallenges = member;
            }
            else if (member.NameEquals(sectors.Name))
            {
                sectors = member;
            }
            else if (member.NameEquals(adventures.Name))
            {
                adventures = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(masteryPoints.Name))
            {
                masteryPoints = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Map
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            MinLevel = minLevel.Select(value => value.GetInt32()),
            MaxLevel = maxLevel.Select(value => value.GetInt32()),
            DefaultFloor = defaultFloor.Select(value => value.GetInt32()),
            LabelCoordinates =
                labelCoordinates.Select(value => value.GetCoordinate(missingMemberBehavior)),
            MapRectangle = mapRectangle.Select(value => value.GetMapRectangle(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetContinentRectangle(missingMemberBehavior)),
            PointsOfInterest = pointsOfInterest.Select(
                value => value.GetMap(entry => entry.GetPointOfInterest(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            GodShrines = godShrines.Select(values => values.GetList(value => value.GetGodShrine(missingMemberBehavior))),
            Hearts = tasks.Select(
                value => value.GetMap(entry => entry.GetHeart(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            HeroChallenges = skillChallenges.Select(values => values.GetList(value => value.GetHeroChallenge(missingMemberBehavior))),
            Sectors = sectors.Select(
                value => value.GetMap(entry => entry.GetSector(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            Adventures = adventures.Select(values => values.GetList(value => value.GetAdventure(missingMemberBehavior))),
            MasteryPoints =
                masteryPoints.Select(values => values.GetList(item => item.GetMasteryPoint(missingMemberBehavior)))
        };
    }
}
