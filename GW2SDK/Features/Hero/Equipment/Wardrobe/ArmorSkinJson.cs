using System.Text.Json;
using GuildWars2.Items;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class ArmorSkinJson
{
    public static ArmorSkin GetArmorSkin(this JsonElement json)
    {
        if (json.TryGetProperty("details", out var discriminator))
        {
            if (discriminator.TryGetProperty("type", out var subtype))
            {
                switch (subtype.GetString())
                {
                    case "Boots":
                        return json.GetBootsSkin();
                    case "Coat":
                        return json.GetCoatSkin();
                    case "Gloves":
                        return json.GetGlovesSkin();
                    case "Helm":
                        return json.GetHelmSkin();
                    case "HelmAquatic":
                        return json.GetHelmAquaticSkin();
                    case "Leggings":
                        return json.GetLeggingsSkin();
                    case "Shoulders":
                        return json.GetShouldersSkin();
                }
            }
        }

        RequiredMember name = "name";
        OptionalMember description = "description";
        RequiredMember rarity = "rarity";
        RequiredMember flags = "flags";
        RequiredMember restrictions = "restrictions";
        RequiredMember id = "id";
        OptionalMember icon = "icon";
        RequiredMember weightClass = "weight_class";
        OptionalMember dyeSlots = "dye_slots";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Armor"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (weightClass.Match(detail))
                    {
                        weightClass = detail;
                    }
                    else if (dyeSlots.Match(detail))
                    {
                        dyeSlots = detail;
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedMember(detail.Name));
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ArmorSkin
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetString()) ?? "",
            Rarity = rarity.Map(static value => value.GetEnum<Rarity>()),
            Flags = flags.Map(static values => values.GetSkinFlags()),
            Races = restrictions.Map(static values => values.GetRestrictions()),
            IconHref = icon.Map(static value => value.GetString()),
            WeightClass = weightClass.Map(static value => value.GetEnum<WeightClass>()),
            DyeSlots = dyeSlots.Map(static value => value.GetDyeSlotInfo())
        };
    }
}
