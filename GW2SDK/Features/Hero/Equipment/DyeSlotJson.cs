using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class DyeSlotJson
{
    public static DyeSlot GetDyeSlot(this in JsonElement json)
    {
        RequiredMember colorId = "color_id";
        RequiredMember material = "material";
        foreach (var member in json.EnumerateObject())
        {
            if (colorId.Match(member))
            {
                colorId = member;
            }
            else if (material.Match(member))
            {
                material = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new DyeSlot
        {
            ColorId = colorId.Map(static (in JsonElement value) => value.GetInt32()),
            Material = material.Map(static (in JsonElement value) => value.GetEnum<Material>())
        };
    }
}
