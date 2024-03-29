using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SkillBadgeJson
{
    public static SkillBadge GetSkillBadge(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SkillBadge
        {
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Overlay = overlay.Map(value => value.GetStringRequired()),
            SmallOverlay = smallOverlay.Map(value => value.GetStringRequired()),
            Tiers = tiers.Map(
                values => values.GetList(value => value.GetSkillBadgeTier(missingMemberBehavior))
            )
        };
    }
}
