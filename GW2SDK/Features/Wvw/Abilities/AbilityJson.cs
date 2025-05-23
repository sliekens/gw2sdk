using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

internal static class AbilityJson
{
    public static Ability GetAbility(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember ranks = "ranks";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
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
            else if (ranks.Match(member))
            {
                ranks = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Ability
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
#pragma warning disable CS0618 // IconHref is obsolete
            IconHref = icon.Map(static value => value.GetStringRequired()),
#pragma warning restore CS0618
            IconUrl = icon.Map(static value => new Uri(value.GetStringRequired())),
            Ranks = ranks.Map(static values =>
                values.GetList(static value => value.GetAbilityRank())
            )
        };
    }
}
