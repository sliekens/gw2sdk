using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Traits;

[PublicAPI]
public static class TraitSkillJson
{
    public static TraitSkill GetTraitSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember facts = new("facts");
        OptionalMember traitedFacts = new("traited_facts");
        RequiredMember description = new("description");
        RequiredMember icon = new("icon");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        OptionalMember categories = new("categories");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts = member;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals("flags"))
            {
                // This seems to be always empty, just ignore it until one day it isn't
                if (missingMemberBehavior == MissingMemberBehavior.Error
                    && member.Value.GetArrayLength() != 0)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitSkill
        {
            Name = name.Select(value => value.GetStringRequired()),
            Facts = facts.SelectMany(
                item => item.GetTraitFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetCompoundTraitFact(missingMemberBehavior)),
            Description = description.Select(value => value.GetStringRequired()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Id = id.Select(value => value.GetInt32()),
            ChatLink = chatLink.Select(value => value.GetStringRequired()),
            Categories = categories.SelectMany(value => value.GetEnum<SkillCategoryName>(missingMemberBehavior))
        };
    }
}
