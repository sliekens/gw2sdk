using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

internal static class SkillBarJson
{
    public static SkillBar GetSkillBar(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember heal = "heal";
        RequiredMember utilities = "utilities";
        NullableMember elite = "elite";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == heal.Name)
            {
                heal = member;
            }
            else if (member.Name == utilities.Name)
            {
                utilities = member;
            }
            else if (member.Name == elite.Name)
            {
                elite = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillBar
        {
            Heal = heal.Map(value => value.GetInt32()),
            Utilities = utilities.Map(values => values.GetList(value => value.GetNullableInt32())),
            Elite = elite.Map(value => value.GetInt32())
        };
    }
}
