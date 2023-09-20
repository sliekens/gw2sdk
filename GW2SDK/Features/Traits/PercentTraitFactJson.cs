using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class PercentTraitFactJson
{
    public static PercentTraitFact GetPercentTraitFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;
        OptionalMember<string> text = new("text");
        OptionalMember<string> icon = new("icon");
        RequiredMember<double> percent = new("percent");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Percent"))
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
                text.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(percent.Name))
            {
                percent.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PercentTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Percent = percent.GetValue()
        };
    }
}
