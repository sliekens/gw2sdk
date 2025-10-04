using System.Text.Json;

using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class EquipmentSkinJson
{
    public static EquipmentSkin GetEquipmentSkin(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Armor":
                    return json.GetArmorSkin();
                case "Back":
                    return json.GetBackpackSkin();
                case "Gathering":
                    return json.GetGatheringToolSkin();
                case "Weapon":
                    return json.GetWeaponSkin();
                default:
                    break;
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
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
            else if (rarity.Match(member))
            {
                rarity = member;
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
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetString()) ?? "";
        return new EquipmentSkin
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            Rarity = rarity.Map(static (in value) => value.GetEnum<Rarity>()),
            Flags = flags.Map(static (in values) => values.GetSkinFlags()),
            Races = restrictions.Map(static (in values) => values.GetRestrictions()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = string.IsNullOrEmpty(iconString) ? null : new Uri(iconString)
        };
    }
}
