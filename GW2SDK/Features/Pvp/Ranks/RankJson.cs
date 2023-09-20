using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public static class RankJson
{
    public static Rank GetRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> finisherId = new("finisher_id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> minRank = new("min_rank");
        RequiredMember<int> maxRank = new("max_rank");
        RequiredMember<Level> levels = new("levels");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(finisherId.Name))
            {
                finisherId.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(minRank.Name))
            {
                minRank.Value = member.Value;
            }
            else if (member.NameEquals(maxRank.Name))
            {
                maxRank.Value = member.Value;
            }
            else if (member.NameEquals(levels.Name))
            {
                levels.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Rank
        {
            Id = id.GetValue(),
            FinisherId = finisherId.GetValue(),
            Name = name.GetValue(),
            Icon = icon.GetValue(),
            MinRank = minRank.GetValue(),
            MaxRank = maxRank.GetValue(),
            Levels = levels.SelectMany(value => value.GetLevel(missingMemberBehavior))
        };
    }
}
