using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class FloorReader : IFloorReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        public IRegionReader Region { get; } = new RegionReader();

        public Floor Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var textureDimensions = new RequiredMember<double[]>("texture_dims");
            var clampedView = new OptionalMember<double[][]>("clamped_view");
            var regions = new RequiredMember<Dictionary<int, Region>>("regions");
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(textureDimensions.Name))
                {
                    textureDimensions = textureDimensions.From(member.Value);
                }
                else if (member.NameEquals(clampedView.Name))
                {
                    clampedView = clampedView.From(member.Value);
                }
                else if (member.NameEquals(regions.Name))
                {
                    regions = regions.From(member.Value);
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

            return new Floor
            {
                Id = id.GetValue(),
                TextureDimensions = textureDimensions.Select(value => value.GetArray(item => item.GetDouble())),
                ClampedView = clampedView.Select(rectangle => rectangle.GetArray(point => point.GetArray(coord => coord.GetDouble()))),
                Regions = regions.Select(value => ReadRegions(value, missingMemberBehavior))
            };
        }

        private Dictionary<int, Region> ReadRegions(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var regions = new Dictionary<int, Region>();
            foreach (var member in json.EnumerateObject())
            {
                if (int.TryParse(member.Name, out var id))
                {
                    regions[id] = Region.Read(member.Value, missingMemberBehavior);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return regions;
        }
    }
}