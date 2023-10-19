using System.Text.Json;
using GuildWars2.Json;

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

        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember rarity = new("rarity");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<SkinFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<SkinRestriction>(missingMemberBehavior)),
            Icon = icon.Select(value => value.GetString())
        };
    }
}
