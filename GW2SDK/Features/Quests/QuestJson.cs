using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Quests;

[PublicAPI]
public static class QuestJson
{
    public static Quest GetQuest(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = new("name");
        RequiredMember level = new("level");
        RequiredMember story = new("story");
        RequiredMember goals = new("goals");
        RequiredMember id = new("id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(story.Name))
            {
                story = member;
            }
            else if (member.NameEquals(goals.Name))
            {
                goals = member;
            }
            else if (member.NameEquals(id.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Level = level.Select(value => value.GetInt32()),
            Story = story.Select(value => value.GetInt32()),
            Goals = goals.SelectMany(value => value.GetGoal(missingMemberBehavior))
        };
    }
}
