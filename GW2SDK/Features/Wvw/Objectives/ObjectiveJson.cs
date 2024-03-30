using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

internal static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember sectorId = "sector_id";
        RequiredMember type = "type";
        RequiredMember mapType = "map_type";
        RequiredMember mapId = "map_id";
        NullableMember upgradeId = "upgrade_id";
        NullableMember coordinates = "coord";
        NullableMember labelCoordinates = "label_coord";
        OptionalMember marker = "marker";
        RequiredMember chatLink = "chat_link";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (sectorId.Match(member))
            {
                sectorId = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (mapType.Match(member))
            {
                mapType = member;
            }
            else if (mapId.Match(member))
            {
                mapId = member;
            }
            else if (upgradeId.Match(member))
            {
                upgradeId = member;
            }
            else if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (labelCoordinates.Match(member))
            {
                labelCoordinates = member;
            }
            else if (marker.Match(member))
            {
                marker = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            SectorId = sectorId.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<ObjectiveKind>()),
            MapKind = mapType.Map(value => value.GetEnum<MapKind>()),
            MapId = mapId.Map(value => value.GetInt32()),
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinate3(missingMemberBehavior)),
            LabelCoordinates =
                labelCoordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            MarkerIconHref = marker.Map(value => value.GetString()) ?? "",
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}
