using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal static class UtilitySkillSummaryJson
{
    public static UtilitySkillSummary GetUtilitySkillSummary(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Utility"))
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

        return new UtilitySkillSummary
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Slot = slot.Map(static (in value) => value.GetEnum<SkillSlot>())
        };
    }
}
