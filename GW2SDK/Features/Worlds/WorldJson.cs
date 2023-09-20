using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Worlds;

[PublicAPI]
public static class WorldJson
{
    public static World GetWorld(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<WorldPopulation> population = new("population");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(population.Name))
            {
                population.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new World
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Population = population.GetValue(missingMemberBehavior)
        };
    }
}
