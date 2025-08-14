using System.Text.Json;

using GuildWars2.Hero.Masteries;
using GuildWars2.Json;

namespace GuildWars2.Exploration.MasteryInsights;

internal static class MasteryInsightJson
{
    public static MasteryInsight GetMasteryInsight(this in JsonElement json)
    {
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember region = "region";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (region.Match(member))
            {
                region = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MasteryInsight
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in JsonElement value) => value.GetCoordinateF()),
            Region = region.Map(static (in JsonElement value) => value.GetEnum<MasteryRegionName>())
        };
    }
}
