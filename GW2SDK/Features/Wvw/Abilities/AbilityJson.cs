using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
public static class AbilityJson
{
    public static Ability GetAbility(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> icon = new("icon");
        RequiredMember<Rank> ranks = new("ranks");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
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
            else if (member.NameEquals(ranks.Name))
            {
                ranks.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ability
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Icon = icon.GetValue(),
            Ranks = ranks.SelectMany(value => value.GetRank(missingMemberBehavior))
        };
    }
}
