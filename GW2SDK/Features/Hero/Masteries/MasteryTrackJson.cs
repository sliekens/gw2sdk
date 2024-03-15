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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (requirement.Match(member))
            {
                requirement = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (background.Match(member))
            {
                background = member;
            }
            else if (region.Match(member))
            {
                region = member;
            }
            else if (levels.Match(member))
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
