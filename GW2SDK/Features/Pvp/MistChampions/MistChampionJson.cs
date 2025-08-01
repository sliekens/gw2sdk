﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionJson
{
    public static MistChampion GetMistChampion(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember type = "type";
        RequiredMember stats = "stats";
        RequiredMember overlay = "overlay";
        RequiredMember underlay = "underlay";
        RequiredMember skins = "skins";

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
            else if (description.Match(member))
            {
                description = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (stats.Match(member))
            {
                stats = member;
            }
            else if (overlay.Match(member))
            {
                overlay = member;
            }
            else if (underlay.Match(member))
            {
                underlay = member;
            }
            else if (skins.Match(member))
            {
                skins = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var overlayString = overlay.Map(static (in JsonElement value) => value.GetStringRequired());
        var underlayString = underlay.Map(static (in JsonElement value) => value.GetStringRequired());
#pragma warning disable CS0618
        return new MistChampion
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
            Type = type.Map(static (in JsonElement value) => value.GetStringRequired()),
            Stats = stats.Map(static (in JsonElement value) => value.GetMistChampionStats()),
            OverlayImageHref = overlayString,
            OverlayImageUrl = new Uri(overlayString),
            UnderlayImageHref = underlayString,
            UnderlayImageUrl = new Uri(underlayString),
            Skins = skins.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetMistChampionSkin())
            )
        };
#pragma warning restore CS0618
    }
}
