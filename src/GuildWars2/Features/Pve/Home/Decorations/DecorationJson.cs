using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Decorations;

internal static class DecorationJson
{
    public static Decoration GetDecoration(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember categories = "categories";
        RequiredMember maxCount = "max_count";
        RequiredMember icon = "icon";

        foreach (JsonProperty member in json.EnumerateObject())
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

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Decoration
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            CategoryIds = categories.Map(static (in value) => value.GetList(static (in item) => item.GetInt32())),
            MaxCount = maxCount.Map(static (in value) => value.GetInt32()),
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute)
        };
    }
}
