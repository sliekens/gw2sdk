using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class CharacterTrainingJson
{
    public static CharacterTraining GetCharacterTraining(this JsonElement json)
    {
        RequiredMember training = "training";

        foreach (var member in json.EnumerateObject())
        {
            if (training.Match(member))
            {
                training = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterTraining
        {
            Training = training.Map(
                static values => values.GetList(static value => value.GetTrainingProgress())
            )
        };
    }
}
