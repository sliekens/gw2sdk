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
            Levels = levels.SelectMany(value => value.GetMasteryLevel(missingMemberBehavior))
        };
    }
}
