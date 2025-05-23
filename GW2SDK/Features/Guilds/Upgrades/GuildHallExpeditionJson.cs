using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildHallExpeditionJson
{
    public static GuildHallExpedition GetGuildHallExpedition(this JsonElement json)
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

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildHallExpedition"))
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

        return new GuildHallExpedition
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            BuildTime = buildTime.Map(static value => TimeSpan.FromMinutes(value.GetDouble())),
#pragma warning disable CS0618 // IconHref is obsolete
            IconHref = icon.Map(static value => value.GetStringRequired()),
#pragma warning restore CS0618
            IconUrl = icon.Map(static value => new Uri(value.GetStringRequired())),
            RequiredLevel = requiredLevel.Map(static value => value.GetInt32()),
            Experience = experience.Map(static value => value.GetInt32()),
            Prerequisites =
                prerequisites.Map(static values => values.GetList(static value => value.GetInt32())
                ),
            Costs = costs.Map(static values =>
                values.GetList(static value => value.GetGuildUpgradeCost())
            )
        };
    }
}
