using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Facts;

internal static class NumberJson
{
    public static Number GetNumber(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        OptionalMember text = "text";
        RequiredMember icon = "icon";
        RequiredMember number = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Number"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == "requires_trait")
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.Name == "overrides")
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.Name == text.Name)
            {
                text = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == number.Name)
            {
                number = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Number
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetStringRequired()),
            Value = number.Map(value => value.GetInt32())
        };
    }
}
