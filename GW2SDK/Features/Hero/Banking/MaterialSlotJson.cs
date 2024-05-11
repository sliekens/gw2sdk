using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialSlotJson
{
    public static MaterialSlot GetMaterialSlot(this JsonElement json)
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaterialSlot
        {
            ItemId = id.Map(static value => value.GetInt32()),
            CategoryId = category.Map(static value => value.GetInt32()),
            Binding = binding.Map(static value => value.GetEnum<ItemBinding>()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}
