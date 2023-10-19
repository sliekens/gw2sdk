using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class SkillBarJson
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
            if (member.NameEquals(heal.Name))
            {
                heal = member;
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities = member;
            }
            else if (member.NameEquals(elite.Name))
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
            Heal = heal.Select(value => value.GetInt32()),
            Utilities =
                utilities.SelectMany<int?>(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Elite = elite.Select(value => value.GetInt32())
        };
    }
}
