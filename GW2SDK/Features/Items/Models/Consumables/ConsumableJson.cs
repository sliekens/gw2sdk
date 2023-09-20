using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ConsumableJson
{
    public static Consumable GetConsumable(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
        {
            case "AppearanceChange":
                return json.GetAppearanceChanger(missingMemberBehavior);
            case "Booze":
                return json.GetBooze(missingMemberBehavior);
            case "ContractNpc":
                return json.GetContractNpc(missingMemberBehavior);
            case "Currency":
                return json.GetCurrency(missingMemberBehavior);
            case "Food":
                return json.GetFood(missingMemberBehavior);
            case "Generic":
                return json.GetGenericConsumable(missingMemberBehavior);
            case "Halloween":
                return json.GetHalloweenConsumable(missingMemberBehavior);
            case "Immediate":
                return json.GetImmediateConsumable(missingMemberBehavior);
            case "MountRandomUnlock":
                return json.GetMountRandomUnlocker(missingMemberBehavior);
            case "RandomUnlock":
                return json.GetRandomUnlocker(missingMemberBehavior);
            case "TeleportToFriend":
                return json.GetTeleportToFriend(missingMemberBehavior);
            case "Transmutation":
                return json.GetTransmutation(missingMemberBehavior);
            case "Unlock":
                return json.GetUnlocker(missingMemberBehavior);
            case "UpgradeRemoval":
                return json.GetUpgradeRemover(missingMemberBehavior);
            case "Utility":
                return json.GetUtility(missingMemberBehavior);
        }

        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<int> level = new("level");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<Coin> vendorValue = new("vendor_value");
        RequiredMember<GameType> gameTypes = new("game_types");
        RequiredMember<ItemFlag> flags = new("flags");
        RequiredMember<ItemRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<string> icon = new("icon");
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

        return new Consumable
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Level = level.GetValue(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            VendorValue = vendorValue.GetValue(),
            GameTypes = gameTypes.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            ChatLink = chatLink.GetValue(),
            Icon = icon.GetValueOrNull()
        };
    }
}
