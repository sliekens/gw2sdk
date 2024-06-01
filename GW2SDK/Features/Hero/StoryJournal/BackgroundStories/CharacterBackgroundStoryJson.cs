using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

internal static class CharacterBackgroundStoryJson
{
    public static CharacterBackgroundStory GetCharacterBackgroundStory(this JsonElement json)
    {
        RequiredMember backstory = "backstory";

        foreach (var member in json.EnumerateObject())
        {
            if (backstory.Match(member))
            {
                backstory = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CharacterBackgroundStory
        {
            AnswerIds = backstory.Map(
                static values => values.GetList(static value => value.GetStringRequired())
            )
        };
    }
}
