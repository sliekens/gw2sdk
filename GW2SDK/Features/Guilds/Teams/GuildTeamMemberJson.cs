﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

internal static class GuildTeamMemberJson
{
    public static GuildTeamMember GetGuildTeamMember(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember role = "role";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (role.Match(member))
            {
                role = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildTeamMember
        {
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Role = role.Map(static (in JsonElement value) => value.GetEnum<GuildTeamRole>())
        };
    }
}
