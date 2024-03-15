using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryJson
{
    public static Mastery GetMastery(
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
            if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (instruction.Match(member))
            {
                instruction = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (pointCost.Match(member))
            {
                pointCost = member;
            }
            else if (experienceCost.Match(member))
            {
                experienceCost = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mastery
        {
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Instruction = instruction.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            PointCost = pointCost.Map(value => value.GetInt32()),
            ExperienceCost = experienceCost.Map(value => value.GetInt32())
        };
    }
}
