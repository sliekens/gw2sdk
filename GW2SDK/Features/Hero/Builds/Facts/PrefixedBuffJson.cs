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

        foreach (var member in json.EnumerateObject())
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
                foreach (var prefixMember in member.Value.EnumerateObject())
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

        var iconString = icon.Map(static (in JsonElement value) => value.GetString()) ?? "";
        var prefixIconString = prefixIcon.Map(static (in JsonElement value) => value.GetString()) ?? "";
#pragma warning disable CS0618
        return new PrefixedBuff
        {
            Precondition = prefixStatus.Map(static (in JsonElement value) => value.GetString()) ?? "",
            PrefixIconHref = prefixIconString,
            PrefixIconUrl = !string.IsNullOrEmpty(prefixIconString) ? new Uri(prefixIconString, UriKind.RelativeOrAbsolute) : null!, // not null
            Text = text.Map(static (in JsonElement value) => value.GetStringRequired()),
            IconHref = iconString,
            IconUrl = !string.IsNullOrEmpty(iconString) ? new Uri(iconString, UriKind.RelativeOrAbsolute) : null!, // not null
            Duration = duration.Map(static (in JsonElement value) => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            ApplyCount = applyCount.Map(static (in JsonElement value) => value.GetInt32())
        };
#pragma warning restore CS0618
    }
}
