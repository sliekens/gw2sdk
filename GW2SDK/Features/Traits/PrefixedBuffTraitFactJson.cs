using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class PrefixedBuffTraitFactJson
{
    public static PrefixedBuffTraitFact GetPrefixedBuffTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember text = "text";
        OptionalMember icon = "icon";
        NullableMember duration = "duration";
        OptionalMember status = "status";
        OptionalMember description = "description";
        NullableMember applyCount = "apply_count";
        RequiredMember prefix = "prefix";
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
                text = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(duration.Name))
            {
                duration = member;
            }
            else if (member.NameEquals(status.Name))
            {
                status = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(applyCount.Name))
            {
                applyCount = member;
            }
            else if (member.NameEquals(prefix.Name))
            {
                prefix = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PrefixedBuffTraitFact
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetString()) ?? "",
            Duration = duration.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Map(value => value.GetString()) ?? "",
            Description = description.Map(value => value.GetString()) ?? "",
            ApplyCount = applyCount.Map(value => value.GetInt32()),
            Prefix = prefix.Map(value => value.GetBuffPrefix(missingMemberBehavior))
        };
    }
}
