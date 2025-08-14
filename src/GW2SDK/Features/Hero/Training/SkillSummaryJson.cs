using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Hero.Training.Skills;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class SkillSummaryJson
{
    public static SkillSummary GetSkillSummary(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Profession":
                    return json.GetProfessionSkillSummary();
                case "Heal":
                    return json.GetHealingSkillSummary();
                case "Utility":
                    return json.GetUtilitySkillSummary();
                case "Elite":
                    return json.GetEliteSkillSummary();
                default:
                    break;
            }
        }

        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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

        return new SkillSummary
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<SkillSlot>())
        };
    }
}
