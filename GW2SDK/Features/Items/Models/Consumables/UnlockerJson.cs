using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UnlockerJson
{
    public static Unlocker GetUnlocker(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("unlock_type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "BagSlot":
                        return json.GetBagSlotExpansion(missingMemberBehavior);
                    case "BankTab":
                        return json.GetBankTabExpansion(missingMemberBehavior);
                    case "BuildLibrarySlot":
                        return json.GetBuildStorageExpansion(missingMemberBehavior);
                    case "BuildLoadoutTab":
                        return json.GetBuildTemplateExpansion(missingMemberBehavior);
                    case "Champion":
                        return json.GetMistChampionSkinUnlocker(missingMemberBehavior);
                    case "CollectibleCapacity":
                        return json.GetStorageExpander(missingMemberBehavior);
                    case "Content":
                        return json.GetContentUnlocker(missingMemberBehavior);
                    case "CraftingRecipe":
                        return json.GetRecipeSheet(missingMemberBehavior);
                    case "Dye":
                        return json.GetDye(missingMemberBehavior);
                    case "GearLoadoutTab":
                        return json.GetEquipmentTemplateExpansion(missingMemberBehavior);
                    case "GliderSkin":
                        return json.GetGliderSkinUnlocker(missingMemberBehavior);
                    case "JadeBotSkin":
                        return json.GetJadeBotSkinUnlocker(missingMemberBehavior);
                    case "Minipet":
                        return json.GetMiniatureUnlocker(missingMemberBehavior);
                    case "Ms":
                        return json.GetMountSkinUnlocker(missingMemberBehavior);
                    case "Outfit":
                        return json.GetOutfitUnlocker(missingMemberBehavior);
                    case "SharedSlot":
                        return json.GetSharedInventorySlot(missingMemberBehavior);
                }
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
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals("unlock_type"))
                    {
                        if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(detail.Name));
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var (races, professions, bodyTypes) =
            restrictions.Map(value => value.GetRestrictions(missingMemberBehavior));
        return new Unlocker
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Level = level.Map(value => value.GetInt32()),
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Map(value => value.GetInt32()),
            GameTypes =
                gameTypes.Map(
                    values => values.GetList(
                        value => value.GetEnum<GameType>(missingMemberBehavior)
                    )
                ),
            Flags = flags.Map(values => values.GetItemFlags()),
            Races = races,
            Professions = professions,
            BodyTypes = bodyTypes,
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
