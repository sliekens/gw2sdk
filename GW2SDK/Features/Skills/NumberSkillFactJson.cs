﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

[PublicAPI]
public static class NumberSkillFactJson
{
    public static NumberSkillFact GetNumberSkillFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        OptionalMember text = new("text");
        RequiredMember icon = new("icon");
        RequiredMember number = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Number"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(number.Name))
            {
                number = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NumberSkillFact
        {
            Text = text.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetStringRequired()),
            Value = number.Select(value => value.GetInt32())
        };
    }
}
