using System;
using System.Text.Json;
using GW2SDK.ItemStats;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public static class EquipmentItemReader
{
    public static EquipmentItem GetEquipmentItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        NullableMember<int> count = new("count");
        NullableMember<EquipmentSlot> slot = new("slot");
        OptionalMember<int> upgrades = new("upgrades");
        OptionalMember<int> infusions = new("infusions");
        NullableMember<int> skin = new("skin");
        OptionalMember<SelectedStat> stats = new("stats");
        OptionalMember<ItemBinding> binding = new("binding");
        OptionalMember<string> boundTo = new("bound_to");
        RequiredMember<EquipmentLocation> location = new("location");
        OptionalMember<int> tabs = new("tabs");
        OptionalMember<int?> dyes = new("dyes");

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
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (member.NameEquals(upgrades.Name))
            {
                upgrades.Value = member.Value;
            }
            else if (member.NameEquals(infusions.Name))
            {
                infusions.Value = member.Value;
            }
            else if (member.NameEquals(skin.Name))
            {
                skin.Value = member.Value;
            }
            else if (member.NameEquals(stats.Name))
            {
                stats.Value = member.Value;
            }
            else if (member.NameEquals(binding.Name))
            {
                binding.Value = member.Value;
            }
            else if (member.NameEquals(boundTo.Name))
            {
                boundTo.Value = member.Value;
            }
            else if (member.NameEquals(location.Name))
            {
                location.Value = member.Value;
            }
            else if (member.NameEquals(tabs.Name))
            {
                tabs.Value = member.Value;
            }
            else if (member.NameEquals(dyes.Name))
            {
                dyes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentItem
        {
            Id = id.GetValue(),
            Count = count.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            SkinId = skin.GetValue(),
            Stats = stats.Select(value => value.GetSelectedStat(missingMemberBehavior)),
            Binding = binding.GetValue(missingMemberBehavior),
            BoundTo = boundTo.GetValueOrEmpty(),
            Location = location.GetValue(missingMemberBehavior),
            Tabs = tabs.SelectMany(value => value.GetInt32()),
            Dyes = dyes.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
