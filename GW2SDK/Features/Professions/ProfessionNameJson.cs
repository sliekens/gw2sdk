using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public static class ProfessionNameJson
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
