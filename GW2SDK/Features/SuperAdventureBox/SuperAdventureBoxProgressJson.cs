using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public static class SuperAdventureBoxProgressJson
{
    public static SuperAdventureBoxProgress GetSuperAdventureBoxProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<SuperAdventureBoxZone> zones = new("zones");
        RequiredMember<SuperAdventureBoxUpgrade> unlocks = new("unlocks");
        RequiredMember<SuperAdventureBoxSong> songs = new("songs");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(zones.Name))
            {
                zones.Value = member.Value;
            }
            else if (member.NameEquals(unlocks.Name))
            {
                unlocks.Value = member.Value;
            }
            else if (member.NameEquals(songs.Name))
            {
                songs.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SuperAdventureBoxProgress
        {
            Zones =
                zones.SelectMany(
                    entry => entry.GetSuperAdventureBoxZone(missingMemberBehavior)
                ),
            Unlocks =
                unlocks.SelectMany(
                    entry => entry.GetSuperAdventureBoxUpgrade(missingMemberBehavior)
                ),
            Songs = songs.SelectMany(
                entry => entry.GetSuperAdventureBoxSong(missingMemberBehavior)
            )
        };
    }
}
