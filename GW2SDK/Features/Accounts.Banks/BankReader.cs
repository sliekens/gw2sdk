using System;
using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankReader : IBankReader
    {
        public Bank Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var slots = new List<BankSlot?>(json.GetArrayLength());

            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var slot in json.EnumerateArray())
            {
                slots.Add(ReadBankSlot(slot, missingMemberBehavior));
            }

            return new Bank(slots);
        }

        private BankSlot? ReadBankSlot(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // Empty slots are represented as null -- but maybe we should use a Null Object pattern here
            if (json.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            var id = new RequiredMember<int>("id");
            var count = new RequiredMember<int>("count");
            var charges = new NullableMember<int>("charges");
            var skin = new NullableMember<int>("skin");
            var upgrades = new OptionalMember<int[]>("upgrades");
            var upgradeSlotIndices = new OptionalMember<int[]>("upgrade_slot_indices");
            var infusions = new OptionalMember<int[]>("infusions");
            var dyes = new OptionalMember<int[]>("dyes");
            var binding = new OptionalMember<ItemBinding>("binding");
            var boundTo = new OptionalMember<string>("bound_to");
            var stats = new OptionalMember<SelectedStat>("stats");
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
                Upgrades = upgrades.Select(value => value.GetArray(item => item.GetInt32())),
                UpgradeSlotIndices = upgradeSlotIndices.Select(value => value.GetArray(item => item.GetInt32())),
                Infusions = infusions.Select(value => value.GetArray(item => item.GetInt32())),
                Dyes = dyes.Select(value => value.GetArray(item => item.GetInt32())),
                Binding = binding.GetValue(),
                BoundTo = boundTo.GetValueOrEmpty(),
                Stats = stats.Select(value => ReadSelectedStat(value, missingMemberBehavior))
            };
        }

        private SelectedStat ReadSelectedStat(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var attributes = new RequiredMember<SelectedModification>("attributes");
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

            return new SelectedStat { Id = id.GetValue(), Attributes = attributes.Select(value => ReadSelectedModification(value, missingMemberBehavior)) };
        }

        private SelectedModification ReadSelectedModification(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var agonyResistance = new NullableMember<int>("AgonyResistance");
            var boonDuration = new NullableMember<int>("BoonDuration");
            var conditionDamage = new NullableMember<int>("ConditionDamage");
            var conditionDuration = new NullableMember<int>("ConditionDuration");
            var critDamage = new NullableMember<int>("CritDamage");
            var healing = new NullableMember<int>("Healing");
            var power = new NullableMember<int>("Power");
            var precision = new NullableMember<int>("Precision");
            var toughness = new NullableMember<int>("Toughness");
            var vitality = new NullableMember<int>("Vitality");
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
}
