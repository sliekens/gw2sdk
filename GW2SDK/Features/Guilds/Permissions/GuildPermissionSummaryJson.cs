using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Permissions;

[PublicAPI]
public static class GuildPermissionSummaryJson
{
    public static GuildPermissionSummary GetGuildPermissionSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");

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
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildPermissionSummary
        {
            Id = id.Select(value => value.GetEnum<GuildPermission>(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired())
        };
    }
}
