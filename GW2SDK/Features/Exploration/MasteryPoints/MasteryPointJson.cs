using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.MasteryPoints;

[PublicAPI]
public static class MasteryPointJson
{
    public static MasteryPoint GetMasteryPoint(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember region = "region";
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
            else if (member.NameEquals(region.Name))
            {
                region = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPoint
        {
            Id = id.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            Region = region.Map(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior))
        };
    }
}
