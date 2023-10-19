using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

[PublicAPI]
public static class GuildJson
{
    public static Guild GetGuild(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember level = new("level");
        RequiredMember motd = new("motd");
        RequiredMember influence = new("influence");
        RequiredMember aetherium = new("aetherium");
        RequiredMember resonance = new("resonance");
        RequiredMember favor = new("favor");
        RequiredMember memberCount = new("member_count");
        RequiredMember memberCapacity = new("member_capacity");
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember tag = new("tag");
        RequiredMember emblem = new("emblem");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(motd.Name))
            {
                motd = member;
            }
            else if (member.NameEquals(influence.Name))
            {
                influence = member;
            }
            else if (member.NameEquals(aetherium.Name))
            {
                aetherium = member;
            }
            else if (member.NameEquals(resonance.Name))
            {
                resonance = member;
            }
            else if (member.NameEquals(favor.Name))
            {
                favor = member;
            }
            else if (member.NameEquals(memberCount.Name))
            {
                memberCount = member;
            }
            else if (member.NameEquals(memberCapacity.Name))
            {
                memberCapacity = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(tag.Name))
            {
                tag = member;
            }
            else if (member.NameEquals(emblem.Name))
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
            Level = level.Select(value => value.GetInt32()),
            MessageOfTheDay = motd.Select(value => value.GetStringRequired()),
            Influence = influence.Select(value => value.GetInt32()),
            Aetherium = aetherium.Select(value => value.GetInt32()),
            Resonance = resonance.Select(value => value.GetInt32()),
            Favor = favor.Select(value => value.GetInt32()),
            MemberCount = memberCount.Select(value => value.GetInt32()),
            MemberCapacity = memberCapacity.Select(value => value.GetInt32()),
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Tag = tag.Select(value => value.GetStringRequired()),
            Emblem = emblem.Select(value => value.GetGuildEmblem(missingMemberBehavior))
        };
    }
}
