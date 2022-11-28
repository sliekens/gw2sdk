using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Masteries;

[PublicAPI]
public static class MasteryLevelJson
{
    public static MasteryLevel GetMasteryLevel(
        this JsonElement json,
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
