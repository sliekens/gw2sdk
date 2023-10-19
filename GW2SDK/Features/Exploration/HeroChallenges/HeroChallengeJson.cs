using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.HeroChallenges;

[PublicAPI]
public static class HeroChallengeJson
{
    public static HeroChallenge GetHeroChallenge(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coordinates = new("coord");

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coordinates.Name))
            {
                coordinates = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HeroChallenge
        {
            Id = id.Select(value => value.GetString()) ?? "",
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior))
        };
    }
}
