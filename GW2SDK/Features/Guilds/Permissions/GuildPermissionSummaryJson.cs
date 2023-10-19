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
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";

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
            Id = id.Map(value => value.GetEnum<GuildPermission>(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired())
        };
    }
}
