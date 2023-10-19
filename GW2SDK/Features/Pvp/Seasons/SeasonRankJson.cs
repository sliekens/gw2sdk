using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class SeasonRankJson
{
    public static SeasonRank GetSeasonRank(
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
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(overlay.Name))
            {
                overlay = member;
            }
            else if (member.NameEquals(smallOverlay.Name))
            {
                smallOverlay = member;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SeasonRank
        {
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Overlay = overlay.Select(value => value.GetStringRequired()),
            SmallOverlay = smallOverlay.Select(value => value.GetStringRequired()),
            Tiers = tiers.Select(values => values.GetList(value => value.GetRankTier(missingMemberBehavior)))
        };
    }
}
