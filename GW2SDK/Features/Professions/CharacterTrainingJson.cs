using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

internal static class CharacterTrainingJson
{
    public static CharacterTraining GetCharacterTraining(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember training = "training";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(training.Name))
            {
                training = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterTraining
        {
            Training = training.Map(
                values => values.GetList(
                    value => value.GetTrainingProgress(missingMemberBehavior)
                )
            )
        };
    }
}
