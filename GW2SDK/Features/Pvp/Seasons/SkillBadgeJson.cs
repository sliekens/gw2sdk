using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SkillBadgeJson
{
    public static SkillBadge GetSkillBadge(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember overlay = "overlay";
        RequiredMember smallOverlay = "overlay_small";
        RequiredMember tiers = "tiers";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (overlay.Match(member))
            {
                overlay = member;
            }
            else if (smallOverlay.Match(member))
            {
                smallOverlay = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        var overlayString = overlay.Map(static (in JsonElement value) => value.GetStringRequired());
        var smallOverlayString = smallOverlay.Map(static (in JsonElement value) => value.GetStringRequired());
        return new SkillBadge
        {
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Description = description.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
            IconUrl = new Uri(iconString),
            Overlay = overlayString,
            OverlayUrl = new Uri(overlayString),
            SmallOverlay = smallOverlayString,
            SmallOverlayUrl = new Uri(smallOverlayString),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            Tiers = tiers.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetSkillBadgeTier())
            )
        };
    }
}
