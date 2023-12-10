using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class CoatSkinJson
{
    public static CoatSkin GetCoatSkin(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Armor"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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
            else if (member.Name == "details")
            {
                foreach (var detail in member.Value.EnumerateObject())
                {
                    if (detail.Name == "type")
                    {
                        if (!detail.Value.ValueEquals("Coat"))
                        {
                            throw new InvalidOperationException(
                                Strings.InvalidDiscriminator(detail.Value.GetString())
                            );
                        }
                    }
                    else if (detail.Name == weightClass.Name)
                    {
                        weightClass = detail;
                    }
                    else if (detail.Name == dyeSlots.Name)
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

        return new CoatSkin
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetString()) ?? "",
            Rarity = rarity.Map(value => value.GetEnum<Rarity>(missingMemberBehavior)),
            Flags = flags.Map(values => values.GetSkinFlags()),
            Restrictions =
                restrictions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<SkinRestriction>(missingMemberBehavior)
                        )
                ),
            IconHref = icon.Map(value => value.GetString()),
            WeightClass =
                weightClass.Map(value => value.GetEnum<WeightClass>(missingMemberBehavior)),
            DyeSlots = dyeSlots.Map(value => value.GetDyeSlotInfo(missingMemberBehavior))
        };
    }
}
