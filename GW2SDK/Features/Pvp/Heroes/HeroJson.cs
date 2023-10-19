using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroJson
{
    public static Hero GetHero(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember type = "type";
        RequiredMember stats = "stats";
        RequiredMember overlay = "overlay";
        RequiredMember underlay = "underlay";
        RequiredMember skins = "skins";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(stats.Name))
            {
                stats = member;
            }
            else if (member.NameEquals(overlay.Name))
            {
                overlay = member;
            }
            else if (member.NameEquals(underlay.Name))
            {
                underlay = member;
            }
            else if (member.NameEquals(skins.Name))
            {
                skins = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Hero
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Type = type.Map(value => value.GetStringRequired()),
            Stats = stats.Map(value => value.GetHeroStats(missingMemberBehavior)),
            Overlay = overlay.Map(value => value.GetStringRequired()),
            Underlay = underlay.Map(value => value.GetStringRequired()),
            Skins = skins.Map(values => values.GetList(value => value.GetHeroSkin(missingMemberBehavior)))
        };
    }
}
