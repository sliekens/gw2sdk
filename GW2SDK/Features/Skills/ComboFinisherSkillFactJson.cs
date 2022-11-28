using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public static class ComboFinisherSkillFactJson
{
    public static ComboFinisherSkillFact GetComboFinisherSkillFact(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember<string> text = new("text");
        RequiredMember<string> icon = new("icon");
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
            else if (member.NameEquals("chance") && IsDefaultInt32(member))
            {
                // Ignore zero values, looks like unsanitized data
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

        return new ComboFinisherSkillFact
        {
            Text = text.GetValue(),
            Icon = icon.GetValue(),
            Percent = percent.GetValue(),
            FinisherName = finisherType.GetValue(missingMemberBehavior)
        };

        static bool IsDefaultInt32(JsonProperty jsonProperty)
        {
            return jsonProperty.Value.ValueKind == JsonValueKind.Number
                && jsonProperty.Value.TryGetInt32(out var value)
                && value == 0;
        }
    }
}
