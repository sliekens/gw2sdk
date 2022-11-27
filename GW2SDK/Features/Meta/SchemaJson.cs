using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
public static class SchemaJson
{
    public static Schema GetSchema(
        this JsonElement jsonElement,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> version = new("v");

        RequiredMember<string> description = new("desc");

        foreach (var member in jsonElement.EnumerateObject())
        {
            if (member.NameEquals(version.Name))
            {
                version.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Schema
        {
            Version = version.GetValue(),
            Description = description.GetValue()
        };
    }
}
