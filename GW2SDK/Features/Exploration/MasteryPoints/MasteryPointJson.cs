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
        RequiredMember coordinates = new("coord");
        RequiredMember id = new("id");
        RequiredMember region = new("region");
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
            else if (member.NameEquals(region.Name))
            {
                region.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPoint
        {
            Id = id.Select(value => value.GetInt32()),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            Region = region.Select(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior))
        };
    }
}
