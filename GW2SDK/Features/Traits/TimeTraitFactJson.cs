using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TimeTraitFactJson
{
    public static TimeTraitFact GetTimeTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<TimeSpan> duration = new("duration");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Time"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals("requires_trait"))
            {
                requiresTrait = member.Value.GetInt32();
            }
            else if (member.NameEquals("overrides"))
            {
                overrides = member.Value.GetInt32();
            }
            else if (member.NameEquals(text.Name))
            {
                text.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(duration.Name))
            {
                duration.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TimeTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble()))
        };
    }
}
