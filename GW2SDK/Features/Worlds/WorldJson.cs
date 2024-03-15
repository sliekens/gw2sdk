using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Worlds;

internal static class WorldJson
{
    public static World GetWorld(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember population = "population";
        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (population.Match(member))
            {
                population = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new World
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Population = population.Map(
                value => value.GetEnum<WorldPopulation>(missingMemberBehavior)
            )
        };
    }
}
