using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

[PublicAPI]
public static class GuildEmblemJson
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
            if (member.NameEquals(background.Name))
            {
                background = member;
            }
            else if (member.NameEquals(foreground.Name))
            {
                foreground = member;
            }
            else if (member.NameEquals(flags.Name))
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
            Background = background.Select(value => value.GetGuildEmblemPart(missingMemberBehavior)),
            Foreground = foreground.Select(value => value.GetGuildEmblemPart(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<GuildEmblemFlag>(missingMemberBehavior))
        };
    }
}

