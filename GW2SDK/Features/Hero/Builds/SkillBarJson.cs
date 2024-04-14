using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SkillBarJson
{
    public static SkillBar GetSkillBar(
        this JsonElement json
    )
    {
        NullableMember heal = "heal";
        RequiredMember utilities = "utilities";
        NullableMember elite = "elite";

        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        var utilitySkills = utilities.Map(static values => values.GetUtilitySkillIds());
        return new SkillBar
        {
            HealSkillId = heal.Map(static value => value.GetInt32()),
            UtilitySkillId1 = utilitySkills.UtilitySkillId,
            UtilitySkillId2 = utilitySkills.UtilitySkillId2,
            UtilitySkillId3 = utilitySkills.UtilitySkillId3,
            EliteSkillId = elite.Map(static value => value.GetInt32())
        };
    }
}
