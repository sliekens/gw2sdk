using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Worlds;

internal static class WorldJson
{
    public static World GetWorld(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new World
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Population = population.Map(static value => value.GetEnum<WorldPopulation>())
        };
    }
}
