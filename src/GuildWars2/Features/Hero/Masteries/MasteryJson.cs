using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryJson
{
    public static Mastery GetMastery(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember instruction = "instruction";
        RequiredMember icon = "icon";
        RequiredMember pointCost = "point_cost";
        RequiredMember experienceCost = "exp_cost";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Mastery
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            Instruction = instruction.Map(static (in value) => value.GetStringRequired()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            PointCost = pointCost.Map(static (in value) => value.GetInt32()),
            ExperienceCost = experienceCost.Map(static (in value) => value.GetInt32())
        };
    }
}
