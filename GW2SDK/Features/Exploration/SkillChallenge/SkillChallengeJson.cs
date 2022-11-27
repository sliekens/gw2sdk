using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.SkillChallenge;

[PublicAPI]
public static class SkillChallengeJson
{
    public static SkillChallenge GetSkillChallenge(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<double> coordinates = new("coord");

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember<string> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillChallenge
        {
            Id = id.GetValueOrEmpty(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble())
        };
    }
}
