using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.SuperAdventureBox;

internal static class SuperAdventureBoxUpgradeJson
{
    public static SuperAdventureBoxUpgrade GetSuperAdventureBoxUpgrade(this JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember name = "name";

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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SuperAdventureBoxUpgrade
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetString()) ?? ""
        };
    }
}
