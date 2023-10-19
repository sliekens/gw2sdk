using System.Text.Json;
using GuildWars2.ItemStats;
using GuildWars2.Json;

namespace GuildWars2.Armory;

[PublicAPI]
public static class EquipmentItemJson
{
    public static EquipmentItem GetEquipmentItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        NullableMember count = new("count");
        NullableMember slot = new("slot");
        OptionalMember upgrades = new("upgrades");
        OptionalMember infusions = new("infusions");
        NullableMember skin = new("skin");
        OptionalMember stats = new("stats");
        OptionalMember binding = new("binding");
        OptionalMember boundTo = new("bound_to");
        RequiredMember location = new("location");
        OptionalMember tabs = new("tabs");
        OptionalMember dyes = new("dyes");

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
            Id = id.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32()),
            Slot = slot.Select(value => value.GetEnum<EquipmentSlot>(missingMemberBehavior)),
            Upgrades = upgrades.SelectMany(value => value.GetInt32()),
            Infusions = infusions.SelectMany(value => value.GetInt32()),
            SkinId = skin.Select(value => value.GetInt32()),
            Stats = stats.Select(value => value.GetSelectedStat(missingMemberBehavior)),
            Binding = binding.Select(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            BoundTo = boundTo.Select(value => value.GetString()) ?? "",
            Location = location.Select(value => value.GetEnum<EquipmentLocation>(missingMemberBehavior)),
            Tabs = tabs.SelectMany(value => value.GetInt32()),
            Dyes = dyes.SelectMany<int?>(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
