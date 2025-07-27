using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class CategoriesJson
{
    public static (Extensible<Hue> hue, Extensible<Material> material, Extensible<ColorSet> set)
        GetCategories(this in JsonElement json)
    {
        if (json.GetArrayLength() == 0)
        {
            // Dye remover
            return (hue: default, material: default, set: default);
        }

        JsonElement hue = default;
        JsonElement material = default;
        JsonElement set = default;
        foreach (var entry in json.EnumerateArray())
        {
            if (hue.ValueKind == JsonValueKind.Undefined)
            {
                hue = entry;
            }
            else if (material.ValueKind == JsonValueKind.Undefined)
            {
                material = entry;
            }
            else if (set.ValueKind == JsonValueKind.Undefined)
            {
                set = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedArrayLength(json.GetArrayLength());
            }
        }

        return (hue: hue.GetEnum<Hue>(), material: material.GetEnum<Material>(),
            set: set.GetEnum<ColorSet>());
    }
}
