using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class QuestJson
{
    public static Quest GetQuest(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = "name";
        RequiredMember level = "level";
        RequiredMember story = "story";
        RequiredMember goals = "goals";
        RequiredMember id = "id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == level.Name)
            {
                level = member;
            }
            else if (member.Name == story.Name)
            {
                story = member;
            }
            else if (member.Name == goals.Name)
            {
                goals = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Quest
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Level = level.Map(value => value.GetInt32()),
            Story = story.Map(value => value.GetInt32()),
            Goals = goals.Map(
                values => values.GetList(value => value.GetGoal(missingMemberBehavior))
            )
        };
    }
}
