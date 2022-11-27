using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Permissions;

[PublicAPI]
public static class GuildPermissionSummaryReader
{
    public static GuildPermissionSummary GetGuildPermissionSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<GuildPermission> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");

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
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildPermissionSummary
        {
            Id = id.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Description = description.GetValue()
        };
    }
}
