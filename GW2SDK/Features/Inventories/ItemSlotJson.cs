using System;
using System.Text.Json;
using GW2SDK.ItemStats;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Inventories;

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

        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");
        NullableMember<int> charges = new("charges");
        NullableMember<int> skin = new("skin");
        OptionalMember<int> upgrades = new("upgrades");
        OptionalMember<int> upgradeSlotIndices = new("upgrade_slot_indices");
        OptionalMember<int> infusions = new("infusions");
        OptionalMember<int?> dyes = new("dyes");
        OptionalMember<ItemBinding> binding = new("binding");
        OptionalMember<string> boundTo = new("bound_to");
        OptionalMember<SelectedStat> stats = new("stats");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(charges.Name))
            {
                charges.Value = member.Value;
            }
            else if (member.NameEquals(skin.Name))
            {
                skin.Value = member.Value;
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades.Value = member.Value;
            }
            else if (member.NameEquals(upgradeSlotIndices.Name))
            {
                upgradeSlotIndices.Value = member.Value;
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions.Value = member.Value;
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes.Value = member.Value;
            }
            else if (member.NameEquals(binding.Name))
            {
                binding.Value = member.Value;
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo.Value = member.Value;
            }
            else if (member.NameEquals(stats.Name))
            {
                stats.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemSlot
        {
            Id = id.GetValue(),
            Count = count.GetValue(),
            Charges = charges.GetValue(),
            Skin = skin.GetValue(),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            UpgradeSlotIndices = upgradeSlotIndices.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            Dyes =
                dyes.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Binding = binding.GetValue(missingMemberBehavior),
            BoundTo = boundTo.GetValueOrEmpty(),
            Stats = stats.Select(value => value.GetSelectedStat(missingMemberBehavior))
        };
    }
}
