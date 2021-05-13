using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class MapTaskReader : IJsonReader<MapTask>
    {
        public MapTask Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var objective = new RequiredMember<string>("objective");
            var level = new RequiredMember<int>("level");
            var coordinates = new RequiredMember<double[]>("coord");
            var boundaries = new RequiredMember<double[][]>("bounds");
            var id = new RequiredMember<int>("id");
            var chatLink = new RequiredMember<string>("chat_link");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(objective.Name))
                {
                    objective = objective.From(member.Value);
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
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new MapTask
            {
                Id = id.GetValue(),
                Objective = objective.GetValue(),
                Level = level.GetValue(),
                Coordinates = coordinates.Select(value => value.GetArray(item => item.GetDouble())),
                Boundaries = boundaries.Select(value => value.GetArray(point => point.GetArray(item => item.GetDouble()))),
                ChatLink = chatLink.GetValue()
            };
        }
    }
}