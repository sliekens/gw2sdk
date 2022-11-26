using System;
using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Amulets;

[PublicAPI]
public static class AttributesReader
{
    public static Dictionary<AttributeAdjustTarget, int> GetAttributes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var result = new Dictionary<AttributeAdjustTarget, int>(4);
        foreach (var member in json.EnumerateObject())
        {
            if (Enum.TryParse(member.Name, false, out AttributeAdjustTarget name))
            {
                result[name] = member.Value.GetInt32();
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return result;
    }
}
