using System.Globalization;
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
    public static Map GetMap(this in JsonElement json)
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
        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            MinLevel = minLevel.Map(static (in value) => value.GetInt32()),
            MaxLevel = maxLevel.Map(static (in value) => value.GetInt32()),
            DefaultFloor = defaultFloor.Map(static (in value) => value.GetInt32()),
            LabelCoordinates = labelCoordinates.Map(static (in value) => value.GetCoordinate()),
            MapRectangle = mapRectangle.Map(static (in value) => value.GetMapRectangle()),
            ContinentRectangle =
                continentRectangle.Map(static (in value) => value.GetContinentRectangle()),
            PointsOfInterest =
                pointsOfInterest.Map(static (in value) =>
                    value.GetMap(
                        static key => int.Parse(key, CultureInfo.InvariantCulture),
                        static (in entry) => entry.GetPointOfInterest())
                ),
            GodShrines =
                godShrines.Map(static (in values) => values.GetList(static (in value) => value.GetGodShrine())
                ),
            Hearts =
                tasks.Map(static (in value) => value.GetMap(
                    static key => int.Parse(key, CultureInfo.InvariantCulture),
                    static (in entry) => entry.GetHeart())
                ),
            HeroChallenges =
                skillChallenges.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetHeroChallenge())
                ),
            Sectors =
                sectors.Map(static (in value) => value.GetMap(
                    static key => int.Parse(key, CultureInfo.InvariantCulture),
                    static (in entry) => entry.GetSector())
                ),
            Adventures =
                adventures.Map(static (in values) => values.GetList(static (in value) => value.GetAdventure())
                ),
            MasteryInsights = masteryPoints.Map(static (in values) =>
                values.GetList(static (in value) => value.GetMasteryInsight())
            )
        };
    }
}
