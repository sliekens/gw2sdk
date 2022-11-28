using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
public static class ShieldSkinJson
{
    public static ShieldSkin GetShieldSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<SkinFlag> flags = new("flags");
        RequiredMember<SkinRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<DamageType> damageType = new("damage_type");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Weapon"))
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
            else if (member.NameEquals(rarity.Name))
            {
                rarity.Value = member.Value;
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
                        if (!detail.Value.ValueEquals("Shield"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(damageType.Name))
                    {
                        damageType.Value = detail.Value;
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

        return new ShieldSkin
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            Icon = icon.GetValueOrNull(),
            DamageType = damageType.GetValue(missingMemberBehavior)
        };
    }
}
