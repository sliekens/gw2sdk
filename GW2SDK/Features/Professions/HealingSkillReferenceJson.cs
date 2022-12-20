﻿using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
public static class HealingSkillReferenceJson
{
    public static HealingSkillReference GetHealingSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SkillSlot> slot = new("slot");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(slot.Name))
            {
                slot.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealingSkillReference
        {
            Id = id.GetValue(),
            Slot = slot.GetValue(missingMemberBehavior)
        };
    }
}