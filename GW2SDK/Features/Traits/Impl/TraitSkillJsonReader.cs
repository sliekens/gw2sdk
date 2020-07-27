using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using JsonValueKind = System.Text.Json.JsonValueKind;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitSkillJsonReader : IJsonReader<TraitSkill>
    {
        private static readonly IJsonReader<TraitFact> TraitFactJsonReader = new TraitFactJsonReader();

        public TraitSkill Read(in JsonElement json)
        {
            string name = "";
            bool nameSeen = default;

            string description = "";
            bool descriptionSeen = default;

            SkillCategoryName[] categories = new SkillCategoryName[0];

            TraitFact[] facts = new TraitFact[0];
            bool factsSeen = default;

            TraitFact[] traitedFacts = new TraitFact[0];

            string icon = "";
            bool iconSeen = default;

            int id = default;
            bool idSeen = default;

            TraitSkillFlag[] flags = new TraitSkillFlag[0];
            bool flagsSeen = default;

            string chatLink = "";
            bool chatLinkSeen = default;

            var typeName = nameof(TraitSkill);
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("name"))
                {
                    nameSeen = true;
                    name = member.Value.GetString();
                }
                else if (member.NameEquals("categories"))
                {
                    categories = new SkillCategoryName[member.Value.GetArrayLength()];
                    for (var i = 0; i < categories.Length; i++)
                    {
                        categories[i] = Enum.Parse<SkillCategoryName>(member.Value[i].GetString(), false);
                    }
                }
                else if (member.NameEquals("facts"))
                {
                    factsSeen = true;
                    facts = new TraitFact[member.Value.GetArrayLength()];
                    for (var i = 0; i < facts.Length; i++)
                    {
                        facts[i] = TraitFactJsonReader.Read(member.Value[i]);
                    }
                }
                else if (member.NameEquals("traited_facts"))
                {
                    traitedFacts = new TraitFact[member.Value.GetArrayLength()];
                    for (var i = 0; i < traitedFacts.Length; i++)
                    {
                        traitedFacts[i] = TraitFactJsonReader.Read(member.Value[i]);
                    }
                }
                else if (member.NameEquals("description"))
                {
                    descriptionSeen = true;
                    description = member.Value.GetString();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("flags"))
                {
                    flagsSeen = true;
                    flags = new TraitSkillFlag[member.Value.GetArrayLength()];
                    for (var i = 0; i < flags.Length; i++)
                    {
                        flags[i] = Enum.Parse<TraitSkillFlag>(member.Value[i].GetString(), false);
                    }
                }
                else if (member.NameEquals("id"))
                {
                    idSeen = true;
                    id = member.Value.GetInt32();
                }
                else if (member.NameEquals("chat_link"))
                {
                    chatLinkSeen = true;
                    chatLink = member.Value.GetString();
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!nameSeen)
            {
                throw new JsonException($"Missing required property 'name' for object of type '{typeName}'.");
            }

            if (!factsSeen)
            {
                throw new JsonException($"Missing required property 'facts' for object of type '{typeName}'.");
            }

            if (!descriptionSeen)
            {
                throw new JsonException($"Missing required property 'description' for object of type '{typeName}'.");
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            if (!flagsSeen)
            {
                throw new JsonException($"Missing required property 'flags' for object of type '{typeName}'.");
            }

            if (!idSeen)
            {
                throw new JsonException($"Missing required property 'id' for object of type '{typeName}'.");
            }

            if (!chatLinkSeen)
            {
                throw new JsonException($"Missing required property 'chat_link' for object of type '{typeName}'.");
            }

            return new TraitSkill
            {
                Id = id,
                Name = name,
                Categories = categories,
                Facts = facts,
                TraitedFacts = traitedFacts,
                Description = description,
                Icon = icon,
                Flags = flags,
                ChatLink = chatLink
            };
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}
