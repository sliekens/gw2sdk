using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public static class ComboFinisherTraitFactJson
{
    public static ComboFinisherTraitFact GetComboFinisherTraitFact(
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
        RequiredMember<int> percent = new("percent");
        RequiredMember<ComboFinisherName> finisherType = new("finisher_type");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboFinisher"))
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
            else if (member.NameEquals(finisherType.Name))
            {
                finisherType.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFinisherTraitFact
        {
            Text = text.GetValueOrEmpty(),
            Icon = icon.GetValueOrEmpty(),
            Percent = percent.GetValue(),
            FinisherName = finisherType.GetValue(missingMemberBehavior)
        };
    }
}
