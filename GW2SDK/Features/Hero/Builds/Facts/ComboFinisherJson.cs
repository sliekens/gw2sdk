using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class ComboFinisherJson
{
    public static ComboFinisher GetComboFinisher(
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
        RequiredMember finisherType = "finisher_type";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ComboFinisher"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (finisherType.Match(member))
            {
                finisherType = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static value => value.GetStringRequired());
        return new ComboFinisher
        {
            Text = text.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Percent = percent.Map(static value => value.GetInt32()),
            FinisherName = finisherType.Map(static value => value.GetEnum<ComboFinisherName>())
        };

        static bool IsDefaultInt32(JsonProperty jsonProperty)
        {
            return jsonProperty.Value.ValueKind == JsonValueKind.Number
                && jsonProperty.Value.TryGetInt32(out var value)
                && value == 0;
        }
    }
}
