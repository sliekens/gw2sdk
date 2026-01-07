using System.Text.Json;

using GuildWars2.Hero.Equipment.Dyes;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class EquipmentItemJson
{
    public static EquipmentItem GetEquipmentItem(this in JsonElement json)
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

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        int? suffixItemId = null, secondarySuffixItemId = null;
        if (upgrades.Map(static (in values) => values.GetList(static (in value) => value.GetInt32())) is
            {
            } ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                int upgradeId = ids[i];
                switch (i)
                {
                    case 0:
                        suffixItemId = upgradeId;
                        break;
                    case 1:
                        secondarySuffixItemId = upgradeId;
                        break;
                    default:
                        ThrowHelper.ThrowUnexpectedArrayLength(ids.Count);
                        break;
                }
            }
        }

        return new EquipmentItem
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Count = count.Map(static (in value) => value.GetInt32()),
            Slot = slot.Map(static (in value) => value.GetEquipmentSlot()),
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId,
            InfusionItemIds =
                infusions.Map(static (in values) => values.GetList(static (in value) => value.GetInt32()))
                ?? [],
            SkinId = skin.Map(static (in value) => value.GetInt32()),
            Stats = stats.Map(static (in value) => value.GetSelectedAttributeCombination()),
            Binding = binding.Map(static (in value) => value.GetEnum<ItemBinding>()),
            BoundTo = boundTo.Map(static (in value) => value.GetString()) ?? "",
            Location = location.Map(static (in value) => value.GetEnum<EquipmentLocation>()),
            TemplateNumbers =
                tabs.Map(static (in values) => values.GetList(static (in value) => value.GetInt32())) ?? [],
            DyeColorIds = dyes.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetNullableInt32() ?? DyeColor.DyeRemoverId
                    )
                )
                ?? []
        };
    }
}
