using System.Text.Json;
using GuildWars2.Hero.Equipment.Dyes;
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (upgrades.Match(member))
            {
                upgrades = member;
            }
            else if (infusions.Match(member))
            {
                infusions = member;
            }
            else if (skin.Match(member))
            {
                skin = member;
            }
            else if (stats.Match(member))
            {
                stats = member;
            }
            else if (binding.Match(member))
            {
                binding = member;
            }
            else if (boundTo.Match(member))
            {
                boundTo = member;
            }
            else if (location.Match(member))
            {
                location = member;
            }
            else if (tabs.Match(member))
            {
                tabs = member;
            }
            else if (dyes.Match(member))
            {
                dyes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        int? suffixItemId = null, secondarySuffixItemId = null;
        if (upgrades.Map(values => values.GetList(value => value.GetInt32())) is { } ids)
        {
            for (var i = 0; i < ids.Count; i++)
            {
                var upgradeId = ids[i];
                switch (i)
                {
                    case 0:
                        suffixItemId = upgradeId;
                        break;
                    case 1:
                        secondarySuffixItemId = upgradeId;
                        break;
                    default:
                        throw new InvalidOperationException(
                            Strings.UnexpectedArrayLength(ids.Count)
                        );
                }
            }
        }

        return new EquipmentItem
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<EquipmentSlot>()),
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId,
            InfusionItemIds =
                infusions.Map(values => values.GetList(value => value.GetInt32()))
                ?? Empty.ListOfInt32,
            SkinId = skin.Map(value => value.GetInt32()),
            Stats =
                stats.Map(value => value.GetSelectedAttributeCombination(missingMemberBehavior)),
            Binding = binding.Map(value => value.GetEnum<ItemBinding>()),
            BoundTo = boundTo.Map(value => value.GetString()) ?? "",
            Location = location.Map(
                value => value.GetEnum<EquipmentLocation>()
            ),
            TemplateNumbers =
                tabs.Map(values => values.GetList(value => value.GetInt32())) ?? Empty.ListOfInt32,
            DyeColorIds =
                dyes.Map(
                    values => values.GetList(
                        value => value.GetNullableInt32() ?? DyeColor.DyeRemoverId
                    )
                )
                ?? Empty.ListOfInt32
        };
    }
}
