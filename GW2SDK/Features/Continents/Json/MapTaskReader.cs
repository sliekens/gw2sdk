﻿using System;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Continents.Json
{
    [PublicAPI]
    public static class MapTaskReader
    {
        public static MapTask Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var objective = new RequiredMember<string>("objective");
            var level = new RequiredMember<int>("level");
            var coordinates = new RequiredMember<PointF>("coord");
            var boundaries = new RequiredMember<PointF>("bounds");
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
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MapTask
            {
                Id = id.GetValue(),
                Objective = objective.GetValue(),
                Level = level.GetValue(),
                Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
                Boundaries = boundaries.SelectMany(value => ReadPointF(value, missingMemberBehavior)),
                ChatLink = chatLink.GetValue()
            };
        }

        private static PointF ReadPointF(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var x = json[0]
                .GetSingle();
            var y = json[1]
                .GetSingle();
            return new PointF(x, y);
        }
    }
}
