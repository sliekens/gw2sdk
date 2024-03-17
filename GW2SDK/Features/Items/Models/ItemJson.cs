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
                return json.GetJadeTechModule(missingMemberBehavior);
            case "MiniPet":
                return json.GetMiniature(missingMemberBehavior);
            case "PowerCore":
                return json.GetPowerCore(missingMemberBehavior);
            case "Relic":
                return json.GetRelic(missingMemberBehavior);
            case "Tool":
                return json.GetTool(missingMemberBehavior);
            case "Trinket":
                return json.GetTrinket(missingMemberBehavior);
            case "Trophy" or "Key":
                // Key acts as a Trophy, and there is only one (Florid Bouquet), so treat it as a Trophy
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
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var (races, professions, bodyTypes) =
            restrictions.Map(value => value.GetRestrictions(missingMemberBehavior));
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
            Flags = flags.Map(values => values.GetItemFlags()),
            Races = races,
            Professions = professions,
            BodyTypes = bodyTypes,
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
