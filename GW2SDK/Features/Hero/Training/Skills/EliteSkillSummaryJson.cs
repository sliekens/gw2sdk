using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Skills;

internal static class EliteSkillSummaryJson
{
    public static EliteSkillSummary GetEliteSkillSummary(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember slot = "slot";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Elite"))
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

        return new EliteSkillSummary
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<SkillSlot>())
        };
    }
}
