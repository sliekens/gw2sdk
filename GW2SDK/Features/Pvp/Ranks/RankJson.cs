﻿using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

internal static class RankJson
{
    public static Rank GetRank(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember finisherId = "finisher_id";
        RequiredMember name = "name";
        RequiredMember icon = "icon";
        RequiredMember minRank = "min_rank";
        RequiredMember maxRank = "max_rank";
        RequiredMember levels = "levels";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (finisherId.Match(member))
            {
                finisherId = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (minRank.Match(member))
            {
                minRank = member;
            }
            else if (maxRank.Match(member))
            {
                maxRank = member;
            }
            else if (levels.Match(member))
            {
                levels = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        return new Rank
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            FinisherId = finisherId.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString),
            MinRank = minRank.Map(static (in JsonElement value) => value.GetInt32()),
            MaxRank = maxRank.Map(static (in JsonElement value) => value.GetInt32()),
            Levels = levels.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetLevel()))
        };
    }
}
