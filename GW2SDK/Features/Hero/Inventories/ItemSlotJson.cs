using System.Text.Json;
using GuildWars2.Hero.Equipment;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class ItemSlotJson
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (member.Name == charges.Name)
            {
                charges = member;
            }
            else if (member.Name == skin.Name)
            {
                skin = member;
            }
            else if (member.Name == upgrades.Name)
            {
                upgrades = member;
            }
            else if (member.Name == upgradeSlotIndices.Name)
            {
                upgradeSlotIndices = member;
            }
            else if (member.Name == infusions.Name)
            {
                infusions = member;
            }
            else if (member.Name == dyes.Name)
            {
                dyes = member;
            }
            else if (member.Name == binding.Name)
            {
                binding = member;
            }
            else if (member.Name == boundTo.Name)
            {
                boundTo = member;
            }
            else if (member.Name == stats.Name)
            {
                stats = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        int? suffixItemId = null, secondarySuffixItemId = null;
        if (upgrades.Map(values => values.GetList(value => value.GetInt32())) is { } ids)
        {
            var indices =
                upgradeSlotIndices.Map(values => values.GetList(value => value.GetInt32()))!;
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
                        throw new InvalidOperationException(Strings.UnexpectedArrayLength(ids.Count));
                }
            }
        }

        return new ItemSlot
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32()),
            Charges = charges.Map(value => value.GetInt32()),
            SkinId = skin.Map(value => value.GetInt32()),
            SuffixItemId = suffixItemId,
            SecondarySuffixItemId = secondarySuffixItemId,
            InfusionItemIds = infusions.Map(values => values.GetList(value => value.GetInt32())),
            DyeIds = dyes.Map(values => values.GetList(value => value.GetNullableInt32())),
            Binding = binding.Map(value => value.GetEnum<ItemBinding>(missingMemberBehavior)),
            BoundTo = boundTo.Map(value => value.GetString()) ?? "",
            Stats = stats.Map(value => value.GetSelectedStat(missingMemberBehavior))
        };
    }
}
