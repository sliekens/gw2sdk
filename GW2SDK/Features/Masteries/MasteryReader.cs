using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Masteries
{
    [PublicAPI]
    public sealed class MasteryReader : IMasteryReader
    {
        public Mastery Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var name = new RequiredMember<string>("name");
            var requirement = new RequiredMember<string>("requirement");
            var order = new RequiredMember<int>("order");
            var background = new RequiredMember<string>("background");
            var region = new RequiredMember<MasteryRegionName>("region");
            var levels = new RequiredMember<MasteryLevel>("levels");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(requirement.Name))
                {
                    requirement = requirement.From(member.Value);
                }
                else if (member.NameEquals(order.Name))
                {
                    order = order.From(member.Value);
                }
                else if (member.NameEquals(background.Name))
                {
                    background = background.From(member.Value);
                }
                else if (member.NameEquals(region.Name))
                {
                    region = region.From(member.Value);
                }
                else if (member.NameEquals(levels.Name))
                {
                    levels = levels.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Mastery
            {
                Id = id.GetValue(),
                Name = name.GetValue(),
                Requirement = requirement.GetValue(),
                Order = order.GetValue(),
                Background = background.GetValue(),
                Region = region.GetValue(missingMemberBehavior),
                Levels = levels.SelectMany(value => ReadLevel(value, missingMemberBehavior))
            };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();

        private MasteryLevel ReadLevel(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var instruction = new RequiredMember<string>("instruction");
            var icon = new RequiredMember<string>("icon");
            var pointCost = new RequiredMember<int>("point_cost");
            var experienceCost = new RequiredMember<int>("exp_cost");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(instruction.Name))
                {
                    instruction = instruction.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(pointCost.Name))
                {
                    pointCost = pointCost.From(member.Value);
                }
                else if (member.NameEquals(experienceCost.Name))
                {
                    experienceCost = experienceCost.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MasteryLevel
            {
                Name = name.GetValue(),
                Description = description.GetValue(),
                Instruction = instruction.GetValue(),
                Icon = icon.GetValue(),
                PointCost = pointCost.GetValue(),
                ExperienceCost = experienceCost.GetValue()
            };
        }
    }
}
