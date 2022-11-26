using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Heroes;

[PublicAPI]
public static class HeroReader
{
    public static Hero GetHero(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> type = new("type");
        RequiredMember<HeroStats> stats = new("stats");
        RequiredMember<string> overlay = new("overlay");
        RequiredMember<string> underlay = new("underlay");
        RequiredMember<HeroSkin> skins = new("skins");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            Type = type.GetValue(),
            Stats = stats.Select(value => value.GetHeroStats(missingMemberBehavior)),
            Overlay = overlay.GetValue(),
            Underlay = underlay.GetValue(),
            Skins = skins.SelectMany(value => value.GetHeroSkin(missingMemberBehavior))
        };
    }
}
