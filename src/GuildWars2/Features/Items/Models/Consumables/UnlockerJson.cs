using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UnlockerJson
{
    public static Unlocker GetUnlocker(this in JsonElement json)
    {
        if (json.TryGetProperty("details", out JsonElement discriminator) && discriminator.TryGetProperty("unlock_type", out JsonElement subtype))
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
                case "MagicDoorSkin":
                    return json.GetMagicDoorSkinUnlocker();
                case "Minipet":
                    return json.GetMiniatureUnlocker();
                case "Ms":
                    return json.GetMountSkinUnlocker();
                case "Outfit":
                    return json.GetOutfitUnlocker();
                case "SharedSlot":
                    return json.GetSharedInventorySlot();
                case "WardrobeTemplateTab":
                    return json.GetFashionTemplateExpansion();
                default:
                    break;
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
        foreach (JsonProperty member in json.EnumerateObject())
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
                foreach (JsonProperty detail in member.Value.EnumerateObject())
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

        string? iconString = icon.Map(static (in value) => value.GetString());
        return new Unlocker
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            Level = level.Map(static (in value) => value.GetInt32()),
            Rarity = rarity.Map(static (in value) => value.GetEnum<Rarity>()),
            VendorValue = vendorValue.Map(static (in value) => value.GetInt32()),
            GameTypes =
                gameTypes.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetEnum<GameType>())
                ),
            Flags = flags.Map(static (in values) => values.GetItemFlags()),
            Restrictions = restrictions.Map(static (in value) => value.GetItemRestriction()),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired()),
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }
}
