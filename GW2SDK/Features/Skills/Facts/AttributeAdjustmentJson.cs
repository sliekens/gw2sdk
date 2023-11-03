using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills.Facts;

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
        RequiredMember target = "target";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("AttributeAdjust"))
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
            else if (member.Name == adjustment.Name)
            {
                adjustment = member;
            }
            else if (member.Name == target.Name)
            {
                target = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeAdjustment
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetStringRequired()),
            Value = adjustment.Map(value => value.GetInt32()),
            Target = target.Map(
                value => value.GetEnum<AttributeAdjustmentTarget>(missingMemberBehavior)
            )
        };
    }
}
