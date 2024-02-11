using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class EquipmentItemJson
{
    public static EquipmentItem GetEquipmentItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        NullableMember count = "count";
        NullableMember slot = "slot";
        OptionalMember upgrades = "upgrades";
        OptionalMember infusions = "infusions";
        NullableMember skin = "skin";
        OptionalMember stats = "stats";
        OptionalMember binding = "binding";
        OptionalMember boundTo = "bound_to";
        RequiredMember location = "location";
        OptionalMember tabs = "tabs";
        OptionalMember dyes = "dyes";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (member.Name == upgrades.Name)
            {
                upgrades = member;
            }
            else if (member.Name == infusions.Name)
            {
                infusions = member;
            }
            else if (member.Name == skin.Name)
            {
                skin = member;
            }
            else if (member.Name == stats.Name)
            {
                stats = member;
            }
            else if (member.Name == binding.Name)
            {
                binding = member;
            }
            else if (member.Name == boundTo.Name)
            {
                boundTo = member;
            }
            else if (member.Name == location.Name)
            {
                location = member;
            }
            else if (member.Name == tabs.Name)
            {
                tabs = member;
            }
            else if (member.Name == dyes.Name)
            {
                dyes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EquipmentItem
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<EquipmentSlot>(missingMemberBehavior)),
            UpgradeItemIds = upgrades.Map(values => values.GetList(value => value.GetInt32())),
            InfusionItemIds = infusions.Map(values => values.GetList(value => value.GetInt32())),
            SkinId = skin.Map(value => value.GetInt32()),
            Stats = stats.Map(value => value.GetSelectedStat(missingMemberBehavior)),
            Binding = binding.Map(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            BoundTo = boundTo.Map(value => value.GetString()) ?? "",
            Location = location.Map(
                value => value.GetEnum<EquipmentLocation>(missingMemberBehavior)
            ),
            TemplateNumbers = tabs.Map(values => values.GetList(value => value.GetInt32())),
            DyeColorIds = dyes.Map(values => values.GetList(value => value.GetNullableInt32()))
        };
    }
}
