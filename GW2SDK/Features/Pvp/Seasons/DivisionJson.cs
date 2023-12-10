using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class DivisionJson
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
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == largeIcon.Name)
            {
                largeIcon = member;
            }
            else if (member.Name == smallIcon.Name)
            {
                smallIcon = member;
            }
            else if (member.Name == pipIcon.Name)
            {
                pipIcon = member;
            }
            else if (member.Name == tiers.Name)
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
            Name = name.Map(value => value.GetStringRequired()),
            Flags = flags.Map(values => values.GetDivisionFlags()),
            LargeIconHref = largeIcon.Map(value => value.GetStringRequired()),
            SmallIconHref = smallIcon.Map(value => value.GetStringRequired()),
            PipIconHref = pipIcon.Map(value => value.GetStringRequired()),
            Tiers = tiers.Map(
                values => values.GetList(value => value.GetDivisionTier(missingMemberBehavior))
            )
        };
    }
}
