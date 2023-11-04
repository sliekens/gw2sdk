using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Facts;

internal static class DurationJson
{
    public static Duration GetDuration(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        RequiredMember duration = "duration";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Duration"))
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
            else if (member.Name == duration.Name)
            {
                duration = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Duration
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Length = duration.Map(value => TimeSpan.FromSeconds(value.GetDouble()))
        };
    }
}
