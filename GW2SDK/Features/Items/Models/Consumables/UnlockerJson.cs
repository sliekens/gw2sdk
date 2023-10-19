using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class UnlockerJson
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

        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember level = new("level");
        RequiredMember rarity = new("rarity");
        RequiredMember vendorValue = new("vendor_value");
        RequiredMember gameTypes = new("game_types");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        OptionalMember icon = new("icon");
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
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity.Value = member.Value;
            }
            else if (member.NameEquals(vendorValue.Name))
            {
                vendorValue.Value = member.Value;
            }
            else if (member.NameEquals(gameTypes.Name))
            {
                gameTypes.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(restrictions.Name))
            {
                restrictions.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
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

        return new Unlocker
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Level = level.Select(value => value.GetInt32()),
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            VendorValue = vendorValue.Select(value => value.GetInt32()),
            GameTypes = gameTypes.SelectMany(value => value.GetEnum<GameType>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<ItemFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<ItemRestriction>(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetString())
        };
    }
}
