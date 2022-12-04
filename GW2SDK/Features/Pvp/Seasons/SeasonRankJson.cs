using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class SeasonRankJson
{
    public static SeasonRank GetSeasonRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> overlay = new("overlay");
        RequiredMember<string> smallOverlay = new("overlay_small");
        RequiredMember<RankTier> tiers = new("tiers");

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
            Name = name.GetValue(),
            Description = description.GetValue(),
            Icon = icon.GetValue(),
            Overlay = overlay.GetValue(),
            SmallOverlay = smallOverlay.GetValue(),
            Tiers = tiers.SelectMany(value => value.GetRankTier(missingMemberBehavior))
        };
    }
}
