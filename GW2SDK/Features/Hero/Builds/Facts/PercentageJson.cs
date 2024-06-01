using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class PercentageJson
{
    public static Percentage GetPercentage(
        this JsonElement json,
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
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Percent"))
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
            else if (percent.Match(member) || member.NameEquals("value"))
            {
                // Some use the name 'percent', some use 'value'... weird
                percent = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Percentage
        {
            Text = text.Map(static value => value.GetStringRequired()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Percent = percent.Map(static value => value.GetDouble())
        };
    }
}
