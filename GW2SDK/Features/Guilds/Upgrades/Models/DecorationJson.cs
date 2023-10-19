using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class DecorationJson
{
    public static Decoration GetDecoration(
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
                if (!member.Value.ValueEquals("Decoration"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(buildTime.Name))
            {
                buildTime = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(requiredLevel.Name))
            {
                requiredLevel = member;
            }
            else if (member.NameEquals(experience.Name))
            {
                experience = member;
            }
            else if (member.NameEquals(prerequisites.Name))
            {
                prerequisites = member;
            }
            else if (member.NameEquals(costs.Name))
            {
                costs = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Decoration
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
