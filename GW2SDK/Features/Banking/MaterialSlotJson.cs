using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Banking;

internal static class MaterialSlotJson
{
    public static MaterialSlot GetMaterialSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember category = "category";
        OptionalMember binding = "binding";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(category.Name))
            {
                category = member;
            }
            else if (member.NameEquals(binding.Name))
            {
                binding = member;
            }
            else if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaterialSlot
        {
            ItemId = id.Map(value => value.GetInt32()),
            CategoryId = category.Map(value => value.GetInt32()),
            Binding = binding.Map(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
