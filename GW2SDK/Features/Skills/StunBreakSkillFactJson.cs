﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

internal static class StunBreakSkillFactJson
{
    public static StunBreakSkillFact GetStunBreakSkillFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        RequiredMember stunBreak = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("StunBreak"))
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
            else if (member.NameEquals(stunBreak.Name))
            {
                stunBreak = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StunBreakSkillFact
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Value = stunBreak.Map(value => value.GetBoolean())
        };
    }
}
