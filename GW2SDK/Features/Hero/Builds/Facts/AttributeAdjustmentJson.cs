using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class AttributeAdjustmentJson
{
    public static AttributeAdjustment GetAttributeAdjustment(
        this JsonElement json,
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
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AttributeAdjustment
        {
            Text = text.Map(static value => value.GetString()) ?? "",
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Value = adjustment.Map(static value => value.GetInt32()),
            Target = target.Map(static value => value.GetAttributeName()),
            HitCount = hitCount.Map(static value => value.GetInt32())
        };
    }
}
