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
        RequiredMember name = "name";
        RequiredMember flags = "flags";
        RequiredMember largeIcon = "large_icon";
        RequiredMember smallIcon = "small_icon";
        RequiredMember pipIcon = "pip_icon";
        RequiredMember tiers = "tiers";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(largeIcon.Name))
            {
                largeIcon = member;
            }
            else if (member.NameEquals(smallIcon.Name))
            {
                smallIcon = member;
            }
            else if (member.NameEquals(pipIcon.Name))
            {
                pipIcon = member;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Division
        {
            Name = name.Select(value => value.GetStringRequired()),
            Flags = flags.Select(values => values.GetList(value => value.GetEnum<DivisionFlag>(missingMemberBehavior))),
            LargeIcon = largeIcon.Select(value => value.GetStringRequired()),
            SmallIcon = smallIcon.Select(value => value.GetStringRequired()),
            PipIcon = pipIcon.Select(value => value.GetStringRequired()),
            Tiers = tiers.Select(values => values.GetList(value => value.GetDivisionTier(missingMemberBehavior)))
        };
    }
}
