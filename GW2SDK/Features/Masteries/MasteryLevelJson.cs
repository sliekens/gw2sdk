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
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember instruction = new("instruction");
        RequiredMember icon = new("icon");
        RequiredMember pointCost = new("point_cost");
        RequiredMember experienceCost = new("exp_cost");

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
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Instruction = instruction.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            PointCost = pointCost.Select(value => value.GetInt32()),
            ExperienceCost = experienceCost.Select(value => value.GetInt32())
        };
    }
}
