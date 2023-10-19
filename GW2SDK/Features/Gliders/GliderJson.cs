using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Gliders;

[PublicAPI]
public static class GliderJson
{
    public static Glider GetGlider(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        OptionalMember unlockItems = new("unlock_items");
        RequiredMember order = new("order");
        RequiredMember icon = new("icon");
        RequiredMember name = new("name");
        RequiredMember description = new("description");
        RequiredMember defaultDyes = new("default_dyes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(defaultDyes.Name))
            {
                defaultDyes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Glider
        {
            Id = id.Select(value => value.GetInt32()),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32()) ?? Array.Empty<int>(),
            Order = order.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            DefaultDyes = defaultDyes.SelectMany(entry => entry.GetInt32())
        };
    }
}
