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
            if (member.Name == @default.Name)
            {
                @default = member;
            }
            else if (member.Name == "overrides")
            {
                foreach (var @override in member.Value.EnumerateObject())
                {
                    if (@override.Name == asuraFemale.Name)
                    {
                        asuraFemale = @override;
                    }
                    else if (@override.Name == asuraMale.Name)
                    {
                        asuraMale = @override;
                    }
                    else if (@override.Name == charrFemale.Name)
                    {
                        charrFemale = @override;
                    }
                    else if (@override.Name == charrMale.Name)
                    {
                        charrMale = @override;
                    }
                    else if (@override.Name == humanFemale.Name)
                    {
                        humanFemale = @override;
                    }
                    else if (@override.Name == humanMale.Name)
                    {
                        humanMale = @override;
                    }
                    else if (@override.Name == nornFemale.Name)
                    {
                        nornFemale = @override;
                    }
                    else if (@override.Name == nornMale.Name)
                    {
                        nornMale = @override;
                    }
                    else if (@override.Name == sylvariFemale.Name)
                    {
                        sylvariFemale = @override;
                    }
                    else if (@override.Name == sylvariMale.Name)
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
