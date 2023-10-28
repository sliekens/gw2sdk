﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skins;

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

        // The dye slot arrays can contain Null to represent the default color, so this is ugly
        // Perhaps there is a better way to model it with a Null Object pattern?
        return new DyeSlotInfo
        {
            Default =
                @default.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            AsuraFemale =
                asuraFemale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            AsuraMale =
                asuraMale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            CharrFemale =
                charrFemale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            CharrMale =
                charrMale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            HumanFemale =
                humanFemale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            HumanMale =
                humanMale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            NornFemale =
                nornFemale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            NornMale =
                nornMale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            SylvariFemale =
                sylvariFemale.Map(
                    values =>
                        values.GetList(
                            value =>
                                value.ValueKind == JsonValueKind.Null
                                    ? null
                                    : value.GetDyeSlot(missingMemberBehavior)
                        )
                ),
            SylvariMale = sylvariMale.Map(
                values => values.GetList(
                    value => value.ValueKind == JsonValueKind.Null
                        ? null
                        : value.GetDyeSlot(missingMemberBehavior)
                )
            )
        };
    }
}
