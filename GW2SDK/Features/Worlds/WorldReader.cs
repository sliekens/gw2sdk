using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public sealed class WorldReader : IWorldReader
    {
        public World Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var population = new RequiredMember<WorldPopulation>("population");
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
                Population = population.GetValue()
            };
        }

        public IJsonReader<int> Id => new Int32JsonReader();
    }
}
