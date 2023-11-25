using System.Text.Json;
using GuildWars2.Exploration.Adventures;
using GuildWars2.Exploration.GodShrines;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Exploration.HeroChallenges;
using GuildWars2.Exploration.MasteryInsights;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Exploration.Sectors;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

internal static class MapJson
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
        RequiredMember pointsOfInterest = "points_of_interest";
        OptionalMember godShrines = "god_shrines";
        RequiredMember tasks = "tasks";
        RequiredMember skillChallenges = "skill_challenges";
        RequiredMember sectors = "sectors";
        RequiredMember adventures = "adventures";
        RequiredMember id = "id";
        RequiredMember masteryPoints = "mastery_points";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == minLevel.Name)
            {
                minLevel = member;
            }
            else if (member.Name == maxLevel.Name)
            {
                maxLevel = member;
            }
            else if (member.Name == defaultFloor.Name)
            {
                defaultFloor = member;
            }
            else if (member.Name == labelCoordinates.Name)
            {
                labelCoordinates = member;
            }
            else if (member.Name == mapRectangle.Name)
            {
                mapRectangle = member;
            }
            else if (member.Name == continentRectangle.Name)
            {
                continentRectangle = member;
            }
            else if (member.Name == pointsOfInterest.Name)
            {
                pointsOfInterest = member;
            }
            else if (member.Name == godShrines.Name)
            {
                godShrines = member;
            }
            else if (member.Name == tasks.Name)
            {
                tasks = member;
            }
            else if (member.Name == skillChallenges.Name)
            {
                skillChallenges = member;
            }
            else if (member.Name == sectors.Name)
            {
                sectors = member;
            }
            else if (member.Name == adventures.Name)
            {
                adventures = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == masteryPoints.Name)
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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            MinLevel = minLevel.Map(value => value.GetInt32()),
            MaxLevel = maxLevel.Map(value => value.GetInt32()),
            DefaultFloor = defaultFloor.Map(value => value.GetInt32()),
            LabelCoordinates =
                labelCoordinates.Map(value => value.GetCoordinate(missingMemberBehavior)),
            MapRectangle = mapRectangle.Map(value => value.GetMapRectangle(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Map(value => value.GetContinentRectangle(missingMemberBehavior)),
            PointsOfInterest =
                pointsOfInterest.Map(
                    value =>
                        value.GetMap(entry => entry.GetPointOfInterest(missingMemberBehavior))
                            .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
                ),
            GodShrines =
                godShrines.Map(
                    values => values.GetList(value => value.GetGodShrine(missingMemberBehavior))
                ),
            Hearts =
                tasks.Map(
                    value =>
                        value.GetMap(entry => entry.GetHeart(missingMemberBehavior))
                            .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
                ),
            HeroChallenges =
                skillChallenges.Map(
                    values => values.GetList(value => value.GetHeroChallenge(missingMemberBehavior))
                ),
            Sectors = sectors.Map(
                value =>
                    value.GetMap(entry => entry.GetSector(missingMemberBehavior))
                        .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
            Adventures =
                adventures.Map(
                    values => values.GetList(value => value.GetAdventure(missingMemberBehavior))
                ),
            MasteryInsights = masteryPoints.Map(
                values => values.GetList(item => item.GetMasteryInsight(missingMemberBehavior))
            )
        };
    }
}
