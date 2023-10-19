using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ItemJson
{
    public static Item GetItem(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Armor":
                return json.GetArmor(missingMemberBehavior);
            case "Back":
                return json.GetBackpack(missingMemberBehavior);
            case "Bag":
                return json.GetBag(missingMemberBehavior);
            case "Consumable":
                return json.GetConsumable(missingMemberBehavior);
            case "Container":
                return json.GetContainer(missingMemberBehavior);
            case "CraftingMaterial":
                return json.GetCraftingMaterial(missingMemberBehavior);
            case "Gathering":
                return json.GetGatheringTool(missingMemberBehavior);
            case "Gizmo":
                return json.GetGizmo(missingMemberBehavior);
            case "JadeTechModule":
                return json.GetJadeBotUpgrade(missingMemberBehavior);
            case "Key":
                return json.GetKey(missingMemberBehavior);
            case "MiniPet":
                return json.GetMinipet(missingMemberBehavior);
            case "PowerCore":
                return json.GetPowerCore(missingMemberBehavior);
            case "Relic":
                return json.GetRelic(missingMemberBehavior);
            case "Tool":
                return json.GetTool(missingMemberBehavior);
            case "Trinket":
                return json.GetTrinket(missingMemberBehavior);
            case "Trophy":
                return json.GetTrophy(missingMemberBehavior);
            case "UpgradeComponent":
                return json.GetUpgradeComponent(missingMemberBehavior);
            case "Weapon":
                return json.GetWeapon(missingMemberBehavior);
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
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Item
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
