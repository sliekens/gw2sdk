using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class ItemJson
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
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Item
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
            Flags =
                flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<ItemFlag>(missingMemberBehavior)
                    )
                ),
            Restrictions =
                restrictions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ItemRestriction>(missingMemberBehavior)
                        )
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetString())
        };
    }
}
