using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Emblems;

internal static class GuildEmblemJson
{
    public static GuildEmblem GetGuildEmblem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildEmblem
        {
            Background =
                background.Map(value => value.GetGuildEmblemBackground(missingMemberBehavior)),
            Foreground =
                foreground.Map(value => value.GetGuildEmblemForeground(missingMemberBehavior)),
            Flags = flags.Map(values => values.GetGuildEmblemFlags())
        };
    }
}
