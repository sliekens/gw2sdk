using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Banking;

[PublicAPI]
public static class MaterialSlotJson
{
    public static MaterialSlot GetMaterialSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember category = new("category");
        OptionalMember binding = new("binding");
        RequiredMember count = new("count");

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
            ItemId = id.Select(value => value.GetInt32()),
            CategoryId = category.Select(value => value.GetInt32()),
            Binding = binding.Select(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
