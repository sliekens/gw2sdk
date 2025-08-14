using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class CharacterTrainingJson
{
    public static CharacterTraining GetCharacterTraining(this in JsonElement json)
    {
        RequiredMember training = "training";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (training.Match(member))
            {
                training = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CharacterTraining
        {
            Training = training.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetTrainingProgress())
            )
        };
    }
}
