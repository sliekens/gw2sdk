﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemBackgroundJson
{
    public static GuildEmblemBackground GetGuildEmblemBackground(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember colors = "colors";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (colors.Match(member))
            {
                colors = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildEmblemBackground
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            ColorIds = colors.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
        };
    }
}
