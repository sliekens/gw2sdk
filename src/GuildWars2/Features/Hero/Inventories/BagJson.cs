using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class BagJson
{
    public static Bag? GetBag(this in JsonElement json)
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember id = "id";
        RequiredMember size = "size";
        RequiredMember inventory = "inventory";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (size.Match(member))
            {
                size = member;
            }
            else if (inventory.Match(member))
            {
                inventory = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Bag
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Size = size.Map(static (in value) => value.GetInt32()),
            Inventory = inventory.Map(static (in value) => value.GetInventory())
        };
    }
}
