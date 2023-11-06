﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class SkillReferenceJson
{
    public static SkillReference GetSkillReference(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Profession":
                return json.GetProfessionSkillReference(missingMemberBehavior);
            case "Heal":
                return json.GetHealingSkillReference(missingMemberBehavior);
            case "Utility":
                return json.GetUtilitySkillReference(missingMemberBehavior);
            case "Elite":
                return json.GetEliteSkillReference(missingMemberBehavior);
        }

        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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

        return new SkillReference
        {
            Id = id.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior))
        };
    }
}