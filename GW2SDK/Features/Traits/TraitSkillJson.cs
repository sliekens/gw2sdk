using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Traits;

[PublicAPI]
public static class TraitSkillJson
{
    public static TraitSkill GetTraitSkill(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<TraitFact> facts = new("facts");
        OptionalMember<CompoundTraitFact> traitedFacts = new("traited_facts");
        RequiredMember<string> description = new("description");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        OptionalMember<SkillCategoryName> categories = new("categories");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(facts.Name))
            {
                facts.Value = member.Value;
            }
            else if (member.NameEquals(traitedFacts.Name))
            {
                traitedFacts.Value = member.Value;
            }
            else if (member.NameEquals(description.Name))
            {
                description.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
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
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (member.NameEquals(categories.Name))
            {
                categories.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TraitSkill
        {
            Name = name.GetValue(),
            Facts = facts.SelectMany(
                item => item.GetTraitFact(missingMemberBehavior, out _, out _)
            ),
            TraitedFacts =
                traitedFacts.SelectMany(value => value.GetCompoundTraitFact(missingMemberBehavior)),
            Description = description.GetValue(),
            Icon = icon.GetValue(),
            Id = id.GetValue(),
            ChatLink = chatLink.GetValue(),
            Categories = categories.GetValues(missingMemberBehavior)
        };
    }
}
