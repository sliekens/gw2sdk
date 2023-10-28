using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

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
            if (member.Name == background.Name)
            {
                background = member;
            }
            else if (member.Name == foreground.Name)
            {
                foreground = member;
            }
            else if (member.Name == flags.Name)
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
                background.Map(value => value.GetGuildEmblemPart(missingMemberBehavior)),
            Foreground =
                foreground.Map(value => value.GetGuildEmblemPart(missingMemberBehavior)),
            Flags = flags.Map(
                values => values.GetList(
                    value => value.GetEnum<GuildEmblemFlag>(missingMemberBehavior)
                )
            )
        };
    }
}
