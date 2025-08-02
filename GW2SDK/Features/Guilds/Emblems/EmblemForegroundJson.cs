﻿using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class EmblemForegroundJson
{
    public static EmblemForeground GetEmblemForeground(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember layers = "layers";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (layers.Match(member))
            {
                layers = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new EmblemForeground
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning
            Layers = layers.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetStringRequired())
            ),
#pragma warning restore CS0618
            LayerUrls = layers.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => new Uri(value.GetStringRequired()))
            )
        };
    }
}
