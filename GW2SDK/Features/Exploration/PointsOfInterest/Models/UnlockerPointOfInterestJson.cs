﻿using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

[PublicAPI]
public static class UnlockerPointOfInterestJson
{
    public static UnlockerPointOfInterest GetUnlockerPointOfInterest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember<string> name = new("name");
        RequiredMember<int> floor = new("floor");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        RequiredMember<string> icon = new("icon");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("unlock"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(floor.Name))
            {
                floor.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UnlockerPointOfInterest
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Floor = floor.GetValue(),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.GetValue(),
            Icon = icon.GetValue()
        };
    }
}
