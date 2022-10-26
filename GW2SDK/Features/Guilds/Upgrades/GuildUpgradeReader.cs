using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
public static class GuildUpgradeReader
{
    public static GuildUpgrade GetGuildUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "AccumulatingCurrency":
                return ReadAccumulatingCurrency(json, missingMemberBehavior);
            case "BankBag":
                return ReadBankBag(json, missingMemberBehavior);
            case "Boost":
                return ReadBoost(json, missingMemberBehavior);
            case "Claimable":
                return ReadClaimable(json, missingMemberBehavior);
            case "Consumable":
                return ReadConsumable(json, missingMemberBehavior);
            case "Decoration":
                return ReadDecoration(json, missingMemberBehavior);
            case "GuildHall":
                return ReadGuildHall(json, missingMemberBehavior);
            case "GuildHallExpedition":
                return ReadGuildHallExpedition(json, missingMemberBehavior);
            case "Hub":
                return ReadHub(json, missingMemberBehavior);
            case "Queue":
                return ReadQueue(json, missingMemberBehavior);
            case "Unlock":
                return ReadUnlock(json, missingMemberBehavior);
        }

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
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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

        return new GuildUpgrade
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static AccumulatingCurrency ReadAccumulatingCurrency(
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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static BankBag ReadBankBag(
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
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior)),
            MaxItems = maxItems.GetValue(),
            MaxCoins = maxCoins.GetValue()
        };
    }

    public static Boost ReadBoost(
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
                if (!member.Value.ValueEquals("Boost"))
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

        return new Boost
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Claimable ReadClaimable(
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
                if (!member.Value.ValueEquals("Claimable"))
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

        return new Claimable
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Consumable ReadConsumable(
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
                if (!member.Value.ValueEquals("Consumable"))
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

        return new Consumable
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Decoration ReadDecoration(
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
                if (!member.Value.ValueEquals("Decoration"))
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

        return new Decoration
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static GuildHall ReadGuildHall(
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
                if (!member.Value.ValueEquals("GuildHall"))
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

        return new GuildHall
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static GuildHallExpedition ReadGuildHallExpedition(
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
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Hub ReadHub(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
                if (!member.Value.ValueEquals("Hub"))
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

        return new Hub
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Queue ReadQueue(
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
                if (!member.Value.ValueEquals("Queue"))
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

        return new Queue
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    public static Unlock ReadUnlock(
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
                if (!member.Value.ValueEquals("Unlock"))
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

        return new Unlock
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            BuildTime = buildTime.Select(value => TimeSpan.FromMinutes(value.GetDouble())),
            Icon = icon.GetValue(),
            RequiredLevel = requiredLevel.GetValue(),
            Experience = experience.GetValue(),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Costs = costs.SelectMany(value => ReadGuildUpgradeCost(value, missingMemberBehavior))
        };
    }

    private static GuildUpgradeCost ReadGuildUpgradeCost(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<GuildUpgradeCostKind> kind = new("type");
        OptionalMember<string> name = new("name");
        RequiredMember<int> count = new("count");
        NullableMember<int> itemId = new("item_id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(kind.Name))
            {
                kind.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCost
        {
            Kind = kind.GetValue(missingMemberBehavior),
            Name = name.GetValueOrEmpty(),
            Count = count.GetValue(),
            ItemId = itemId.GetValue()
        };
    }
}
