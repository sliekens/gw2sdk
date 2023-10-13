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
        RequiredMember<int> level = new("level");
        RequiredMember<string> motd = new("motd");
        RequiredMember<int> influence = new("influence");
        RequiredMember<int> aetherium = new("aetherium");
        RequiredMember<int> resonance = new("resonance");
        RequiredMember<int> favor = new("favor");
        RequiredMember<int> memberCount = new("member_count");
        RequiredMember<int> memberCapacity = new("member_capacity");
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> tag = new("tag");
        RequiredMember<GuildEmblem> emblem = new("emblem");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(motd.Name))
            {
                motd.Value = member.Value;
            }
            else if (member.NameEquals(influence.Name))
            {
                influence.Value = member.Value;
            }
            else if (member.NameEquals(aetherium.Name))
            {
                aetherium.Value = member.Value;
            }
            else if (member.NameEquals(resonance.Name))
            {
                resonance.Value = member.Value;
            }
            else if (member.NameEquals(favor.Name))
            {
                favor.Value = member.Value;
            }
            else if (member.NameEquals(memberCount.Name))
            {
                memberCount.Value = member.Value;
            }
            else if (member.NameEquals(memberCapacity.Name))
            {
                memberCapacity.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(tag.Name))
            {
                tag.Value = member.Value;
            }
            else if (member.NameEquals(emblem.Name))
            {
                emblem.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Guild
        {
            Level = level.GetValue(),
            MessageOfTheDay = motd.GetValue(),
            Influence = influence.GetValue(),
            Aetherium = aetherium.GetValue(),
            Resonance = resonance.GetValue(),
            Favor = favor.GetValue(),
            MemberCount = memberCount.GetValue(),
            MemberCapacity = memberCapacity.GetValue(),
            Id = id.GetValue(),
            Name = name.GetValue(),
            Tag = tag.GetValue(),
            Emblem = emblem.Select(value => value.GetGuildEmblem(missingMemberBehavior))
        };
    }
}
