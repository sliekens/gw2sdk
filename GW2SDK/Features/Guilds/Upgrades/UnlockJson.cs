﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class UnlockJson
{
    public static Unlock GetUnlock(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Unlock"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Unlock
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            BuildTime = buildTime.Map(value => TimeSpan.FromMinutes(value.GetDouble())),
            IconHref = icon.Map(value => value.GetStringRequired()),
            RequiredLevel = requiredLevel.Map(value => value.GetInt32()),
            Experience = experience.Map(value => value.GetInt32()),
            Prerequisites = prerequisites.Map(values => values.GetList(value => value.GetInt32())),
            Costs = costs.Map(
                values => values.GetList(value => value.GetGuildUpgradeCost(missingMemberBehavior))
            )
        };
    }
}
