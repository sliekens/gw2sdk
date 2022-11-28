using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
public static class ArmorSkinJson
{
    public static ArmorSkin GetArmorSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("details").GetProperty("type").GetString())
        {
            case "Boots":
                return json.GetBootsSkin(missingMemberBehavior);
            case "Coat":
                return json.GetCoatSkin(missingMemberBehavior);
            case "Gloves":
                return json.GetGlovesSkin(missingMemberBehavior);
            case "Helm":
                return json.GetHelmSkin(missingMemberBehavior);
            case "HelmAquatic":
                return json.GetHelmAquaticSkin(missingMemberBehavior);
            case "Leggings":
                return json.GetLeggingsSkin(missingMemberBehavior);
            case "Shoulders":
                return json.GetShouldersSkin(missingMemberBehavior);
        }

        RequiredMember<string> name = new("name");
        OptionalMember<string> description = new("description");
        RequiredMember<Rarity> rarity = new("rarity");
        RequiredMember<SkinFlag> flags = new("flags");
        RequiredMember<SkinRestriction> restrictions = new("restrictions");
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<WeightClass> weightClass = new("weight_class");
        OptionalMember<DyeSlotInfo> dyeSlots = new("dye_slots");
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
                        if (missingMemberBehavior == MissingMemberBehavior.Error)
                        {
                            throw new InvalidOperationException(
                                Strings.UnexpectedDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(weightClass.Name))
                    {
                        weightClass.Value = detail.Value;
                    }
                    else if (detail.NameEquals(dyeSlots.Name))
                    {
                        dyeSlots.Value = detail.Value;
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

        return new ArmorSkin
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValueOrEmpty(),
            Rarity = rarity.GetValue(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Restrictions = restrictions.GetValues(missingMemberBehavior),
            Icon = icon.GetValueOrNull(),
            WeightClass = weightClass.GetValue(missingMemberBehavior),
            DyeSlots = dyeSlots.Select(value => value.GetDyeSlotInfo(missingMemberBehavior))
        };
    }
}
