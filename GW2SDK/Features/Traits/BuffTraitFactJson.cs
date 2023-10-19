using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class BuffTraitFactJson
{
    public static BuffTraitFact GetBuffTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember text = new("text");
        OptionalMember icon = new("icon");
        NullableMember duration = new("duration");
        OptionalMember status = new("status");
        OptionalMember description = new("description");
        NullableMember applyCount = new("apply_count");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Buff"))
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuffTraitFact
        {
            Text = text.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetString()) ?? "",
            Duration = duration.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Select(value => value.GetString()) ?? "",
            Description = description.Select(value => value.GetString()) ?? "",
            ApplyCount = applyCount.Select(value => value.GetInt32())
        };
    }
}
