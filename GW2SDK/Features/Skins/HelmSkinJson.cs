﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skins;

[PublicAPI]
public static class HelmSkinJson
{
    public static HelmSkin GetHelmSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        OptionalMember description = new("description");
        RequiredMember rarity = new("rarity");
        RequiredMember flags = new("flags");
        RequiredMember restrictions = new("restrictions");
        RequiredMember id = new("id");
        OptionalMember icon = new("icon");
        RequiredMember weightClass = new("weight_class");
        OptionalMember dyeSlots = new("dye_slots");
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
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(rarity.Name))
            {
                rarity = member;
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
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals("details"))
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.NameEquals("type"))
                    {
                        if (!detail.Value.ValueEquals("Helm"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.NameEquals(weightClass.Name))
                    {
                        weightClass = detail;
                    }
                    else if (detail.NameEquals(dyeSlots.Name))
                    {
                        dyeSlots = detail;
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

        return new HelmSkin
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetString()) ?? "",
            Rarity = rarity.Select(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<SkinFlag>(missingMemberBehavior)),
            Restrictions = restrictions.SelectMany(value => value.GetEnum<SkinRestriction>(missingMemberBehavior)),
            Icon = icon.Select(value => value.GetString()),
            WeightClass = weightClass.Select(value => value.GetEnum<WeightClass>(missingMemberBehavior)),
            DyeSlots = dyeSlots.Select(value => value.GetDyeSlotInfo(missingMemberBehavior))
        };
    }
}
