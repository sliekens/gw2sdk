using System;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Continents.Json
{
    [PublicAPI]
    public static class ContinentReader
    {
        public static Continent Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var continentDimensions = new RequiredMember<Size>("continent_dims");
            var minZoom = new RequiredMember<int>("min_zoom");
            var maxZoom = new RequiredMember<int>("max_zoom");
            var floors = new RequiredMember<int>("floors");
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
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Continent
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                ContinentDimensions = continentDimensions.Select(value => ReadSize(value, missingMemberBehavior)),
                MinZoom = minZoom.GetValue(),
                MaxZoom = maxZoom.GetValue(),
                Floors = floors.SelectMany(value => value.GetInt32())
            };
        }

        private static Size ReadSize(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var width = json[0]
                .GetInt32();
            var height = json[1]
                .GetInt32();
            return new Size(width, height);
        }
    }
}
