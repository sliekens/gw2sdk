﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class HealingSkillReferenceJson
{
    public static HealingSkillReference GetHealingSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Heal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == slot.Name)
            {
                slot = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HealingSkillReference
        {
            Id = id.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior))
        };
    }
}