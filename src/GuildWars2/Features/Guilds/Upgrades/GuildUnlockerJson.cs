using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUnlockerJson
{
    public static GuildUnlocker GetGuildUnlocker(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember buildTime = "build_time";
        RequiredMember icon = "icon";
        RequiredMember requiredLevel = "required_level";
        RequiredMember experience = "experience";
        RequiredMember prerequisites = "prerequisites";
        RequiredMember costs = "costs";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Unlock"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
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
            else if (buildTime.Match(member))
            {
                buildTime = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (requiredLevel.Match(member))
            {
                requiredLevel = member;
            }
            else if (experience.Match(member))
            {
                experience = member;
            }
            else if (prerequisites.Match(member))
            {
                prerequisites = member;
            }
            else if (costs.Match(member))
            {
                costs = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildUnlocker
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            BuildTime = buildTime.Map(static (in value) => TimeSpan.FromMinutes(value.GetDouble())),
            IconUrl = icon.Map(static (in value) => new Uri(value.GetStringRequired())),
            RequiredLevel = requiredLevel.Map(static (in value) => value.GetInt32()),
            Experience = experience.Map(static (in value) => value.GetInt32()),
            Prerequisites =
                prerequisites.Map(static (in values) => values.GetList(static (in value) => value.GetInt32())
                ),
            Costs = costs.Map(static (in values) =>
                values.GetList(static (in value) => value.GetGuildUpgradeCost())
            )
        };
    }
}
