using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class CharacterTrainingJson
{
    public static CharacterTraining GetCharacterTraining(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<TrainingProgress> training = new("training");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(training.Name))
            {
                training.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterTraining
        {
            Training = training.SelectMany(
                value => value.GetTrainingProgress(missingMemberBehavior)
            )
        };
    }
}
