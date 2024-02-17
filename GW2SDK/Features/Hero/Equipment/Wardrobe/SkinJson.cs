using System.Text.Json;
using GuildWars2.Hero.Races;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class SkinJson
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

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
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
            else if (member.Name == rarity.Name)
            {
                rarity = member;
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
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Skin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.Map(values => values.GetSkinFlags()),
            Restrictions =
                restrictions.Map(
                    values => values.GetList(
                        value => value.GetEnum<RaceName>(missingMemberBehavior)
                    )
                ),
            IconHref = icon.Map(value => value.GetString())
        };
    }
}
