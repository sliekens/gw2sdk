using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
public static class SkinJson
{
    public static Skin GetSkin(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Armor":
                return json.GetArmorSkin(missingMemberBehavior);
            case "Back":
                return json.GetBackpackSkin(missingMemberBehavior);
            case "Gathering":
                return json.GetGatheringToolSkin(missingMemberBehavior);
            case "Weapon":
                return json.GetWeaponSkin(missingMemberBehavior);
        }

        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<SkinFlag> flags = new("flags");
        RequiredMember<SkinRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skin
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            Icon = icon.GetValueOrNull()
        };
    }
}
