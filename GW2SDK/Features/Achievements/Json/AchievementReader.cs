using System;
using System.Text.Json;
using GW2SDK.Achievements.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Json;

[PublicAPI]
public static class AchievementReader
{
    public static Achievement Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        switch (json.GetProperty("type")
                    .GetString())
        {
            case "Default":
                return ReadDefaultAchievement(json, missingMemberBehavior);
            case "ItemSet":
                return ReadItemSetAchievement(json, missingMemberBehavior);
        }

        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> requirement = new("requirement");
        RequiredMember<string> lockedText = new("locked_text");
        RequiredMember<AchievementFlag> flags = new("flags");
        RequiredMember<AchievementTier> tiers = new("tiers");
        OptionalMember<int> prerequisites = new("prerequisites");
        OptionalMember<AchievementReward> rewards = new("rewards");
        OptionalMember<AchievementBit> bits = new("bits");
        NullableMember<int> pointCap = new("point_cap");

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
            Flags = flags.GetValues(missingMemberBehavior),
            Tiers = tiers.SelectMany(value => ReadAchievementTier(value, missingMemberBehavior)),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Rewards = rewards.SelectMany(value => ReadAchievementReward(value, missingMemberBehavior)),
            Bits = bits.SelectMany(value => ReadAchievementBit(value, missingMemberBehavior)),
            PointCap = pointCap.GetValue()
        };
    }

    private static DefaultAchievement ReadDefaultAchievement(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> requirement = new("requirement");
        RequiredMember<string> lockedText = new("locked_text");
        RequiredMember<AchievementFlag> flags = new("flags");
        RequiredMember<AchievementTier> tiers = new("tiers");
        OptionalMember<int> prerequisites = new("prerequisites");
        OptionalMember<AchievementReward> rewards = new("rewards");
        OptionalMember<AchievementBit> bits = new("bits");
        NullableMember<int> pointCap = new("point_cap");

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
            Flags = flags.GetValues(missingMemberBehavior),
            Tiers = tiers.SelectMany(value => ReadAchievementTier(value, missingMemberBehavior)),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Rewards = rewards.SelectMany(value => ReadAchievementReward(value, missingMemberBehavior)),
            Bits = bits.SelectMany(value => ReadAchievementBit(value, missingMemberBehavior)),
            PointCap = pointCap.GetValue()
        };
    }

    private static ItemSetAchievement ReadItemSetAchievement(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        OptionalMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<string> description = new("description");
        RequiredMember<string> requirement = new("requirement");
        RequiredMember<string> lockedText = new("locked_text");
        RequiredMember<AchievementFlag> flags = new("flags");
        RequiredMember<AchievementTier> tiers = new("tiers");
        OptionalMember<int> prerequisites = new("prerequisites");
        OptionalMember<AchievementReward> rewards = new("rewards");
        OptionalMember<AchievementBit> bits = new("bits");
        NullableMember<int> pointCap = new("point_cap");

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
            Flags = flags.GetValues(missingMemberBehavior),
            Tiers = tiers.SelectMany(value => ReadAchievementTier(value, missingMemberBehavior)),
            Prerequisites = prerequisites.SelectMany(value => value.GetInt32()),
            Rewards = rewards.SelectMany(value => ReadAchievementReward(value, missingMemberBehavior)),
            Bits = bits.SelectMany(value => ReadAchievementBit(value, missingMemberBehavior)),
            PointCap = pointCap.GetValue()
        };
    }

    private static AchievementBit ReadAchievementBit(JsonElement json, MissingMemberBehavior missingMemberBehavior)
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

    private static AchievementSkinBit ReadAchievementSkinBit(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
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

        return new AchievementSkinBit
        {
            Id = id.GetValue()
        };
    }

    private static AchievementItemBit ReadAchievementItemBit(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
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

        return new AchievementItemBit
        {
            Id = id.GetValue()
        };
    }

    private static AchievementMinipetBit ReadAchievementMinipetBit(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
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

        return new AchievementMinipetBit
        {
            Id = id.GetValue()
        };
    }

    private static AchievementTextBit ReadAchievementTextBit(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> text = new("text");
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

        return new AchievementTextBit
        {
            Text = text.GetValue()
        };
    }

    private static AchievementReward ReadAchievementReward(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type")
                    .GetString())
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

    private static TitleReward ReadTitleReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
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

        return new TitleReward
        {
            Id = id.GetValue()
        };
    }

    private static MasteryReward ReadMasteryReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<MasteryRegionName> region = new("region");
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

        return new MasteryReward
        {
            Id = id.GetValue(),
            Region = region.GetValue(missingMemberBehavior)
        };
    }

    private static ItemReward ReadItemReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");
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

        return new ItemReward
        {
            Id = id.GetValue(),
            Count = count.GetValue()
        };
    }

    private static CoinsReward ReadCoinsReward(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> coins = new("count");
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

        return new CoinsReward
        {
            Coins = coins.GetValue()
        };
    }

    private static AchievementTier ReadAchievementTier(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> count = new("count");
        RequiredMember<int> points = new("points");
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

        return new AchievementTier
        {
            Count = count.GetValue(),
            Points = points.GetValue()
        };
    }
}
