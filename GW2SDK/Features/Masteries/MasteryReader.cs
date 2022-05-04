using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Masteries;

[PublicAPI]
public static class MasteryReader
{
    public static Mastery GetMastery(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> requirement = new("requirement");
        RequiredMember<int> order = new("order");
        RequiredMember<string> background = new("background");
        RequiredMember<MasteryRegionName> region = new("region");
        RequiredMember<MasteryLevel> levels = new("levels");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(requirement.Name))
            {
                requirement.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(background.Name))
            {
                background.Value = member.Value;
            }
            else if (member.NameEquals(region.Name))
            {
                region.Value = member.Value;
            }
            else if (member.NameEquals(levels.Name))
            {
                levels.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mastery
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Requirement = requirement.GetValue(),
            Order = order.GetValue(),
            Background = background.GetValue(),
            Region = region.GetValue(missingMemberBehavior),
            Levels = levels.SelectMany(value => ReadLevel(value, missingMemberBehavior))
        };
    }

    private static MasteryLevel ReadLevel(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> instruction = new("instruction");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> pointCost = new("point_cost");
        RequiredMember<int> experienceCost = new("exp_cost");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(instruction.Name))
            {
                instruction.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(pointCost.Name))
            {
                pointCost.Value = member.Value;
            }
            else if (member.NameEquals(experienceCost.Name))
            {
                experienceCost.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryLevel
        {
            Name = name.GetValue(),
            Description = description.GetValue(),
            Instruction = instruction.GetValue(),
            Icon = icon.GetValue(),
            PointCost = pointCost.GetValue(),
            ExperienceCost = experienceCost.GetValue()
        };
    }
}
