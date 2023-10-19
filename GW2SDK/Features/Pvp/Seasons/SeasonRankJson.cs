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
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember icon = new("icon");
        RequiredMember overlay = new("overlay");
        RequiredMember smallOverlay = new("overlay_small");
        RequiredMember tiers = new("tiers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(overlay.Name))
            {
                overlay.Value = member.Value;
            }
            else if (member.NameEquals(smallOverlay.Name))
            {
                smallOverlay.Value = member.Value;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers.Value = member.Value;
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
            Tiers = tiers.SelectMany(value => value.GetRankTier(missingMemberBehavior))
        };
    }
}
