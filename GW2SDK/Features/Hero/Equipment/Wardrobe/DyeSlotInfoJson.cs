using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class DyeSlotInfoJson
{
    public static DyeSlotInfo GetDyeSlotInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember @default = "default";
        OptionalMember asuraFemale = "AsuraFemale";
        OptionalMember asuraMale = "AsuraMale";
        OptionalMember charrFemale = "CharrFemale";
        OptionalMember charrMale = "CharrMale";
        OptionalMember humanFemale = "HumanFemale";
        OptionalMember humanMale = "HumanMale";
        OptionalMember nornFemale = "NornFemale";
        OptionalMember nornMale = "NornMale";
        OptionalMember sylvariFemale = "SylvariFemale";
        OptionalMember sylvariMale = "SylvariMale";
        foreach (var member in json.EnumerateObject())
        {
            if (@default.Match(member))
            {
                @default = member;
            }
            else if (member.NameEquals("overrides"))
            {
                foreach (var @override in member.Value.EnumerateObject())
                {
                    if (asuraFemale.Match(@override))
                    {
                        asuraFemale = @override;
                    }
                    else if (asuraMale.Match(@override))
                    {
                        asuraMale = @override;
                    }
                    else if (charrFemale.Match(@override))
                    {
                        charrFemale = @override;
                    }
                    else if (charrMale.Match(@override))
                    {
                        charrMale = @override;
                    }
                    else if (humanFemale.Match(@override))
                    {
                        humanFemale = @override;
                    }
                    else if (humanMale.Match(@override))
                    {
                        humanMale = @override;
                    }
                    else if (nornFemale.Match(@override))
                    {
                        nornFemale = @override;
                    }
                    else if (nornMale.Match(@override))
                    {
                        nornMale = @override;
                    }
                    else if (sylvariFemale.Match(@override))
                    {
                        sylvariFemale = @override;
                    }
                    else if (sylvariMale.Match(@override))
                    {
                        sylvariMale = @override;
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(
                            Strings.UnexpectedMember(@override.Name)
                        );
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DyeSlotInfo
        {
            Default = @default.Map(GetDyeSlots),
            AsuraFemale = asuraFemale.Map(GetDyeSlots),
            AsuraMale = asuraMale.Map(GetDyeSlots),
            CharrFemale = charrFemale.Map(GetDyeSlots),
            CharrMale = charrMale.Map(GetDyeSlots),
            HumanFemale = humanFemale.Map(GetDyeSlots),
            HumanMale = humanMale.Map(GetDyeSlots),
            NornFemale = nornFemale.Map(GetDyeSlots),
            NornMale = nornMale.Map(GetDyeSlots),
            SylvariFemale = sylvariFemale.Map(GetDyeSlots),
            SylvariMale = sylvariMale.Map(GetDyeSlots)
        };

        List<DyeSlot?> GetDyeSlots(JsonElement values)
        {
            // The dye slot arrays can contain Null to represent the default color, so this is ugly
            // Perhaps there is a better way to model it with a Null Object pattern?
            return values.GetList(
                value => value.ValueKind == JsonValueKind.Null
                    ? null
                    : value.GetDyeSlot(missingMemberBehavior)
            );
        }
    }
}
