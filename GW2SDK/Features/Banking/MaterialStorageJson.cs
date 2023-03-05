using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

[PublicAPI]
public static class MaterialStorageJson
{
    public static MaterialStorage GetMaterialStorage(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        List<MaterialSlot> slots = new(json.GetArrayLength());

        slots.AddRange(
            json.EnumerateArray().Select(entry => entry.GetMaterialSlot(missingMemberBehavior))
        );

        return new MaterialStorage(slots);
    }
}
