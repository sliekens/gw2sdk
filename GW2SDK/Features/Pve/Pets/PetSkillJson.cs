using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Pets;

internal static class PetSkillJson
{
    public static PetSkill GetPetSkill(this JsonElement json)
    {
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new PetSkill { Id = id.Map(static value => value.GetInt32()) };
    }
}
