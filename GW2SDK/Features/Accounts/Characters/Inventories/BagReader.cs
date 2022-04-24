using System;
using System.Text.Json;
using GW2SDK.Accounts.Inventories;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters.Inventories;

[PublicAPI]
public static class BagReader
{
    public static Bag? Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember<int> id = new("id");
        RequiredMember<int> size = new("size");
        RequiredMember<Inventory> inventory = new("inventory");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(size.Name))
            {
                size = size.From(member.Value);
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory = inventory.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bag
        {
            Id = id.GetValue(),
            Size = size.GetValue(),
            Inventory = inventory.Select(value => InventoryReader.Read(value, missingMemberBehavior))
        };
    }
}
