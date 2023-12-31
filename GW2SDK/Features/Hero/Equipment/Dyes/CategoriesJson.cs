using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

internal static class CategoriesJson
{
    public static (Hue hue, Material material, ColorSet set) GetCategories(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return (hue: hue.GetEnum<Hue>(missingMemberBehavior),
            material: material.GetEnum<Material>(missingMemberBehavior),
            set: set.GetEnum<ColorSet>(missingMemberBehavior));
    }
}
