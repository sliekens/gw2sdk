using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class BagJson
{
    public static Bag GetBag(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
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
        RequiredMember noSellOrSort = "no_sell_or_sort";
        RequiredMember size = "size";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Bag"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity = member;
            }
            else if (member.NameEquals(vendorValue.Name))
            {
                vendorValue = member;
            }
            else if (member.NameEquals(gameTypes.Name))
            {
                gameTypes = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(restrictions.Name))
            {
                restrictions = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals(noSellOrSort.Name))
                    {
                        noSellOrSort = detail;
                    }
                    else if (detail.NameEquals(size.Name))
                    {
                        size = detail;
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

        return new Bag
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
            Icon = icon.Select(value => value.GetString()),
            NoSellOrSort = noSellOrSort.Select(value => value.GetBoolean()),
            Size = size.Select(value => value.GetInt32())
        };
    }
}
