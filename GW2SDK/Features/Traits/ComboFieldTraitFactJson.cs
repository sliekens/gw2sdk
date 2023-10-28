using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

internal static class ComboFieldTraitFactJson
{
    public static ComboFieldTraitFact GetComboFieldTraitFact(
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
        RequiredMember fieldType = "field_type";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("ComboField"))
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
            else if (member.Name == fieldType.Name)
            {
                fieldType = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFieldTraitFact
        {
            Text = text.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetString()) ?? "",
            Field = fieldType.Map(value => value.GetEnum<ComboFieldName>(missingMemberBehavior))
        };
    }
}
