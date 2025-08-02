using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UnlockerJson
{
    public static Unlocker GetUnlocker(this in JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator) && discriminator.TryGetProperty("unlock_type", out var subtype))
        {
            switch (subtype.GetString())
            {
                case "BagSlot":
                    return json.GetBagSlotExpansion();
                case "BankTab":
                    return json.GetBankTabExpansion();
                case "BuildLibrarySlot":
                    return json.GetBuildStorageExpansion();
                case "BuildLoadoutTab":
                    return json.GetBuildTemplateExpansion();
                case "Champion":
                    return json.GetMistChampionSkinUnlocker();
                case "CollectibleCapacity":
                    return json.GetStorageExpander();
                case "Content":
                    return json.GetContentUnlocker();
                case "CraftingRecipe":
                    return json.GetRecipeSheet();
                case "Dye":
                    return json.GetDye();
                case "GearLoadoutTab":
                    return json.GetEquipmentTemplateExpansion();
                case "GliderSkin":
                    return json.GetGliderSkinUnlocker();
                case "JadeBotSkin":
                    return json.GetJadeBotSkinUnlocker();
                case "Minipet":
                    return json.GetMiniatureUnlocker();
                case "Ms":
                    return json.GetMountSkinUnlocker();
                case "Outfit":
                    return json.GetOutfitUnlocker();
                case "SharedSlot":
                    return json.GetSharedInventorySlot();
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember level = "level";
        RequiredMember rarity = "rarity";
        RequiredMember vendorValue = "vendor_value";
        RequiredMember gameTypes = "game_types";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        OptionalMember icon = "icon";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Consumable"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (rarity.Match(member))
            {
                rarity = member;
            }
            else if (vendorValue.Match(member))
            {
                vendorValue = member;
            }
            else if (gameTypes.Match(member))
            {
                gameTypes = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (restrictions.Match(member))
            {
                restrictions = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Unlock"))
                        {
                            ThrowHelper.ThrowInvalidDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (detail.NameEquals("unlock_type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            ThrowHelper.ThrowUnexpectedDiscriminator(detail.Value.GetString());
                        }
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(detail.Name);
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetString());
        return new Unlocker
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Level = level.Map(static (in JsonElement value) => value.GetInt32()),
            Rarity = rarity.Map(static (in JsonElement value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in JsonElement value) => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static (in JsonElement values) => values.GetItemFlags()),
            Restrictions = restrictions.Map(static (in JsonElement value) => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }
}
