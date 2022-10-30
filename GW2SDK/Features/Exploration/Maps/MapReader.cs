using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
public static class MapReader
{
    public static Map GetMap(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<int> minLevel = new("min_level");
        RequiredMember<int> maxLevel = new("max_level");
        RequiredMember<int> defaultFloor = new("default_floor");
        OptionalMember<PointF> labelCoordinates = new("label_coord");
        RequiredMember<MapView2> mapRectangle = new("map_rect");
        RequiredMember<MapView> continentRectangle = new("continent_rect");
        RequiredMember<Dictionary<int, PointOfInterest>> pointsOfInterest =
            new("points_of_interest");
        OptionalMember<GodShrine> godShrines = new("god_shrines");
        RequiredMember<Dictionary<int, MapTask>> tasks = new("tasks");
        RequiredMember<SkillChallenge> skillChallenges = new("skill_challenges");
        RequiredMember<Dictionary<int, MapSector>> sectors = new("sectors");
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
            MapRectangle = mapRectangle.Select(value => value.GetMapView2(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetMapView(missingMemberBehavior)),
            PointsOfInterest =
                pointsOfInterest.Select(
                    value => value.GetMapPointsOfInterest(missingMemberBehavior)
                ),
            GodShrines = godShrines.SelectMany(value => value.GetGodShrine(missingMemberBehavior)),
            Tasks = tasks.Select(value => value.GetMapTasks(missingMemberBehavior)),
            SkillChallenges =
                skillChallenges.SelectMany(value => value.GetSkillChallenge(missingMemberBehavior)),
            Sectors = sectors.Select(value => value.GetMapSectors(missingMemberBehavior)),
            Adventures = adventures.SelectMany(value => value.GetAdventure(missingMemberBehavior)),
            MasteryPoints =
                masteryPoints.SelectMany(item => item.GetMasteryPoint(missingMemberBehavior))
        };
    }
}
