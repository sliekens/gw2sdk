using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
public static class BankBagJson
{
    public static BankBag GetBankBag(
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
        RequiredMember<int> maxItems = new("bag_max_items");
        RequiredMember<int> maxCoins = new("bag_max_coins");
        RequiredMember<GuildUpgradeCost> costs = new("costs");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("BankBag"))
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
            else if (member.NameEquals(maxItems.Name))
            {
                maxItems.Value = member.Value;
            }
            else if (member.NameEquals(maxCoins.Name))
            {
                maxCoins.Value = member.Value;
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

        return new BankBag
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => value.GetGuildUpgradeCost(missingMemberBehavior)),
            MaxItems = maxItems.GetValue(),
            MaxCoins = maxCoins.GetValue()
        };
    }
}
