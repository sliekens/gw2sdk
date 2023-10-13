using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

[PublicAPI]
public static class GuildEmblemPartJson
{
    public static GuildEmblemPart GetGuildEmblemPart(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> colors = new("colors");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(colors.Name))
            {
                colors.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildEmblemPart
        {
            Id = id.GetValue(),
            Colors = colors.GetValues()
        };
    }
}
