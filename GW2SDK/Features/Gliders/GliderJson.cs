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
        RequiredMember<int> id = new("id");
        OptionalMember<int> unlockItems = new("unlock_items");
        RequiredMember<int> order = new("order");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<int> defaultDyes = new("default_dyes");

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
            Id = id.GetValue(),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32()) ?? Array.Empty<int>(),
            Order = order.GetValue(),
            Icon = icon.GetValue(),
            Name = name.GetValue(),
            Description = description.GetValue(),
            DefaultDyes = defaultDyes.SelectMany(entry => entry.GetInt32())
        };
    }
}
