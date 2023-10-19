using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

[PublicAPI]
public static class MasteryLevelJson
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
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Instruction = instruction.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            PointCost = pointCost.Select(value => value.GetInt32()),
            ExperienceCost = experienceCost.Select(value => value.GetInt32())
        };
    }
}
