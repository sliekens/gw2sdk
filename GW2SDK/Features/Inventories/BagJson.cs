using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

[PublicAPI]
public static class BagJson
{
    public static Bag? GetBag(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember id = new("id");
        RequiredMember size = new("size");
        RequiredMember inventory = new("inventory");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(size.Name))
            {
                size.Value = member.Value;
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bag
        {
            Id = id.Select(value => value.GetInt32()),
            Size = size.Select(value => value.GetInt32()),
            Inventory = inventory.Select(value => value.GetInventory(missingMemberBehavior))
        };
    }
}
