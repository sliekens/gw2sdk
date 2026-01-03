using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Abilities;

internal static class AbilityJson
{
    public static Ability GetAbility(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";
        RequiredMember ranks = "ranks";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            IconUrl = icon.Map(static (in value) => new Uri(value.GetStringRequired())),
            Ranks = ranks.Map(static (in values) =>
                values.GetList(static (in value) => value.GetAbilityRank())
            )
        };
    }
}
