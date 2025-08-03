using System.Text.Json;

using GuildWars2.Guilds.Emblems;
using GuildWars2.Json;

namespace GuildWars2.Guilds;

internal static class GuildJson
{
    public static Guild GetGuild(this in JsonElement json)
    {
        NullableMember level = "level";
        OptionalMember motd = "motd";
        NullableMember influence = "influence";
        NullableMember aetherium = "aetherium";
        NullableMember resonance = "resonance";
        NullableMember favor = "favor";
        NullableMember memberCount = "member_count";
        NullableMember memberCapacity = "member_capacity";
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember tag = "tag";
        RequiredMember emblem = "emblem";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Guild
        {
            Level = level.Map(static (in JsonElement value) => value.GetInt32()),
            MessageOfTheDay = motd.Map(static (in JsonElement value) => value.GetStringRequired()),
            Influence = influence.Map(static (in JsonElement value) => value.GetInt32()),
            Aetherium = aetherium.Map(static (in JsonElement value) => value.GetInt32()),
            Resonance = resonance.Map(static (in JsonElement value) => value.GetInt32()),
            Favor = favor.Map(static (in JsonElement value) => value.GetInt32()),
            MemberCount = memberCount.Map(static (in JsonElement value) => value.GetInt32()),
            MemberCapacity = memberCapacity.Map(static (in JsonElement value) => value.GetInt32()),
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Tag = tag.Map(static (in JsonElement value) => value.GetStringRequired()),
            Emblem = emblem.Map(static (in JsonElement value) => value.GetGuildEmblem())
        };
    }
}
