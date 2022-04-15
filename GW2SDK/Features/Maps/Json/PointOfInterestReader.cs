using System;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Maps.Models;
using JetBrains.Annotations;

namespace GW2SDK.Maps.Json;

[PublicAPI]
public static class PointOfInterestReader
{
    public static PointOfInterest Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        switch (json.GetProperty("type")
                    .GetString())
        {
            case "landmark":
                return ReadLandmark(json, missingMemberBehavior);
            case "waypoint":
                return ReadWaypoint(json, missingMemberBehavior);
            case "vista":
                return ReadVista(json, missingMemberBehavior);
            case "unlock":
                return ReadUnlockerPointOfInterest(json, missingMemberBehavior);
        }

        OptionalMember<string> name = new("name");
        RequiredMember<int> floor = new("floor");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(floor.Name))
            {
                floor = floor.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
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

        return new PointOfInterest
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Floor = floor.GetValue(),
            Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static Landmark ReadLandmark(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        OptionalMember<string> name = new("name");
        RequiredMember<int> floor = new("floor");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("landmark"))
                {
                    throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(floor.Name))
            {
                floor = floor.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
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

        return new Landmark
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Floor = floor.GetValue(),
            Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
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

    private static UnlockerPointOfInterest ReadUnlockerPointOfInterest(
        JsonElement json,
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
                    throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(floor.Name))
            {
                floor = floor.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
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
            Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue(),
            Icon = icon.GetValue()
        };
    }

    private static Vista ReadVista(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        OptionalMember<string> name = new("name");
        RequiredMember<int> floor = new("floor");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("vista"))
                {
                    throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(floor.Name))
            {
                floor = floor.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
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

        return new Vista
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Floor = floor.GetValue(),
            Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static Waypoint ReadWaypoint(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        OptionalMember<string> name = new("name");
        RequiredMember<int> floor = new("floor");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("waypoint"))
                {
                    throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(floor.Name))
            {
                floor = floor.From(member.Value);
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = coordinates.From(member.Value);
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

        return new Waypoint
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Floor = floor.GetValue(),
            Coordinates = coordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}
