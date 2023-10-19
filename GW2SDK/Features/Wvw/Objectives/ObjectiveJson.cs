using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
public static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember sectorId = new("sector_id");
        RequiredMember type = new("type");
        RequiredMember mapType = new("map_type");
        RequiredMember mapId = new("map_id");
        NullableMember upgradeId = new("upgrade_id");
        OptionalMember coordinates = new("coord");
        NullableMember labelCoordinates = new("label_coord");
        OptionalMember marker = new("marker");
        RequiredMember chatLink = new("chat_link");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(sectorId.Name))
            {
                sectorId.Value = member.Value;
            }
            else if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(mapType.Name))
            {
                mapType.Value = member.Value;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId.Value = member.Value;
            }
            else if (member.NameEquals(upgradeId.Name))
            {
                upgradeId.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates.Value = member.Value;
            }
            else if (member.NameEquals(marker.Name))
            {
                marker.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            SectorId = sectorId.Select(value => value.GetInt32()),
            Kind = type.Select(value => value.GetEnum<ObjectiveKind>(missingMemberBehavior)),
            MapKind = mapType.Select(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            MapId = mapId.Select(value => value.GetInt32()),
            UpgradeId = upgradeId.Select(value => value.GetInt32()),
            Coordinates = coordinates.Select(value => value.GetCoordinate3(missingMemberBehavior)),
            LabelCoordinates =
                labelCoordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            MarkerHref = marker.Select(value => value.GetString()) ?? "",
            ChatLink = chatLink.Select(value => value.GetStringRequired())
        };
    }
}
