using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemForegroundJson
{
    public static GuildEmblemForeground GetGuildEmblemForeground(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember colors = "colors";

        foreach (JsonProperty member in json.EnumerateObject())
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

        return new GuildEmblemForeground
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            ColorIds = colors.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
        };
    }
}
