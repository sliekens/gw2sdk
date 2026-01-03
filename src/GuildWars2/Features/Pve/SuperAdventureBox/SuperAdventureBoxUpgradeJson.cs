using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.SuperAdventureBox;

internal static class SuperAdventureBoxUpgradeJson
{
    public static SuperAdventureBoxUpgrade GetSuperAdventureBoxUpgrade(this in JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember name = "name";

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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SuperAdventureBoxUpgrade
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetString()) ?? ""
        };
    }
}
