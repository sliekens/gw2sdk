using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Emblems;

[PublicAPI]
public static class EmblemReader
{
    public static Emblem GetEmblem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> layers = new("layers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(layers.Name))
            {
                layers.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Emblem
        {
            Id = id.GetValue(),
            Layers = layers.SelectMany(value => value.GetStringRequired())
        };
    }
}
