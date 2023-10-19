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
        RequiredMember background = new("background");
        RequiredMember foreground = new("foreground");
        RequiredMember flags = new("flags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(background.Name))
            {
                background.Value = member.Value;
            }
            else if (member.NameEquals(foreground.Name))
            {
                foreground.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
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

