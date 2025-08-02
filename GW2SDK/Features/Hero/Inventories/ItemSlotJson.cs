using System.Text.Json;

using GuildWars2.Hero.Equipment;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class ItemSlotJson
{
    public static ItemSlot? GetItemSlot(this in JsonElement json)
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (charges.Match(member))
            {
                charges = member;
            }
            else if (skin.Match(member))
            {
                skin = member;
            }
            else if (upgrades.Match(member))
            {
                upgrades = member;
            }
            else if (upgradeSlotIndices.Match(member))
            {
                upgradeSlotIndices = member;
            }
            else if (infusions.Match(member))
            {
                infusions = member;
            }
            else if (dyes.Match(member))
            {
                dyes = member;
            }
            else if (binding.Match(member))
            {
                binding = member;
            }
            else if (boundTo.Match(member))
            {
                boundTo = member;
            }
            else if (stats.Match(member))
            {
                stats = member;
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
            var indices = upgradeSlotIndices.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetInt32())
            )!;
            for (var i = 0; i < ids.Count; i++)
            {
                var upgradeId = ids[i];
                switch (indices[i])
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

        return new ItemSlot
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32()),
            Charges = charges.Map(static (in JsonElement value) => value.GetInt32()),
            SkinId = skin.Map(static (in JsonElement value) => value.GetInt32()),
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId,
            InfusionItemIds =
                infusions.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32()))
                ?? new Collections.ValueList<int>(),
            DyeColorIds =
                dyes.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())) ?? new Collections.ValueList<int>(),
            Binding = binding.Map(static (in JsonElement value) => value.GetEnum<ItemBinding>()),
            BoundTo = boundTo.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Stats = stats.Map(static (in JsonElement value) => value.GetSelectedAttributeCombination())
        };
    }
}
