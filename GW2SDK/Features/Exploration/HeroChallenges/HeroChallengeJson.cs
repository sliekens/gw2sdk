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
        RequiredMember coordinates = "coord";

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember id = "id";
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
            Id = id.Map(value => value.GetString()) ?? "",
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior))
        };
    }
}
