using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class MapSectorReader : IJsonReader<MapSector>
    {
        public MapSector Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new OptionalMember<string>("name");
            var level = new RequiredMember<int>("level");
            var coordinates = new RequiredMember<double[]>("coord");
            var boundaries = new RequiredMember<double[][]>("bounds");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(level.Name))
                {
                    level = level.From(member.Value);
                }
                else if (member.NameEquals(coordinates.Name))
                {
                    coordinates = coordinates.From(member.Value);
                }
                else if (member.NameEquals(boundaries.Name))
                {
                    boundaries = boundaries.From(member.Value);
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(chatLink.Name))
                {
                    chatLink = chatLink.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MapSector
            {
                Id = id.GetValue(),
                Name = name.GetValueOrEmpty(),
                Level = level.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                Boundaries = boundaries.Select(value => value.GetArray(point => point.GetArray(item => item.GetDouble()))),
                ChatLink = chatLink.GetValue()
            };
        }
    }
}