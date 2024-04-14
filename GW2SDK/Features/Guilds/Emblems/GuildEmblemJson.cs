using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemJson
{
    public static GuildEmblem GetGuildEmblem(
        this JsonElement json
    )
    {
        RequiredMember background = "background";
        RequiredMember foreground = "foreground";
        RequiredMember flags = "flags";

        foreach (var member in json.EnumerateObject())
        {
            if (background.Match(member))
            {
                background = member;
            }
            else if (foreground.Match(member))
            {
                foreground = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildEmblem
        {
            Background =
                background.Map(static value => value.GetGuildEmblemBackground()),
            Foreground =
                foreground.Map(static value => value.GetGuildEmblemForeground()),
            Flags = flags.Map(static values => values.GetGuildEmblemFlags())
        };
    }
}
