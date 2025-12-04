using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Metadata;

internal static class SchemaJson
{
    public static Schema GetSchema(this in JsonElement jsonElement)
    {
        RequiredMember version = "v";

        RequiredMember description = "desc";

        foreach (JsonProperty member in jsonElement.EnumerateObject())
        {
            if (version.Match(member))
            {
                version = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Schema
        {
            Version = version.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired())
        };
    }
}
