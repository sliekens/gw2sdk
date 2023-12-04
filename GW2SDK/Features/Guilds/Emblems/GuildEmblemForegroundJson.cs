using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemForegroundJson
{
    public static GuildEmblemForeground GetGuildEmblemForeground(
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

        return new GuildEmblemForeground
        {
            Id = id.Map(value => value.GetInt32()),
            ColorIds = colors.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
