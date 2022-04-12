using System;
using System.Text.Json;

using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Maps.Json;

[PublicAPI]
public static class WorldReader
{
    public static World Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<WorldPopulation> population = new("population");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(population.Name))
            {
                population = population.From(member.Value);
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