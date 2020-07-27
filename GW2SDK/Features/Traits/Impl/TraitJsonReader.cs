using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using JsonValueKind = System.Text.Json.JsonValueKind;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitJsonReader : IJsonReader<Trait>
    {
        private static readonly IJsonReader<TraitFact> TraitFactJsonReader = new TraitFactJsonReader();

        private static readonly IJsonReader<TraitSkill> TraitSkillJsonReader = new TraitSkillJsonReader();

        public Trait Read(in JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("JSON is not an object.");
            }

            int id = default;
            bool idSeen = default;

            int tier = default;
            bool tierSeen = default;

            int order = default;
            bool orderSeen = default;

            string name = "";
            bool nameSeen = default;

            string? description = default;

            TraitSlot slot = default;
            bool slotSeen = default;

            TraitFact[] facts = new TraitFact[0];

            int specialization = default;
            bool specializationSeen = default;

            string icon = "";
            bool iconSeen = default;

            TraitSkill[]? skills = default;

            TraitFact[]? traitedFacts = default;

            var typeName = nameof(Trait);
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("id"))
                {
                    idSeen = true;
                    id = member.Value.GetInt32();
                }
                else if (member.NameEquals("tier"))
                {
                    tierSeen = true;
                    tier = member.Value.GetInt32();
                }
                else if (member.NameEquals("order"))
                {
                    orderSeen = true;
                    order = member.Value.GetInt32();
                }
                else if (member.NameEquals("name"))
                {
                    nameSeen = true;
                    name = member.Value.GetString();
                }
                else if (member.NameEquals("description"))
                {
                    description = member.Value.GetString();
                }
                else if (member.NameEquals("slot"))
                {
                    slotSeen = true;
                    slot = Enum.Parse<TraitSlot>(member.Value.GetString(), false);
                }
                else if (member.NameEquals("facts"))
                {
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
                else if (member.NameEquals("specialization"))
                {
                    specializationSeen = true;
                    specialization = member.Value.GetInt32();
                }
                else if (member.NameEquals("icon"))
                {
                    iconSeen = true;
                    icon = member.Value.GetString();
                }
                else if (member.NameEquals("skills"))
                {
                    skills = new TraitSkill[member.Value.GetArrayLength()];
                    for (var i = 0; i < skills.Length; i++)
                    {
                        skills[i] = TraitSkillJsonReader.Read(member.Value[i]);
                    }
                }
                else
                {
                    throw new JsonException($"Could not find member '{member.Name}' on object of type '{typeName}'.");
                }
            }

            if (!idSeen)
            {
                throw new JsonException($"Missing required property 'id' for object of type '{typeName}'.");
            }

            if (!tierSeen)
            {
                throw new JsonException($"Missing required property 'tier' for object of type '{typeName}'.");
            }

            if (!orderSeen)
            {
                throw new JsonException($"Missing required property 'order' for object of type '{typeName}'.");
            }

            if (!nameSeen)
            {
                throw new JsonException($"Missing required property 'name' for object of type '{typeName}'.");
            }

            if (!slotSeen)
            {
                throw new JsonException($"Missing required property 'slot' for object of type '{typeName}'.");
            }

            if (!specializationSeen)
            {
                throw new JsonException($"Missing required property 'specialization' for object of type '{typeName}'.");
            }

            if (!iconSeen)
            {
                throw new JsonException($"Missing required property 'icon' for object of type '{typeName}'.");
            }

            return new Trait
            {
                Id = id,
                Tier = tier,
                Order = order,
                Name = name,
                Description = description,
                Slot = slot,
                Facts = facts,
                TraitedFacts = traitedFacts,
                SpezializationId = specialization,
                Icon = icon,
                Skills = skills
            };
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}
