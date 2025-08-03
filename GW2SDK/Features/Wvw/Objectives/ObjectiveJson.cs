using System.Text.Json;

using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

internal static class ObjectiveJson
{
    public static Objective GetObjective(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Camp":
                    return json.GetCamp();
                case "Castle":
                    return json.GetCastle();
                case "Keep":
                    return json.GetKeep();
                case "Mercenary":
                    return json.GetMercenary();
                case "Resource":
                    return json.GetResource();
                case "Ruins":
                    return json.GetRuins();
                case "Spawn":
                    return json.GetSpawn();
                case "Tower":
                    return json.GetTower();
            }
        }

        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember sectorId = "sector_id";
        RequiredMember mapType = "map_type";
        RequiredMember mapId = "map_id";
        NullableMember upgradeId = "upgrade_id";
        NullableMember coordinates = "coord";
        NullableMember labelCoordinates = "label_coord";
        OptionalMember marker = "marker";
        RequiredMember chatLink = "chat_link";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error
                    && !member.Value.ValueEquals("Generic"))
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)

            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Objective
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            SectorId = sectorId.Map(static (in JsonElement value) => value.GetInt32()),
            MapKind = mapType.Map(static (in JsonElement value) => value.GetEnum<MapKind>()),
            MapId = mapId.Map(static (in JsonElement value) => value.GetInt32()),
            UpgradeId = upgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in JsonElement value) => value.GetCoordinate3()),
            LabelCoordinates = labelCoordinates.Map(static (in JsonElement value) => value.GetCoordinateF()),
#pragma warning disable CS0618 // Suppress obsolete warning
            MarkerIconHref = marker.Map(static (in JsonElement value) => value.GetString()) ?? "",
#pragma warning restore CS0618
            MarkerIconUrl = marker.Map(static (in JsonElement value) => value.GetString() is { } url ? new Uri(url) : null),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
