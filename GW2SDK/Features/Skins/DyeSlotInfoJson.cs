using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skins;

[PublicAPI]
public static class DyeSlotInfoJson
{
    public static DyeSlotInfo GetDyeSlotInfo(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<DyeSlot?> @default = new("default");
        OptionalMember<DyeSlot?> asuraFemale = new("AsuraFemale");
        OptionalMember<DyeSlot?> asuraMale = new("AsuraMale");
        OptionalMember<DyeSlot?> charrFemale = new("CharrFemale");
        OptionalMember<DyeSlot?> charrMale = new("CharrMale");
        OptionalMember<DyeSlot?> humanFemale = new("HumanFemale");
        OptionalMember<DyeSlot?> humanMale = new("HumanMale");
        OptionalMember<DyeSlot?> nornFemale = new("NornFemale");
        OptionalMember<DyeSlot?> nornMale = new("NornMale");
        OptionalMember<DyeSlot?> sylvariFemale = new("SylvariFemale");
        OptionalMember<DyeSlot?> sylvariMale = new("SylvariMale");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(@default.Name))
            {
                @default.Value = member.Value;
            }
            else if (member.NameEquals("overrides"))
            {
                foreach (var @override in member.Value.EnumerateObject())
                {
                    if (@override.NameEquals(asuraFemale.Name))
                    {
                        asuraFemale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(asuraMale.Name))
                    {
                        asuraMale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(charrFemale.Name))
                    {
                        charrFemale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(charrMale.Name))
                    {
                        charrMale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(humanFemale.Name))
                    {
                        humanFemale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(humanMale.Name))
                    {
                        humanMale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(nornFemale.Name))
                    {
                        nornFemale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(nornMale.Name))
                    {
                        nornMale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(sylvariFemale.Name))
                    {
                        sylvariFemale.Value = @override.Value;
                    }
                    else if (@override.NameEquals(sylvariMale.Name))
                    {
                        sylvariMale.Value = @override.Value;
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

        // The dye slot arrays can contain Null to represent the default color, so this is ugly
        // Perhaps there is a better way to model it with a Null Object pattern?
        return new DyeSlotInfo
        {
            Default =
                @default.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            AsuraFemale =
                asuraFemale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            AsuraMale =
                asuraMale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            CharrFemale =
                charrFemale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            CharrMale =
                charrMale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            HumanFemale =
                humanFemale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            HumanMale =
                humanMale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            NornFemale =
                nornFemale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            NornMale =
                nornMale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            SylvariFemale =
                sylvariFemale.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                ),
            SylvariMale = sylvariMale.SelectMany(
                value => value.ValueKind == JsonValueKind.Null
                    ? null
                    : value.GetDyeSlot(missingMemberBehavior)
            )
        };
    }
}
