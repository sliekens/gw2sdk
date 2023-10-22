using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

internal static class MasteryLevelJson
{
    public static MasteryLevel GetMasteryLevel(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember instruction = "instruction";
        RequiredMember icon = "icon";
        RequiredMember pointCost = "point_cost";
        RequiredMember experienceCost = "exp_cost";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(instruction.Name))
            {
                instruction = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(pointCost.Name))
            {
                pointCost = member;
            }
            else if (member.NameEquals(experienceCost.Name))
            {
                experienceCost = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryLevel
        {
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Instruction = instruction.Map(value => value.GetStringRequired()),
            Icon = icon.Map(value => value.GetStringRequired()),
            PointCost = pointCost.Map(value => value.GetInt32()),
            ExperienceCost = experienceCost.Map(value => value.GetInt32())
        };
    }
}
