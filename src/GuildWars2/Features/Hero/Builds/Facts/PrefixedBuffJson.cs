using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class PrefixedBuffJson
{
    public static PrefixedBuff GetPrefixedBuff(
        this in JsonElement json,
        out int? requiresTrait,
        out int? overrides
    )
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        NullableMember duration = "duration";
        OptionalMember status = "status";
        OptionalMember description = "description";
        NullableMember applyCount = "apply_count";
        RequiredMember prefixIcon = "icon";
        OptionalMember prefixStatus = "status";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("PrefixedBuff"))
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
            else if (duration.Match(member))
            {
                duration = member;
            }
            else if (status.Match(member))
            {
                status = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (applyCount.Match(member))
            {
                applyCount = member;
            }
            else if (member.NameEquals("prefix"))
            {
                foreach (JsonProperty prefixMember in member.Value.EnumerateObject())
                {
                    if (prefixIcon.Match(prefixMember))
                    {
                        prefixIcon = prefixMember;
                    }
                    else if (prefixStatus.Match(prefixMember))
                    {
                        prefixStatus = prefixMember;
                    }
                    else if (prefixMember.Name is "text" or "description")
                    {
                        // Discard useless text
                    }
                    else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        ThrowHelper.ThrowUnexpectedMember(prefixMember.Name);
                    }
                }
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetString()) ?? "";
        string prefixIconString = prefixIcon.Map(static (in value) => value.GetString()) ?? "";
        return new PrefixedBuff
        {
            Precondition = prefixStatus.Map(static (in value) => value.GetString()) ?? "",
            PrefixIconUrl = !string.IsNullOrEmpty(prefixIconString) ? new Uri(prefixIconString, UriKind.RelativeOrAbsolute) : null!, // not null
            Text = text.Map(static (in value) => value.GetStringRequired()),
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString, UriKind.RelativeOrAbsolute) : null!, // not null
            Duration = duration.Map(static (in value) => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Map(static (in value) => value.GetString()) ?? "",
            Description = description.Map(static (in value) => value.GetString()) ?? "",
            ApplyCount = applyCount.Map(static (in value) => value.GetInt32())
        };
    }
}
