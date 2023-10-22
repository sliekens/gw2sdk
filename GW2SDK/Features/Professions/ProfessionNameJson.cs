using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

internal static class ProfessionNameJson
{
    public static ProfessionName GetProfessionName(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var text = json.GetStringRequired();
        if (Enum.TryParse(text, out ProfessionName name))
        {
            return name;
        }

        if (missingMemberBehavior == MissingMemberBehavior.Error)
        {
            throw new InvalidOperationException(Strings.UnexpectedMember(text));
        }

        return (ProfessionName)text.GetDeterministicHashCode();
    }
}
