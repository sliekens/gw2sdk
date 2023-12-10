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
        switch (json.GetProperty("details").GetProperty("unlock_type").GetString())
        {
            case "BagSlot":
                return json.GetBagSlotUnlocker(missingMemberBehavior);
            case "BankTab":
                return json.GetBankTabUnlocker(missingMemberBehavior);
            case "BuildLibrarySlot":
                return json.GetBuildLibrarySlotUnlocker(missingMemberBehavior);
            case "BuildLoadoutTab":
                return json.GetBuildLoadoutTabUnlocker(missingMemberBehavior);
            case "Champion":
                return json.GetChampionUnlocker(missingMemberBehavior);
            case "CollectibleCapacity":
                return json.GetCollectibleCapacityUnlocker(missingMemberBehavior);
            case "Content":
                return json.GetContentUnlocker(missingMemberBehavior);
            case "CraftingRecipe":
                return json.GetCraftingRecipeUnlocker(missingMemberBehavior);
            case "Dye":
                return json.GetDyeUnlocker(missingMemberBehavior);
            case "GearLoadoutTab":
                return json.GetGearLoadoutTabUnlocker(missingMemberBehavior);
            case "GliderSkin":
                return json.GetGliderSkinUnlocker(missingMemberBehavior);
            case "JadeBotSkin":
                return json.GetJadeBotSkinUnlocker(missingMemberBehavior);
            case "Minipet":
                return json.GetMinipetUnlocker(missingMemberBehavior);
            case "Ms":
                return json.GetMsUnlocker(missingMemberBehavior);
            case "Outfit":
                return json.GetOutfitUnlocker(missingMemberBehavior);
            case "SharedSlot":
                return json.GetSharedSlotUnlocker(missingMemberBehavior);
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Consumable"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == rarity.Name)
            {
                rarity = member;
            }
            else if (member.Name == vendorValue.Name)
            {
                vendorValue = member;
            }
            else if (member.Name == gameTypes.Name)
            {
                gameTypes = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == restrictions.Name)
            {
                restrictions = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == chatLink.Name)
            {
                chatLink = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == "details")
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.Name == "type")
                    {
                        if (!detail.Value.ValueEquals("Unlock"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.Name == "unlock_type")
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
            Restrictions =
                restrictions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ItemRestriction>(missingMemberBehavior)
                        )
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
