using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Builds.Facts;

internal static class ComboFinisherJson
{
    public static ComboFinisher GetComboFinisher(
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
        RequiredMember finisherType = "finisher_type";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("ComboFinisher"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == "chance" && IsDefaultInt32(member))
            {
                // Ignore zero values, looks like unsanitized data
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
            else if (member.Name == percent.Name)
            {
                percent = member;
            }
            else if (member.Name == finisherType.Name)
            {
                finisherType = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComboFinisher
        {
            Text = text.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Percent = percent.Map(value => value.GetInt32()),
            FinisherName =
                finisherType.Map(value => value.GetEnum<ComboFinisherName>(missingMemberBehavior))
        };

        static bool IsDefaultInt32(JsonProperty jsonProperty)
        {
            return jsonProperty.Value.ValueKind == JsonValueKind.Number
                && jsonProperty.Value.TryGetInt32(out var value)
                && value == 0;
        }
    }
}
