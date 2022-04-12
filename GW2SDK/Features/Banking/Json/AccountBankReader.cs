using System;
using System.Collections.Generic;
using System.Text.Json;

using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Banking.Json;

[PublicAPI]
public static class AccountBankReader
{
    public static AccountBank Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        List<BankSlot?> slots = new(json.GetArrayLength());

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var slot in json.EnumerateArray())
        {
            slots.Add(ReadBankSlot(slot, missingMemberBehavior));
        }

        return new AccountBank(slots);
    }

    private static BankSlot? ReadBankSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
        OptionalMember<int> dyes = new("dyes");
        OptionalMember<ItemBinding> binding = new("binding");
        OptionalMember<string> boundTo = new("bound_to");
        OptionalMember<SelectedStat> stats = new("stats");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (member.NameEquals(charges.Name))
            {
                charges = charges.From(member.Value);
            }
            else if (member.NameEquals(skin.Name))
            {
                skin = skin.From(member.Value);
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades = upgrades.From(member.Value);
            }
            else if (member.NameEquals(upgradeSlotIndices.Name))
            {
                upgradeSlotIndices = upgradeSlotIndices.From(member.Value);
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions = infusions.From(member.Value);
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes = dyes.From(member.Value);
            }
            else if (member.NameEquals(binding.Name))
            {
                binding = binding.From(member.Value);
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo = boundTo.From(member.Value);
            }
            else if (member.NameEquals(stats.Name))
            {
                stats = stats.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BankSlot
        {
            Id = id.GetValue(),
            Count = count.GetValue(),
            Charges = charges.GetValue(),
            Skin = skin.GetValue(),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            UpgradeSlotIndices = upgradeSlotIndices.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            Dyes = dyes.SelectMany(value => value.GetInt32()),
            Binding = binding.GetValue(),
            BoundTo = boundTo.GetValueOrEmpty(),
            Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior))
        };
    }

    private static SelectedStat ReadSelectedStat(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SelectedModification> attributes = new("attributes");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = attributes.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedStat
        {
            Id = id.GetValue(),
            Attributes = attributes.Select(value => ReadSelectedModification(value, missingMemberBehavior))
        };
    }

    private static SelectedModification ReadSelectedModification(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> agonyResistance = new("AgonyResistance");
        NullableMember<int> boonDuration = new("BoonDuration");
        NullableMember<int> conditionDamage = new("ConditionDamage");
        NullableMember<int> conditionDuration = new("ConditionDuration");
        NullableMember<int> critDamage = new("CritDamage");
        NullableMember<int> healing = new("Healing");
        NullableMember<int> power = new("Power");
        NullableMember<int> precision = new("Precision");
        NullableMember<int> toughness = new("Toughness");
        NullableMember<int> vitality = new("Vitality");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(agonyResistance.Name))
            {
                agonyResistance = agonyResistance.From(member.Value);
            }
            else if (member.NameEquals(boonDuration.Name))
            {
                boonDuration = boonDuration.From(member.Value);
            }
            else if (member.NameEquals(conditionDamage.Name))
            {
                conditionDamage = conditionDamage.From(member.Value);
            }
            else if (member.NameEquals(conditionDuration.Name))
            {
                conditionDuration = conditionDuration.From(member.Value);
            }
            else if (member.NameEquals(critDamage.Name))
            {
                critDamage = critDamage.From(member.Value);
            }
            else if (member.NameEquals(healing.Name))
            {
                healing = healing.From(member.Value);
            }
            else if (member.NameEquals(power.Name))
            {
                power = power.From(member.Value);
            }
            else if (member.NameEquals(precision.Name))
            {
                precision = precision.From(member.Value);
            }
            else if (member.NameEquals(toughness.Name))
            {
                toughness = toughness.From(member.Value);
            }
            else if (member.NameEquals(vitality.Name))
            {
                vitality = vitality.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedModification
        {
            AgonyResistance = agonyResistance.GetValue(),
            BoonDuration = boonDuration.GetValue(),
            ConditionDamage = conditionDamage.GetValue(),
            ConditionDuration = conditionDuration.GetValue(),
            CritDamage = critDamage.GetValue(),
            Healing = healing.GetValue(),
            Power = power.GetValue(),
            Precision = precision.GetValue(),
            Toughness = toughness.GetValue(),
            Vitality = vitality.GetValue()
        };
    }
}