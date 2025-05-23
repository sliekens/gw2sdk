using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Decorations;

internal static class DecorationJson
{
    public static Decoration GetDecoration(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember categories = "categories";
        RequiredMember maxCount = "max_count";
        RequiredMember icon = "icon";

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
            else if (categories.Match(member))
            {
                categories = member;
            }
            else if (maxCount.Match(member))
            {
                maxCount = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static value => value.GetStringRequired());
        return new Decoration
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Description = description.Map(static value => value.GetStringRequired()),
            CategoryIds = categories.Map(static value => value.GetList(item => item.GetInt32())),
            MaxCount = maxCount.Map(static value => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute)
        };
    }
}
