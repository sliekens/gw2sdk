using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionJson
{
    public static MistChampion GetMistChampion(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == type.Name)
            {
                type = member;
            }
            else if (member.Name == stats.Name)
            {
                stats = member;
            }
            else if (member.Name == overlay.Name)
            {
                overlay = member;
            }
            else if (member.Name == underlay.Name)
            {
                underlay = member;
            }
            else if (member.Name == skins.Name)
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
            Overlay = overlay.Map(value => value.GetStringRequired()),
            Underlay = underlay.Map(value => value.GetStringRequired()),
            Skins = skins.Map(
                values => values.GetList(value => value.GetMistChampionSkin(missingMemberBehavior))
            )
        };
    }
}
