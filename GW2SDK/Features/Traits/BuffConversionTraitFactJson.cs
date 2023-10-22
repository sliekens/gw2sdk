using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

internal static class BuffConversionTraitFactJson
{
    public static BuffConversionTraitFact GetBuffConversionTraitFact(
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
        RequiredMember percent = "percent";
        RequiredMember source = "source";
        RequiredMember target = "target";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("BuffConversion"))
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
            else if (member.NameEquals(percent.Name))
            {
                percent = member;
            }
            else if (member.NameEquals(source.Name))
            {
                source = member;
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

        return new BuffConversionTraitFact
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetString()) ?? "",
            Percent = percent.Map(value => value.GetInt32()),
            Source = source.Map(
                value => value.GetEnum<AttributeAdjustTarget>(missingMemberBehavior)
            ),
            Target = target.Map(
                value => value.GetEnum<AttributeAdjustTarget>(missingMemberBehavior)
            )
        };
    }
}
