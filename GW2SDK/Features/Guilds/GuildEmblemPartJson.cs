using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

internal static class GuildEmblemPartJson
{
    public static GuildEmblemPart GetGuildEmblemPart(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember colors = "colors";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == colors.Name)
            {
                colors = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildEmblemPart
        {
            Id = id.Map(value => value.GetInt32()),
            Colors = colors.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
