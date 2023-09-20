using System.Drawing;
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
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> sectorId = new("sector_id");
        RequiredMember<ObjectiveKind> type = new("type");
        RequiredMember<MapKind> mapType = new("map_type");
        RequiredMember<int> mapId = new("map_id");
        NullableMember<int> upgradeId = new("upgrade_id");
        OptionalMember<double> coordinates = new("coord");
        NullableMember<PointF> labelCoordinates = new("label_coord");
        OptionalMember<string> marker = new("marker");
        RequiredMember<string> chatLink = new("chat_link");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            SectorId = sectorId.GetValue(),
            Kind = type.GetValue(missingMemberBehavior),
            MapKind = mapType.GetValue(missingMemberBehavior),
            MapId = mapId.GetValue(),
            UpgradeId = upgradeId.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            LabelCoordinates =
                labelCoordinates.Select(value => value.GetLabelCoordinate(missingMemberBehavior)),
            MarkerHref = marker.GetValueOrEmpty(),
            ChatLink = chatLink.GetValue()
        };
    }
}
