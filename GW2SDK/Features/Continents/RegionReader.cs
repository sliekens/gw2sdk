using System;
using System.Collections.Generic;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class RegionReader : IRegionReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        public IMapReader Map { get; } = new MapReader();

        public Region Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var name = new RequiredMember<string>("name");
            var labelCoordinates = new RequiredMember<double[]>("label_coord");
            var continentRectangle = new RequiredMember<double[][]>("continent_rect");
            var maps = new RequiredMember<Dictionary<int, Map>>("maps");
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(labelCoordinates.Name))
                {
                    labelCoordinates = labelCoordinates.From(member.Value);
                }
                else if (member.NameEquals(continentRectangle.Name))
                {
                    continentRectangle = continentRectangle.From(member.Value);
                }
                else if (member.NameEquals(maps.Name))
                {
                    maps = maps.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Region
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                LabelCoordinates = labelCoordinates.Select(value => value.GetArray(item => item.GetDouble())),
                ContinentRectangle = continentRectangle.Select(rectangle => rectangle.GetArray(point => point.GetArray(coord => coord.GetDouble()))),
                Maps = maps.Select(value => ReadMaps(value, missingMemberBehavior))
            };
        }

        private Dictionary<int, Map> ReadMaps(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var maps = new Dictionary<int, Map>();
            foreach (var member in json.EnumerateObject())
            {
                if (int.TryParse(member.Name, out var id))
                {
                    maps[id] = Map.Read(member.Value, missingMemberBehavior);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return maps;
        }
    }
}