using System.Text.Json;
using GuildWars2.ItemStats;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

[PublicAPI]
public static class ItemSlotJson
{
    public static ItemSlot? GetItemSlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        RequiredMember id = "id";
        RequiredMember count = "count";
        NullableMember charges = "charges";
        NullableMember skin = "skin";
        OptionalMember upgrades = "upgrades";
        OptionalMember upgradeSlotIndices = "upgrade_slot_indices";
        OptionalMember infusions = "infusions";
        OptionalMember dyes = "dyes";
        OptionalMember binding = "binding";
        OptionalMember boundTo = "bound_to";
        OptionalMember stats = "stats";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (member.NameEquals(charges.Name))
            {
                charges = member;
            }
            else if (member.NameEquals(skin.Name))
            {
                skin = member;
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades = member;
            }
            else if (member.NameEquals(upgradeSlotIndices.Name))
            {
                upgradeSlotIndices = member;
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions = member;
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes = member;
            }
            else if (member.NameEquals(binding.Name))
            {
                binding = member;
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo = member;
            }
            else if (member.NameEquals(stats.Name))
            {
                stats = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemSlot
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Charges = charges.Map(value => value.GetInt32()),
            Skin = skin.Map(value => value.GetInt32()),
            Upgrades = upgrades.Map(values => values.GetList(value => value.GetInt32())),
            UpgradeSlotIndices =
                upgradeSlotIndices.Map(values => values.GetList(value => value.GetInt32())),
            Infusions = infusions.Map(values => values.GetList(value => value.GetInt32())),
            Dyes = dyes.Map(values => values.GetList(value => value.GetNullableInt32())),
            Binding = binding.Map(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            BoundTo = boundTo.Map(value => value.GetString()) ?? "",
            Stats = stats.Map(value => value.GetSelectedStat(missingMemberBehavior))
        };
    }
}
