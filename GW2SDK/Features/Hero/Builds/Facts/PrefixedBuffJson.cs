﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class PrefixedBuffJson
{
    public static PrefixedBuff GetPrefixedBuff(
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
        NullableMember duration = "duration";
        OptionalMember status = "status";
        OptionalMember description = "description";
        NullableMember applyCount = "apply_count";
        RequiredMember prefixIcon = "icon";
        OptionalMember prefixStatus = "status";

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
            else if (text.Match(member))
            {
                text = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (duration.Match(member))
            {
                duration = member;
            }
            else if (status.Match(member))
            {
                status = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (applyCount.Match(member))
            {
                applyCount = member;
            }
            else if (member.NameEquals("prefix"))
            {
                foreach (var prefixMember in member.Value.EnumerateObject())
                {
                    if (prefixIcon.Match(prefixMember))
                    {
                        prefixIcon = prefixMember;
                    }
                    else if (prefixStatus.Match(prefixMember))
                    {
                        prefixStatus = prefixMember;
                    }
                    else if (prefixMember.Name is "text" or "description")
                    {
                        // Discard useless text
                    }
                    else if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(
                            Strings.UnexpectedMember(prefixMember.Name)
                        );
                    }
                }
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PrefixedBuff
        {
            Precondition = prefixStatus.Map(value => value.GetString()) ?? "",
            PrefixIconHref = prefixIcon.Map(value => value.GetStringRequired()),
            Text = text.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Duration = duration.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Map(value => value.GetString()) ?? "",
            Description = description.Map(value => value.GetString()) ?? "",
            ApplyCount = applyCount.Map(value => value.GetInt32())
        };
    }
}
