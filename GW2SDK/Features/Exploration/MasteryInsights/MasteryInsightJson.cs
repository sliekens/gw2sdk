using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.MasteryInsights;

internal static class MasteryInsightJson
{
    public static MasteryInsight GetMasteryInsight(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember region = "region";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == coordinates.Name)
            {
                coordinates = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == region.Name)
            {
                region = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryInsight
        {
            Id = id.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            Region = region.Map(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior))
        };
    }
}
