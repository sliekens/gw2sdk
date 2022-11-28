using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public static class DyeUnlockerJson
{
    public static DyeUnlocker GetDyeUnlocker(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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
        RequiredMember<int> colorId = new("color_id");
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
                        if (!detail.Value.ValueEquals("Dye"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(colorId.Name))
                    {
                        colorId.Value = detail.Value;
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

        return new DyeUnlocker
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
            Icon = icon.GetValueOrNull(),
            ColorId = colorId.GetValue()
        };
    }
}
