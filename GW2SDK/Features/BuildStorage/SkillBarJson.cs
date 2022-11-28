using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class SkillBarJson
{
    public static SkillBar GetSkillBar(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> heal = new("heal");
        RequiredMember<int?> utilities = new("utilities");
        NullableMember<int> elite = new("elite");

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
            Heal = heal.GetValue(),
            Utilities =
                utilities.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Elite = elite.GetValue()
        };
    }
}
