using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class GuildTeamMemberJson
{
    public static GuildTeamMember GetGuildTeamMember(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember role = new("role");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(role.Name))
            {
                role = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTeamMember
        {
            Name = name.Select(value => value.GetStringRequired()),
            Role = role.Select(value => value.GetEnum<GuildTeamRole>(missingMemberBehavior))
        };
    }
}
