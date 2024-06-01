using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.SuperAdventureBox;

internal static class SuperAdventureBoxProgressJson
{
    public static SuperAdventureBoxProgress GetSuperAdventureBoxProgress(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SuperAdventureBoxProgress
        {
            Zones =
                zones.Map(
                    static values =>
                        values.GetList(static value => value.GetSuperAdventureBoxZone())
                ),
            Unlocks =
                unlocks.Map(
                    static values =>
                        values.GetList(static value => value.GetSuperAdventureBoxUpgrade())
                ),
            Songs = songs.Map(
                static values =>
                    values.GetList(static value => value.GetSuperAdventureBoxSong())
            )
        };
    }
}
