using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
public static class DyeSlotJson
{
    public static DyeSlot GetDyeSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> colorId = new("color_id");
        RequiredMember<Material> material = new("material");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(colorId.Name))
            {
                colorId.Value = member.Value;
            }
            else if (member.NameEquals(material.Name))
            {
                material.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DyeSlot
        {
            ColorId = colorId.GetValue(),
            Material = material.GetValue(missingMemberBehavior)
        };
    }
}
