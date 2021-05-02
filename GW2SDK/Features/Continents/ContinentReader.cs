using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class ContinentReader : IContinentReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        public IFloorReader Floor { get; } = new FloorReader();

        public Continent Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var name = new RequiredMember<string>("name");
            var continentDimensions = new RequiredMember<int[]>("continent_dims");
            var minZoom = new RequiredMember<int>("min_zoom");
            var maxZoom = new RequiredMember<int>("max_zoom");
            var floors = new RequiredMember<int[]>("floors");
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(continentDimensions.Name))
                {
                    continentDimensions = continentDimensions.From(member.Value);
                }
                else if (member.NameEquals(minZoom.Name))
                {
                    minZoom = minZoom.From(member.Value);
                }
                else if (member.NameEquals(maxZoom.Name))
                {
                    maxZoom = maxZoom.From(member.Value);
                }
                else if (member.NameEquals(floors.Name))
                {
                    floors = floors.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Continent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                ContinentDimensions = continentDimensions.Select(value => value.GetArray(item => item.GetInt32())),
                MinZoom = minZoom.GetValue(),
                MaxZoom = maxZoom.GetValue(),
                Floors = floors.Select(value => value.GetArray(item => item.GetInt32()))
            };
        }
    }
}
