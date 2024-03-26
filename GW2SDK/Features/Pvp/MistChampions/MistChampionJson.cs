using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionJson
{
    public static MistChampion GetMistChampion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (description.Match(member))
            {
                description = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (stats.Match(member))
            {
                stats = member;
            }
            else if (overlay.Match(member))
            {
                overlay = member;
            }
            else if (underlay.Match(member))
            {
                underlay = member;
            }
            else if (skins.Match(member))
            {
                skins = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MistChampion
        {
            Id = id.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Type = type.Map(value => value.GetStringRequired()),
            Stats = stats.Map(value => value.GetMistChampionStats(missingMemberBehavior)),
            OverlayImageHref = overlay.Map(value => value.GetStringRequired()),
            UnderlayImageHref = underlay.Map(value => value.GetStringRequired()),
            Skins = skins.Map(
                values => values.GetList(value => value.GetMistChampionSkin(missingMemberBehavior))
            )
        };
    }
}
