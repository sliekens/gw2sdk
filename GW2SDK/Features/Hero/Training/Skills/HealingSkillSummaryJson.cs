﻿using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal static class HealingSkillSummaryJson
{
    public static HealingSkillSummary GetHealingSkillSummary(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new HealingSkillSummary
        {
            Id = id.Map(static value => value.GetInt32()),
            Slot = slot.Map(static value => value.GetEnum<SkillSlot>())
        };
    }
}
