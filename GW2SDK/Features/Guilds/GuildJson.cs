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
            if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == motd.Name)
            {
                motd = member;
            }
            else if (member.Name == influence.Name)
            {
                influence = member;
            }
            else if (member.Name == aetherium.Name)
            {
                aetherium = member;
            }
            else if (member.Name == resonance.Name)
            {
                resonance = member;
            }
            else if (member.Name == favor.Name)
            {
                favor = member;
            }
            else if (member.Name == memberCount.Name)
            {
                memberCount = member;
            }
            else if (member.Name == memberCapacity.Name)
            {
                memberCapacity = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == tag.Name)
            {
                tag = member;
            }
            else if (member.Name == emblem.Name)
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
