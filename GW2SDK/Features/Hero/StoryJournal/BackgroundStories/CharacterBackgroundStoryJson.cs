using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class CharacterBackgroundStoryJson
{
    public static CharacterBackgroundStory GetCharacterBackgroundStory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember backstory = "backstory";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == backstory.Name)
            {
                backstory = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterBackgroundStory
        {
            AnswerIds = backstory
                .Map(values => values.GetList(entry => entry.GetStringRequired()))
                .ToList()
        };
    }
}
