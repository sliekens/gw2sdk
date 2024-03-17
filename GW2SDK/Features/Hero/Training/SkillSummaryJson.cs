using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Training.Skills;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class SkillSummaryJson
{
    public static SkillSummary GetSkillSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Profession":
                return json.GetProfessionSkillSummary(missingMemberBehavior);
            case "Heal":
                return json.GetHealingSkillSummary(missingMemberBehavior);
            case "Utility":
                return json.GetUtilitySkillSummary(missingMemberBehavior);
            case "Elite":
                return json.GetEliteSkillSummary(missingMemberBehavior);
        }

        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillSummary
        {
            Id = id.Map(value => value.GetInt32()),
            Slot = slot.Map(value => value.GetEnum<SkillSlot>(missingMemberBehavior))
        };
    }
}
