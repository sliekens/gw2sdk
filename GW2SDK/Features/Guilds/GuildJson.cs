using System.Text.Json;
using GuildWars2.Guilds.Emblems;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

internal static class GuildJson
{
    public static Guild GetGuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember level = "level";
        RequiredMember motd = "motd";
        RequiredMember influence = "influence";
        RequiredMember aetherium = "aetherium";
        RequiredMember resonance = "resonance";
        RequiredMember favor = "favor";
        RequiredMember memberCount = "member_count";
        RequiredMember memberCapacity = "member_capacity";
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember tag = "tag";
        RequiredMember emblem = "emblem";

        foreach (var member in json.EnumerateObject())
        {
            if (level.Match(member))
            {
                level = member;
            }
            else if (motd.Match(member))
            {
                motd = member;
            }
            else if (influence.Match(member))
            {
                influence = member;
            }
            else if (aetherium.Match(member))
            {
                aetherium = member;
            }
            else if (resonance.Match(member))
            {
                resonance = member;
            }
            else if (favor.Match(member))
            {
                favor = member;
            }
            else if (memberCount.Match(member))
            {
                memberCount = member;
            }
            else if (memberCapacity.Match(member))
            {
                memberCapacity = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (tag.Match(member))
            {
                tag = member;
            }
            else if (emblem.Match(member))
            {
                emblem = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Guild
        {
            Level = level.Map(value => value.GetInt32()),
            MessageOfTheDay = motd.Map(value => value.GetStringRequired()),
            Influence = influence.Map(value => value.GetInt32()),
            Aetherium = aetherium.Map(value => value.GetInt32()),
            Resonance = resonance.Map(value => value.GetInt32()),
            Favor = favor.Map(value => value.GetInt32()),
            MemberCount = memberCount.Map(value => value.GetInt32()),
            MemberCapacity = memberCapacity.Map(value => value.GetInt32()),
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Tag = tag.Map(value => value.GetStringRequired()),
            Emblem = emblem.Map(value => value.GetGuildEmblem(missingMemberBehavior))
        };
    }
}
