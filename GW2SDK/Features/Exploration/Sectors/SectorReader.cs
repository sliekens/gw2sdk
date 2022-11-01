using System;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Sectors;

[PublicAPI]
public static class SectorReader
{
    public static Sector GetSector(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember<string> name = new("name");
        RequiredMember<int> level = new("level");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<PointF> boundaries = new("bounds");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(boundaries.Name))
            {
                boundaries.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Sector
        {
            Id = id.GetValue(),
            Name = name.GetValueOrEmpty(),
            Level = level.GetValue(),
            Coordinates = coordinates.Select(value => value.GetCoordinate(missingMemberBehavior)),
            Boundaries = boundaries.SelectMany(value => value.GetCoordinate(missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}
