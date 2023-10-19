using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class GuildUpgradeJson
{
    public static GuildUpgrade GetGuildUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "AccumulatingCurrency":
                return json.GetAccumulatingCurrency(missingMemberBehavior);
            case "BankBag":
                return json.GetBankBag(missingMemberBehavior);
            case "Boost":
                return json.GetBoost(missingMemberBehavior);
            case "Claimable":
                return json.GetClaimable(missingMemberBehavior);
            case "Consumable":
                return json.GetConsumable(missingMemberBehavior);
            case "Decoration":
                return json.GetDecoration(missingMemberBehavior);
            case "GuildHall":
                return json.GetGuildHall(missingMemberBehavior);
            case "GuildHallExpedition":
                return json.GetGuildHallExpedition(missingMemberBehavior);
            case "Hub":
                return json.GetHub(missingMemberBehavior);
            case "Queue":
                return json.GetQueue(missingMemberBehavior);
            case "Unlock":
                return json.GetUnlock(missingMemberBehavior);
        }

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
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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

        return new GuildUpgrade
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            BuildTime = buildTime.Map(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.Map(value => value.GetStringRequired()),
            RequiredLevel = requiredLevel.Map(value => value.GetInt32()),
            Experience = experience.Map(value => value.GetInt32()),
            Prerequisites = prerequisites.Map(values => values.GetList(value => value.GetInt32())),
            Costs = costs.Map(
                values => values.GetList(value => value.GetGuildUpgradeCost(missingMemberBehavior))
            )
        };
    }
}
