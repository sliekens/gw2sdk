using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
public static class RankJson
{
    public static Rank GetRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember finisherId = new("finisher_id");
        RequiredMember name = new("name");
        RequiredMember icon = new("icon");
        RequiredMember minRank = new("min_rank");
        RequiredMember maxRank = new("max_rank");
        RequiredMember levels = new("levels");

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
            Id = id.Select(value => value.GetInt32()),
            FinisherId = finisherId.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            MinRank = minRank.Select(value => value.GetInt32()),
            MaxRank = maxRank.Select(value => value.GetInt32()),
            Levels = levels.SelectMany(value => value.GetLevel(missingMemberBehavior))
        };
    }
}
