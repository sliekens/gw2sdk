using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

[PublicAPI]
public static class MaterialSlotJson
{
    public static MaterialSlot GetCraftingMaterial(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> category = new("category");
        OptionalMember<ItemBinding> binding = new("binding");
        RequiredMember<int> count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(category.Name))
            {
                category.Value = member.Value;
            }
            else if (member.NameEquals(binding.Name))
            {
                binding.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaterialSlot
        {
            ItemId = id.GetValue(),
            CategoryId = category.GetValue(),
            Binding = binding.GetValue(missingMemberBehavior),
            Count = count.GetValue()
        };
    }
}
