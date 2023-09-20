using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class GuildHallExpeditionJson
{
    public static GuildHallExpedition GetGuildHallExpedition(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<TimeSpan> buildTime = new("build_time");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> requiredLevel = new("required_level");
        RequiredMember<int> experience = new("experience");
        RequiredMember<int> prerequisites = new("prerequisites");
        RequiredMember<GuildUpgradeCost> costs = new("costs");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildHallExpedition"))
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

        return new GuildHallExpedition
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => value.GetGuildUpgradeCost(missingMemberBehavior))
        };
    }
}
