using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Meta;

[PublicAPI]
public static class SchemaJson
{
    public static Schema GetSchema(
        this JsonElement jsonElement,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember version = new("v");

        RequiredMember description = new("desc");

        foreach (var member in jsonElement.EnumerateObject())
        {
            if (member.NameEquals(version.Name))
            {
                version = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Schema
        {
            Version = version.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired())
        };
    }
}
