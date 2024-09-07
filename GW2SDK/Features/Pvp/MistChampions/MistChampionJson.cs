using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionJson
{
    public static MistChampion GetMistChampion(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MistChampion
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            Type = type.Map(static value => value.GetStringRequired()),
            Stats = stats.Map(static value => value.GetMistChampionStats()),
            OverlayImageHref = overlay.Map(static value => value.GetStringRequired()),
            UnderlayImageHref = underlay.Map(static value => value.GetStringRequired()),
            Skins = skins.Map(
                static values => values.GetList(static value => value.GetMistChampionSkin())
            )
        };
    }
}
