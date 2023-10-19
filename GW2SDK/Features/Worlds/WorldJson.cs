using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Worlds;

[PublicAPI]
public static class WorldJson
{
    public static World GetWorld(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember population = "population";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(population.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Population = population.Select(value => value.GetEnum<WorldPopulation>(missingMemberBehavior))
        };
    }
}
