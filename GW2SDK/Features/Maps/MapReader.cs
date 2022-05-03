using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Maps;

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
        RequiredMember<MapRectangle> mapRectangle = new("map_rect");
        RequiredMember<ContinentRectangle> continentRectangle = new("continent_rect");
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
                name = name.From(member.Value);
            }
            else if (member.NameEquals(minLevel.Name))
            {
                minLevel = minLevel.From(member.Value);
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel = maxLevel.From(member.Value);
            }
            else if (member.NameEquals(defaultFloor.Name))
            {
                defaultFloor = defaultFloor.From(member.Value);
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates = labelCoordinates.From(member.Value);
            }
            else if (member.NameEquals(mapRectangle.Name))
            {
                mapRectangle = mapRectangle.From(member.Value);
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle = continentRectangle.From(member.Value);
            }
            else if (member.NameEquals(pointsOfInterest.Name))
            {
                pointsOfInterest = pointsOfInterest.From(member.Value);
            }
            else if (member.NameEquals(godShrines.Name))
            {
                godShrines = godShrines.From(member.Value);
            }
            else if (member.NameEquals(tasks.Name))
            {
                tasks = tasks.From(member.Value);
            }
            else if (member.NameEquals(skillChallenges.Name))
            {
                skillChallenges = skillChallenges.From(member.Value);
            }
            else if (member.NameEquals(sectors.Name))
            {
                sectors = sectors.From(member.Value);
            }
            else if (member.NameEquals(adventures.Name))
            {
                adventures = adventures.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(masteryPoints.Name))
            {
                masteryPoints = masteryPoints.From(member.Value);
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
                labelCoordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            MapRectangle =
                mapRectangle.Select(value => ReadMapRectangle(value, missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(
                    value => ReadContinentRectangle(value, missingMemberBehavior)
                ),
            PointsOfInterest =
                pointsOfInterest.Select(
                    value => ReadPointsOfInterest(value, missingMemberBehavior)
                ),
            GodShrines =
                godShrines.SelectMany(value => ReadGodShrine(value, missingMemberBehavior)),
            Tasks = tasks.Select(value => ReadTasks(value, missingMemberBehavior)),
            SkillChallenges =
                skillChallenges.SelectMany(
                    value => ReadSkillChallenge(value, missingMemberBehavior)
                ),
            Sectors = sectors.Select(value => ReadSectors(value, missingMemberBehavior)),
            Adventures =
                adventures.SelectMany(value => ReadAdventure(value, missingMemberBehavior)),
            MasteryPoints =
                masteryPoints.SelectMany(item => ReadMasteryPoint(item, missingMemberBehavior))
        };
    }

    private static PointF ReadPointF(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        var x = json[0].GetSingle();
        var y = json[1].GetSingle();
        return new PointF(x, y);
    }

    private static MapRectangle ReadMapRectangle(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var bottomLeft = json[0];
        var x = bottomLeft[0].GetSingle();
        var y = bottomLeft[1].GetSingle();
        var size = json[1];
        var width = size[0].GetSingle();
        var height = size[1].GetSingle();
        return new MapRectangle
        {
            BottomLeft = new PointF(x, y),
            Size = new SizeF(width, height)
        };
    }

    private static ContinentRectangle ReadContinentRectangle(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var topLeft = json[0];
        var x = topLeft[0].GetSingle();
        var y = topLeft[1].GetSingle();
        var size = json[1];
        var width = size[0].GetSingle();
        var height = size[1].GetSingle();
        return new ContinentRectangle
        {
            TopLeft = new PointF(x, y),
            Size = new SizeF(width, height)
        };
    }

    private static MasteryPoint ReadMasteryPoint(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<MasteryRegionName> region = new("region");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(region.Name))
            {
                region = region.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPoint
        {
            Id = id.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            Region = region.GetValue(missingMemberBehavior)
        };
    }

    private static Adventure ReadAdventure(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> coordinates = new("coord");
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(description.Name))
            {
                description = description.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Adventure
        {
            Id = id.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            Name = name.GetValue(),
            Description = description.GetValue()
        };
    }

    private static Dictionary<int, MapSector> ReadSectors(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, MapSector> sectors = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                sectors[id] = member.Value.GetMapSector(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return sectors;
    }

    private static SkillChallenge ReadSkillChallenge(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> coordinates = new("coord");

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember<string> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillChallenge
        {
            Id = id.GetValueOrEmpty(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble())
        };
    }

    private static Dictionary<int, MapTask> ReadTasks(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, MapTask> tasks = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                tasks[id] = member.Value.GetMapTask(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return tasks;
    }

    private static GodShrine ReadGodShrine(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> nameContested = new("name_contested");
        RequiredMember<int> pointOfInterestId = new("poi_id");
        RequiredMember<double> coordinates = new("coord");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> iconContested = new("icon_contested");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(nameContested.Name))
            {
                nameContested = nameContested.From(member.Value);
            }
            else if (member.NameEquals(pointOfInterestId.Name))
            {
                pointOfInterestId = pointOfInterestId.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(iconContested.Name))
            {
                iconContested = iconContested.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GodShrine
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            NameContested = nameContested.GetValue(),
            PointOfInterestId = pointOfInterestId.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            Icon = icon.GetValue(),
            IconContested = iconContested.GetValue()
        };
    }

    private static Dictionary<int, PointOfInterest> ReadPointsOfInterest(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, PointOfInterest> pointsOfInterest = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                pointsOfInterest[id] = member.Value.GetPointOfInterest(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return pointsOfInterest;
    }
}
