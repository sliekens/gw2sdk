using System.Text.Json;
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
        OptionalMember coordinates = "coord";
        NullableMember labelCoordinates = "label_coord";
        OptionalMember marker = "marker";
        RequiredMember chatLink = "chat_link";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(sectorId.Name))
            {
                sectorId = member;
            }
            else if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(mapType.Name))
            {
                mapType = member;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId = member;
            }
            else if (member.NameEquals(upgradeId.Name))
            {
                upgradeId = member;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = member;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates = member;
            }
            else if (member.NameEquals(marker.Name))
            {
                marker = member;
            }
            else if (member.NameEquals(chatLink.Name))
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
            Kind = type.Map(value => value.GetEnum<ObjectiveKind>(missingMemberBehavior)),
            MapKind = mapType.Map(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            MapId = mapId.Map(value => value.GetInt32()),
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinate3(missingMemberBehavior)),
            LabelCoordinates =
                labelCoordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            MarkerHref = marker.Map(value => value.GetString()) ?? "",
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}
