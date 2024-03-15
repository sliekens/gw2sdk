using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemBackgroundJson
{
    public static GuildEmblemBackground GetGuildEmblemBackground(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildEmblemBackground
        {
            Id = id.Map(value => value.GetInt32()),
            ColorIds = colors.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
