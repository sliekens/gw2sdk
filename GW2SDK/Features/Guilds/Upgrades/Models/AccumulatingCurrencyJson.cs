using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class AccumulatingCurrencyJson
{
    public static AccumulatingCurrency GetAccumulatingCurrency(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember buildTime = new("build_time");
        RequiredMember icon = new("icon");
        RequiredMember requiredLevel = new("required_level");
        RequiredMember experience = new("experience");
        RequiredMember prerequisites = new("prerequisites");
        RequiredMember costs = new("costs");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("AccumulatingCurrency"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
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
            else if (member.NameEquals(buildTime.Name))
            {
                buildTime.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(requiredLevel.Name))
            {
                requiredLevel.Value = member.Value;
            }
            else if (member.NameEquals(experience.Name))
            {
                experience.Value = member.Value;
            }
            else if (member.NameEquals(prerequisites.Name))
            {
                prerequisites.Value = member.Value;
            }
            else if (member.NameEquals(costs.Name))
            {
                costs.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccumulatingCurrency
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.Select(value => value.GetStringRequired()),
            RequiredLevel = requiredLevel.Select(value => value.GetInt32()),
            Experience = experience.Select(value => value.GetInt32()),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => value.GetGuildUpgradeCost(missingMemberBehavior))
        };
    }
}
