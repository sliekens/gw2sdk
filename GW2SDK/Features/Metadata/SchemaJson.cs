using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class SchemaJson
{
    public static Schema GetSchema(
        this JsonElement jsonElement,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember version = "v";

        RequiredMember description = "desc";

        foreach (var member in jsonElement.EnumerateObject())
        {
            if (version.Match(member))
            {
                version = member;
            }
            else if (description.Match(member))
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
            Version = version.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired())
        };
    }
}
