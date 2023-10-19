using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class FoodJson
{
    public static Food GetFood(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
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
        NullableMember duration = new("duration_ms");
        NullableMember applyCount = new("apply_count");
        OptionalMember effectName = new("name");
        OptionalMember effectIcon = new("icon");
        OptionalMember effectDescription = new("description");
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
                        if (!detail.Value.ValueEquals("Food"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(duration.Name))
                    {
                        duration.Value = detail.Value;
                    }
                    else if (detail.NameEquals(applyCount.Name))
                    {
                        applyCount.Value = detail.Value;
                    }
                    else if (detail.NameEquals(effectName.Name))
                    {
                        effectName.Value = detail.Value;
                    }
                    else if (detail.NameEquals(effectIcon.Name))
                    {
                        effectIcon.Value = detail.Value;
                    }
                    else if (detail.NameEquals(effectDescription.Name))
                    {
                        effectDescription.Value = detail.Value;
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

        return new Food
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
            Duration = duration.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            ApplyCount = applyCount.Select(value => value.GetInt32()),
            EffectName = effectName.Select(value => value.GetString()) ?? "",
            EffectIcon = effectIcon.Select(value => value.GetString()),
            EffectDescription = effectDescription.Select(value => value.GetString()) ?? ""
        };
    }
}
