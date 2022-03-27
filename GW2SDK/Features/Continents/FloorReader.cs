using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class FloorReader : IFloorReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        public IRegionReader Region { get; } = new RegionReader();

        public Floor Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var textureDimensions = new RequiredMember<SizeF>("texture_dims");
            var clampedView = new OptionalMember<ContinentRectangle>("clamped_view");
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
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Floor
            {
                Id = id.GetValue(),
                TextureDimensions = textureDimensions.Select(value => ReadSizeF(value, missingMemberBehavior)),
                ClampedView = clampedView.Select(value => ReadContinentRectangle(value, missingMemberBehavior)),
                Regions = regions.Select(value => ReadRegions(value, missingMemberBehavior))
            };
        }

        private SizeF ReadSizeF(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var width = json[0]
                .GetSingle();
            var height = json[1]
                .GetSingle();
            return new SizeF(width, height);
        }

        private ContinentRectangle ReadContinentRectangle(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var topLeft = json[0];
            var x = topLeft[0]
                .GetSingle();
            var y = topLeft[1]
                .GetSingle();
            var size = json[1];
            var width = size[0]
                .GetSingle();
            var height = size[1]
                .GetSingle();
            return new ContinentRectangle
            {
                TopLeft = new PointF(x, y),
                Size = new SizeF(width, height)
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
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return regions;
        }
    }
}