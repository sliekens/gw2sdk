using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Heroes;

[PublicAPI]
public static class HeroJson
{
    public static Hero GetHero(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember type = new("type");
        RequiredMember stats = new("stats");
        RequiredMember overlay = new("overlay");
        RequiredMember underlay = new("underlay");
        RequiredMember skins = new("skins");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(stats.Name))
            {
                stats.Value = member.Value;
            }
            else if (member.NameEquals(overlay.Name))
            {
                overlay.Value = member.Value;
            }
            else if (member.NameEquals(underlay.Name))
            {
                underlay.Value = member.Value;
            }
            else if (member.NameEquals(skins.Name))
            {
                skins.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Hero
        {
            Id = id.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            Type = type.Select(value => value.GetStringRequired()),
            Stats = stats.Select(value => value.GetHeroStats(missingMemberBehavior)),
            Overlay = overlay.Select(value => value.GetStringRequired()),
            Underlay = underlay.Select(value => value.GetStringRequired()),
            Skins = skins.SelectMany(value => value.GetHeroSkin(missingMemberBehavior))
        };
    }
}
