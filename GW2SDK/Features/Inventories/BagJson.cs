using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

internal static class BagJson
{
    public static Bag? GetBag(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember id = "id";
        RequiredMember size = "size";
        RequiredMember inventory = "inventory";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(size.Name))
            {
                size = member;
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bag
        {
            Id = id.Map(value => value.GetInt32()),
            Size = size.Map(value => value.GetInt32()),
            Inventory = inventory.Map(value => value.GetInventory(missingMemberBehavior))
        };
    }
}
