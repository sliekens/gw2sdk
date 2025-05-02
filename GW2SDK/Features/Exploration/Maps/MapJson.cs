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
    public static Map GetMap(this JsonElement json)
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
            if (name.Match(member))
            {
                name = member;
            }
            else if (minLevel.Match(member))
            {
                minLevel = member;
            }
            else if (maxLevel.Match(member))
            {
                maxLevel = member;
            }
            else if (defaultFloor.Match(member))
            {
                defaultFloor = member;
            }
            else if (labelCoordinates.Match(member))
            {
                labelCoordinates = member;
            }
            else if (mapRectangle.Match(member))
            {
                mapRectangle = member;
            }
            else if (continentRectangle.Match(member))
            {
                continentRectangle = member;
            }
            else if (pointsOfInterest.Match(member))
            {
                pointsOfInterest = member;
            }
            else if (godShrines.Match(member))
            {
                godShrines = member;
            }
            else if (tasks.Match(member))
            {
                tasks = member;
            }
            else if (skillChallenges.Match(member))
            {
                skillChallenges = member;
            }
            else if (sectors.Match(member))
            {
                sectors = member;
            }
            else if (adventures.Match(member))
            {
                adventures = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (masteryPoints.Match(member))
            {
                masteryPoints = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Map
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            MinLevel = minLevel.Map(static value => value.GetInt32()),
            MaxLevel = maxLevel.Map(static value => value.GetInt32()),
            DefaultFloor = defaultFloor.Map(static value => value.GetInt32()),
            LabelCoordinates = labelCoordinates.Map(static value => value.GetCoordinate()),
            MapRectangle = mapRectangle.Map(static value => value.GetMapRectangle()),
            ContinentRectangle =
                continentRectangle.Map(static value => value.GetContinentRectangle()),
            PointsOfInterest =
                pointsOfInterest.Map(static value =>
                    value.GetMap(static entry => entry.GetPointOfInterest())
                        .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
                ),
            GodShrines =
                godShrines.Map(static values => values.GetList(static value => value.GetGodShrine())
                ),
            Hearts =
                tasks.Map(static value => value.GetMap(static entry => entry.GetHeart())
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
                ),
            HeroChallenges =
                skillChallenges.Map(static values =>
                    values.GetList(static value => value.GetHeroChallenge())
                ),
            Sectors =
                sectors.Map(static value => value.GetMap(static entry => entry.GetSector())
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
                ),
            Adventures =
                adventures.Map(static values => values.GetList(static value => value.GetAdventure())
                ),
            MasteryInsights = masteryPoints.Map(static values =>
                values.GetList(static value => value.GetMasteryInsight())
            )
        };
    }
}
