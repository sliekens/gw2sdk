using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class AttributeConversionJson
{
    public static AttributeConversion GetAttributeConversion(
        this JsonElement json,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember text = "text";
        OptionalMember icon = "icon";
        RequiredMember percent = "percent";
        RequiredMember source = "source";
        RequiredMember target = "target";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("BuffConversion"))
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
            else if (percent.Match(member))
            {
                percent = member;
            }
            else if (source.Match(member))
            {
                source = member;
            }
            else if (target.Match(member))
            {
                target = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AttributeConversion
        {
            Text = text.Map(static value => value.GetString()) ?? "",
            IconHref = icon.Map(static value => value.GetString()) ?? "",
            Percent = percent.Map(static value => value.GetInt32()),
            Source = source.Map(static value => value.GetAttributeName()),
            Target = target.Map(static value => value.GetAttributeName())
        };
    }
}
