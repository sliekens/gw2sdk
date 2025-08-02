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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        int? suffixItemId = null, secondarySuffixItemId = null;
        if (upgrades.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())) is
            {
            } ids)
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
                        ThrowHelper.ThrowUnexpectedArrayLength(ids.Count);
                        break;
                }
            }
        }

        return new EquipmentItem
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<EquipmentSlot>()),
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId,
            InfusionItemIds =
                infusions.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
                ?? new Collections.ValueList<int>(),
            SkinId = skin.Map(static (in JsonElement value) => value.GetInt32()),
            Stats = stats.Map(static (in JsonElement value) => value.GetSelectedAttributeCombination()),
            Binding = binding.Map(static (in JsonElement value) => value.GetEnum<ItemBinding>()),
            BoundTo = boundTo.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Location = location.Map(static (in JsonElement value) => value.GetEnum<EquipmentLocation>()),
            TemplateNumbers =
                tabs.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())) ?? new Collections.ValueList<int>(),
            DyeColorIds = dyes.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetNullableInt32() ?? DyeColor.DyeRemoverId
                    )
                )
                ?? new Collections.ValueList<int>()
        };
    }
}
