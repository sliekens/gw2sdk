using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

[PublicAPI]
public static class MasteryJson
{
    public static Mastery GetMastery(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember requirement = new("requirement");
        RequiredMember order = new("order");
        RequiredMember background = new("background");
        RequiredMember region = new("region");
        RequiredMember levels = new("levels");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(requirement.Name))
            {
                requirement = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(background.Name))
            {
                background = member;
            }
            else if (member.NameEquals(region.Name))
            {
                region = member;
            }
            else if (member.NameEquals(levels.Name))
            {
                levels = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mastery
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Requirement = requirement.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            Background = background.Select(value => value.GetStringRequired()),
            Region = region.Select(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior)),
            Levels = levels.SelectMany(value => value.GetMasteryLevel(missingMemberBehavior))
        };
    }
}
