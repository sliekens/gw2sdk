using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class DivisionJson
{
    public static Division GetDivision(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<DivisionFlag> flags = new("flags");
        RequiredMember<string> largeIcon = new("large_icon");
        RequiredMember<string> smallIcon = new("small_icon");
        RequiredMember<string> pipIcon = new("pip_icon");
        RequiredMember<DivisionTier> tiers = new("tiers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(largeIcon.Name))
            {
                largeIcon.Value = member.Value;
            }
            else if (member.NameEquals(smallIcon.Name))
            {
                smallIcon.Value = member.Value;
            }
            else if (member.NameEquals(pipIcon.Name))
            {
                pipIcon.Value = member.Value;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Division
        {
            Name = name.GetValue(),
            Flags = flags.GetValues(missingMemberBehavior),
            LargeIcon = largeIcon.GetValue(),
            SmallIcon = smallIcon.GetValue(),
            PipIcon = pipIcon.GetValue(),
            Tiers = tiers.SelectMany(value => value.GetDivisionTier(missingMemberBehavior))
        };
    }
}
