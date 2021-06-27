using System;
using System.Text.Json;
using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementReader : IAchievementReader,
        IJsonReader<DefaultAchievement>,
        IJsonReader<ItemSetAchievement>
    {
        public Achievement Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "Default":
                    return ((IJsonReader<DefaultAchievement>) this).Read(json, missingMemberBehavior);
                case "ItemSet":
                    return ((IJsonReader<ItemSetAchievement>) this).Read(json, missingMemberBehavior);
            }

            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var requirement = new RequiredMember<string>("requirement");
            var lockedText = new RequiredMember<string>("locked_text");
            var flags = new RequiredMember<AchievementFlag[]>("flags");
            var tiers = new RequiredMember<AchievementTier[]>("tiers");
            var prerequisites = new OptionalMember<int[]>("prerequisites");
            var rewards = new OptionalMember<AchievementReward[]>("rewards");
            var bits = new OptionalMember<AchievementBit[]>("bits");
            var pointCap = new NullableMember<int>("point_cap");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (missingMemberBehavior == MissingMemberBehavior.Error)
                    {
                        throw new InvalidOperationException(Strings.UnexpectedDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(requirement.Name))
                {
                    requirement = requirement.From(member.Value);
                }
                else if (member.NameEquals(lockedText.Name))
                {
                    lockedText = lockedText.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(tiers.Name))
                {
                    tiers = tiers.From(member.Value);
                }
                else if (member.NameEquals(prerequisites.Name))
                {
                    prerequisites = prerequisites.From(member.Value);
                }
                else if (member.NameEquals(rewards.Name))
                {
                    rewards = rewards.From(member.Value);
                }
                else if (member.NameEquals(bits.Name))
                {
                    bits = bits.From(member.Value);
                }
                else if (member.NameEquals(pointCap.Name))
                {
                    pointCap = pointCap.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new Achievement
            {
                Id = id.GetValue(),
                Icon = icon.GetValueOrEmpty(),
                Name = name.GetValue(),
                Description = description.GetValue(),
                Requirement = requirement.GetValue(),
                LockedText = lockedText.GetValue(),
                Flags = flags.GetValue(missingMemberBehavior),
                Tiers = tiers.Select(value => value.GetArray(item => ReadAchievementTier(item, missingMemberBehavior))),
                Prerequisites = prerequisites.Select(value => value.GetArray(item => item.GetInt32())),
                Rewards = rewards.Select(value => value.GetArray(item => ReadAchievementReward(item, missingMemberBehavior))),
                Bits = bits.Select(value => value.GetArray(item => ReadAchievementBit(item, missingMemberBehavior))),
                PointCap = pointCap.GetValue()
            };
        }

        DefaultAchievement IJsonReader<DefaultAchievement>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var requirement = new RequiredMember<string>("requirement");
            var lockedText = new RequiredMember<string>("locked_text");
            var flags = new RequiredMember<AchievementFlag[]>("flags");
            var tiers = new RequiredMember<AchievementTier[]>("tiers");
            var prerequisites = new OptionalMember<int[]>("prerequisites");
            var rewards = new OptionalMember<AchievementReward[]>("rewards");
            var bits = new OptionalMember<AchievementBit[]>("bits");
            var pointCap = new NullableMember<int>("point_cap");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Default"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(requirement.Name))
                {
                    requirement = requirement.From(member.Value);
                }
                else if (member.NameEquals(lockedText.Name))
                {
                    lockedText = lockedText.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(tiers.Name))
                {
                    tiers = tiers.From(member.Value);
                }
                else if (member.NameEquals(prerequisites.Name))
                {
                    prerequisites = prerequisites.From(member.Value);
                }
                else if (member.NameEquals(rewards.Name))
                {
                    rewards = rewards.From(member.Value);
                }
                else if (member.NameEquals(bits.Name))
                {
                    bits = bits.From(member.Value);
                }
                else if (member.NameEquals(pointCap.Name))
                {
                    pointCap = pointCap.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new DefaultAchievement
            {
                Id = id.GetValue(),
                Icon = icon.GetValueOrEmpty(),
                Name = name.GetValue(),
                Description = description.GetValue(),
                Requirement = requirement.GetValue(),
                LockedText = lockedText.GetValue(),
                Flags = flags.GetValue(missingMemberBehavior),
                Tiers = tiers.Select(value => value.GetArray(item => ReadAchievementTier(item, missingMemberBehavior))),
                Prerequisites = prerequisites.Select(value => value.GetArray(item => item.GetInt32())),
                Rewards = rewards.Select(value => value.GetArray(item => ReadAchievementReward(item, missingMemberBehavior))),
                Bits = bits.Select(value => value.GetArray(item => ReadAchievementBit(item, missingMemberBehavior))),
                PointCap = pointCap.GetValue()
            };
        }

        ItemSetAchievement IJsonReader<ItemSetAchievement>.Read(
            JsonElement json,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            var id = new RequiredMember<int>("id");
            var icon = new OptionalMember<string>("icon");
            var name = new RequiredMember<string>("name");
            var description = new RequiredMember<string>("description");
            var requirement = new RequiredMember<string>("requirement");
            var lockedText = new RequiredMember<string>("locked_text");
            var flags = new RequiredMember<AchievementFlag[]>("flags");
            var tiers = new RequiredMember<AchievementTier[]>("tiers");
            var prerequisites = new OptionalMember<int[]>("prerequisites");
            var rewards = new OptionalMember<AchievementReward[]>("rewards");
            var bits = new OptionalMember<AchievementBit[]>("bits");
            var pointCap = new NullableMember<int>("point_cap");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("ItemSet"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(icon.Name))
                {
                    icon = icon.From(member.Value);
                }
                else if (member.NameEquals(name.Name))
                {
                    name = name.From(member.Value);
                }
                else if (member.NameEquals(description.Name))
                {
                    description = description.From(member.Value);
                }
                else if (member.NameEquals(requirement.Name))
                {
                    requirement = requirement.From(member.Value);
                }
                else if (member.NameEquals(lockedText.Name))
                {
                    lockedText = lockedText.From(member.Value);
                }
                else if (member.NameEquals(flags.Name))
                {
                    flags = flags.From(member.Value);
                }
                else if (member.NameEquals(tiers.Name))
                {
                    tiers = tiers.From(member.Value);
                }
                else if (member.NameEquals(prerequisites.Name))
                {
                    prerequisites = prerequisites.From(member.Value);
                }
                else if (member.NameEquals(rewards.Name))
                {
                    rewards = rewards.From(member.Value);
                }
                else if (member.NameEquals(bits.Name))
                {
                    bits = bits.From(member.Value);
                }
                else if (member.NameEquals(pointCap.Name))
                {
                    pointCap = pointCap.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemSetAchievement
            {
                Id = id.GetValue(),
                Icon = icon.GetValueOrEmpty(),
                Name = name.GetValue(),
                Description = description.GetValue(),
                Requirement = requirement.GetValue(),
                LockedText = lockedText.GetValue(),
                Flags = flags.GetValue(missingMemberBehavior),
                Tiers = tiers.Select(value => value.GetArray(item => ReadAchievementTier(item, missingMemberBehavior))),
                Prerequisites = prerequisites.Select(value => value.GetArray(item => item.GetInt32())),
                Rewards = rewards.Select(value => value.GetArray(item => ReadAchievementReward(item, missingMemberBehavior))),
                Bits = bits.Select(value => value.GetArray(item => ReadAchievementBit(item, missingMemberBehavior))),
                PointCap = pointCap.GetValue()
            };
        }

        private AchievementBit ReadAchievementBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            // BUG: some achievement bits don't have a type property, see https://github.com/arenanet/api-cdi/issues/670
            // Hopefully this will get fixed and then TryGetProperty can be replaced by GetProperty
            if (json.TryGetProperty("type", out var type))
            {
                switch (type.GetString())
                {
                    case "Text":
                        return ReadAchievementTextBit(json, missingMemberBehavior);
                    case "Minipet":
                        return ReadAchievementMinipetBit(json, missingMemberBehavior);
                    case "Item":
                        return ReadAchievementItemBit(json, missingMemberBehavior);
                    case "Skin":
                        return ReadAchievementSkinBit(json, missingMemberBehavior);
                }
            }

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var member in json.EnumerateObject())
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementBit();
        }

        private AchievementSkinBit ReadAchievementSkinBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Skin"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementSkinBit { Id = id.GetValue() };
        }

        private AchievementItemBit ReadAchievementItemBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Item"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementItemBit { Id = id.GetValue() };
        }

        private AchievementMinipetBit ReadAchievementMinipetBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Minipet"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementMinipetBit { Id = id.GetValue() };
        }

        private AchievementTextBit ReadAchievementTextBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var text = new RequiredMember<string>("text");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Text"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(text.Name))
                {
                    text = text.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementTextBit { Text = text.GetValue() };
        }

        private AchievementReward ReadAchievementReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            switch (json.GetProperty("type").GetString())
            {
                case "Coins":
                    return ReadCoinsReward(json, missingMemberBehavior);
                case "Item":
                    return ReadItemReward(json, missingMemberBehavior);
                case "Mastery":
                    return ReadMasteryReward(json, missingMemberBehavior);
                case "Title":
                    return ReadTitleReward(json, missingMemberBehavior);
            }

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var member in json.EnumerateObject())
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementReward();
        }

        private TitleReward ReadTitleReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Title"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new TitleReward { Id = id.GetValue() };
        }

        private MasteryReward ReadMasteryReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var region = new RequiredMember<MasteryRegionName>("region");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Mastery"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(region.Name))
                {
                    region = region.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new MasteryReward { Id = id.GetValue(), Region = region.GetValue(missingMemberBehavior) };
        }

        private ItemReward ReadItemReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var id = new RequiredMember<int>("id");
            var count = new RequiredMember<int>("count");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Item"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(id.Name))
                {
                    id = id.From(member.Value);
                }
                else if (member.NameEquals(count.Name))
                {
                    count = count.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new ItemReward { Id = id.GetValue(), Count = count.GetValue() };
        }

        private CoinsReward ReadCoinsReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var coins = new RequiredMember<int>("count");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals("type"))
                {
                    if (!member.Value.ValueEquals("Coins"))
                    {
                        throw new InvalidOperationException(Strings.InvalidDiscriminator(member.Value.GetString()));
                    }
                }
                else if (member.NameEquals(coins.Name))
                {
                    coins = coins.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new CoinsReward { Coins = coins.GetValue() };
        }

        private AchievementTier ReadAchievementTier(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var count = new RequiredMember<int>("count");
            var points = new RequiredMember<int>("points");
            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(count.Name))
                {
                    count = count.From(member.Value);
                }
                else if (member.NameEquals(points.Name))
                {
                    points = points.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new AchievementTier { Count = count.GetValue(), Points = points.GetValue() };
        }

        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}
