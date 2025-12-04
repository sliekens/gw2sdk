using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillBarJson
{
    public static SkillBar GetSkillBar(this in JsonElement json)
    {
        NullableMember heal = "heal";
        RequiredMember utilities = "utilities";
        NullableMember elite = "elite";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (heal.Match(member))
            {
                heal = member;
            }
            else if (utilities.Match(member))
            {
                utilities = member;
            }
            else if (elite.Match(member))
            {
                elite = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        (int? utilitySkillId, int? utilitySkillId2, int? utilitySkillId3) = utilities.Map(static (in values) => values.GetUtilitySkillIds());
        return new SkillBar
        {
            HealSkillId = heal.Map(static (in value) => value.GetInt32()),
            UtilitySkillId1 = utilitySkillId,
            UtilitySkillId2 = utilitySkillId2,
            UtilitySkillId3 = utilitySkillId3,
            EliteSkillId = elite.Map(static (in value) => value.GetInt32())
        };
    }
}
