using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryTrackJson
{
    public static MasteryTrack GetMasteryTrack(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember requirement = "requirement";
        RequiredMember order = "order";
        RequiredMember background = "background";
        RequiredMember region = "region";
        RequiredMember levels = "levels";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == requirement.Name)
            {
                requirement = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == background.Name)
            {
                background = member;
            }
            else if (member.Name == region.Name)
            {
                region = member;
            }
            else if (member.Name == levels.Name)
            {
                levels = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryTrack
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Requirement = requirement.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            BackgroundHref = background.Map(value => value.GetStringRequired()),
            Region = region.Map(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior)),
            Masteries = levels.Map(
                values => values.GetList(value => value.GetMastery(missingMemberBehavior))
            )
        };
    }
}
