using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class AttributeAdjustTraitFactJson
{
    public static AttributeAdjustTraitFact GetAttributeAdjustTraitFact(
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
        RequiredMember adjustment = "value";
        RequiredMember target = "target";
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
            else if (member.NameEquals(text.Name))
            {
                text = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(adjustment.Name))
            {
                adjustment = member;
            }
            else if (member.NameEquals(target.Name))
            {
                target = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeAdjustTraitFact
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetString()) ?? "",
            Value = adjustment.Map(value => value.GetInt32()),
            Target = target.Map(
                value => value.GetEnum<AttributeAdjustTarget>(missingMemberBehavior)
            )
        };
    }
}
