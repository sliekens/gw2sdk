using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Skills;

internal static class PercentSkillFactJson
{
    public static PercentSkillFact GetPercentSkillFact(
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
        RequiredMember percent = "percent";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Percent"))
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
            else if (member.Name == percent.Name || member.Name == "value")
            {
                // Some use the name 'percent', some use 'value'... weird
                percent = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PercentSkillFact
        {
            Text = text.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Percent = percent.Map(value => value.GetDouble())
        };
    }
}
