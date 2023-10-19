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
        NullableMember heal = new("heal");
        RequiredMember utilities = new("utilities");
        NullableMember elite = new("elite");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(heal.Name))
            {
                heal.Value = member.Value;
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities.Value = member.Value;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite.Value = member.Value;
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
