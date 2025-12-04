using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class RangeJson
{
    public static Range GetRange(this in JsonElement json, out int? requiresTrait, out int? overrides)
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        RequiredMember range = "value";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Range"))
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
            else if (range.Match(member))
            {
                range = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Range
        {
            Text = text.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Distance = range.Map(static (in value) => value.GetInt32())
        };
    }
}
