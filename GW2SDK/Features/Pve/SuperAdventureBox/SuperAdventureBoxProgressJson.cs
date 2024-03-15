using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.SuperAdventureBox;

internal static class SuperAdventureBoxProgressJson
{
    public static SuperAdventureBoxProgress GetSuperAdventureBoxProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember zones = "zones";
        RequiredMember unlocks = "unlocks";
        RequiredMember songs = "songs";

        foreach (var member in json.EnumerateObject())
        {
            if (zones.Match(member))
            {
                zones = member;
            }
            else if (unlocks.Match(member))
            {
                unlocks = member;
            }
            else if (songs.Match(member))
            {
                songs = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SuperAdventureBoxProgress
        {
            Zones =
                zones.Map(
                    values =>
                        values.GetList(
                            entry => entry.GetSuperAdventureBoxZone(missingMemberBehavior)
                        )
                ),
            Unlocks =
                unlocks.Map(
                    values =>
                        values.GetList(
                            entry => entry.GetSuperAdventureBoxUpgrade(missingMemberBehavior)
                        )
                ),
            Songs = songs.Map(
                values => values.GetList(
                    entry => entry.GetSuperAdventureBoxSong(missingMemberBehavior)
                )
            )
        };
    }
}
