using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.HeroChallenges;

internal static class HeroChallengeJson
{
    public static HeroChallenge GetHeroChallenge(this JsonElement json)
    {
        RequiredMember coordinates = "coord";

        // The 'id' is missing from hero points in End of Dragon maps
        OptionalMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new HeroChallenge
        {
            Id = id.Map(static value => value.GetString()) ?? "",
            Coordinates = coordinates.Map(static value => value.GetCoordinateF())
        };
    }
}
