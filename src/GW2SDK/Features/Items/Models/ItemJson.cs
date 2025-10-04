using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class ItemJson
{
    public static Item GetItem(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Armor":
                    return json.GetArmor();
                case "Back":
                    return json.GetBackpack();
                case "Bag":
                    return json.GetBag();
                case "Consumable":
                    return json.GetConsumable();
                case "Container":
                    return json.GetContainer();
                case "CraftingMaterial":
                    return json.GetCraftingMaterial();
                case "Gathering":
                    return json.GetGatheringTool();
                case "Gizmo":
                    return json.GetGizmo();
                case "JadeTechModule":
                    return json.GetJadeTechModule();
                case "MiniPet":
                    return json.GetMiniature();
                case "PowerCore":
                    return json.GetPowerCore();
                case "Relic":
                    return json.GetRelic();
                case "Tool":
                    return json.GetSalvageTool();
                case "Trinket":
                    return json.GetTrinket();
                case "Trophy" or "Key":
                    // Key acts as a Trophy, and there is only one (Florid Bouquet), so treat it as a Trophy
                    return json.GetTrophy();
                case "UpgradeComponent":
                    return json.GetUpgradeComponent();
                case "Weapon":
                    return json.GetWeapon();
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
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string? iconString = icon.Map(static (in value) => value.GetString());
        return new Item
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
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString) : null
        };
    }
}
