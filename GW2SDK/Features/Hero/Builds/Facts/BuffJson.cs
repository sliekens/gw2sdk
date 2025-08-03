using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Facts;

internal static class BuffJson
{
    public static Buff GetBuff(this in JsonElement json, out int? requiresTrait, out int? overrides)
    {
        requiresTrait = null;
        overrides = null;

        RequiredMember text = "text";
        RequiredMember icon = "icon";
        NullableMember duration = "duration";
        OptionalMember status = "status";
        OptionalMember description = "description";
        NullableMember applyCount = "apply_count";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Buff"))
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        return new Buff
        {
            Text = text.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Duration = duration.Map(static (in JsonElement value) => TimeSpan.FromSeconds(value.GetDouble())),
            Status = status.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Description = description.Map(static (in JsonElement value) => value.GetString()) ?? "",
            ApplyCount = applyCount.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
