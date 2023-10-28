using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Gliders;

internal static class GliderJson
{
    public static Glider GetGlider(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        OptionalMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember defaultDyes = "default_dyes";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == unlockItems.Name)
            {
                unlockItems = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == description.Name)
            {
                description = member;
            }
            else if (member.Name == defaultDyes.Name)
            {
                defaultDyes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Glider
        {
            Id = id.Map(value => value.GetInt32()),
            UnlockItems =
                unlockItems.Map(values => values.GetList(entry => entry.GetInt32()))
                ?? new List<int>(),
            Order = order.Map(value => value.GetInt32()),
            Icon = icon.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            DefaultDyes = defaultDyes.Map(values => values.GetList(entry => entry.GetInt32()))
        };
    }
}
