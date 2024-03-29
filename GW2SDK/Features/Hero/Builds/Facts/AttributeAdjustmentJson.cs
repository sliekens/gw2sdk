﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class AttributeAdjustmentJson
{
    public static AttributeAdjustment GetAttributeAdjustment(
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
        NullableMember adjustment = "value";
        NullableMember target = "target";
        NullableMember hitCount = "hit_count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("AttributeAdjust"))
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
            else if (adjustment.Match(member))
            {
                adjustment = member;
            }
            else if (target.Match(member))
            {
                target = member;
            }
            else if (hitCount.Match(member))
            {
                hitCount = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeAdjustment
        {
            Text = text.Map(value => value.GetString()) ?? "",
            IconHref = icon.Map(value => value.GetStringRequired()),
            Value = adjustment.Map(value => value.GetInt32()),
            Target = target.Map(value => value.GetAttributeName(missingMemberBehavior)),
            HitCount = hitCount.Map(value => value.GetInt32())
        };
    }
}
