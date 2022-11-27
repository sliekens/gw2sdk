using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public static class PrefixedBuffSkillFactJson
{
    public static PrefixedBuffSkillFact GetPrefixedBuffSkillFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
        NullableMember<TimeSpan> duration = new("duration");
        OptionalMember<string> status = new("status");
        OptionalMember<string> description = new("description");
        NullableMember<int> applyCount = new("apply_count");
        RequiredMember<BuffPrefix> prefix = new("prefix");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("PrefixedBuff"))
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
            else if (member.NameEquals(status.Name))
            {
                status.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(applyCount.Name))
            {
                applyCount.Value = member.Value;
            }
            else if (member.NameEquals(prefix.Name))
            {
                prefix.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PrefixedBuffSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.GetValueOrEmpty(),
            Description = description.GetValueOrEmpty(),
            ApplyCount = applyCount.GetValue(),
            Prefix = prefix.Select(value => value.GetBuffPrefix(missingMemberBehavior))
        };
    }
}
