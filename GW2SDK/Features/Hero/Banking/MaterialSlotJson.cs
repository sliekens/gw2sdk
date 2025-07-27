using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialSlotJson
{
    public static MaterialSlot GetMaterialSlot(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember category = "category";
        OptionalMember binding = "binding";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (category.Match(member))
            {
                category = member;
            }
            else if (binding.Match(member))
            {
                binding = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MaterialSlot
        {
            ItemId = id.Map(static (in JsonElement value) => value.GetInt32()),
            CategoryId = category.Map(static (in JsonElement value) => value.GetInt32()),
            Binding = binding.Map(static (in JsonElement value) => value.GetEnum<ItemBinding>()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
