﻿namespace GuildWars2.Accounts;

[PublicAPI]
[DataTransferObject]
public sealed record AccountLuck
{
    public required int Luck { get; init; }

    /// <summary>The percentage of Magic Find from Luck.</summary>
    public int MagicFind => Math.Min(300, MagicFindWithoutLevelCap);

    /// <summary>The hypothetical percentage of Magic Find from Luck if there was no level cap.</summary>
    public int MagicFindWithoutLevelCap
    {
        get
        {
            var level = 0;
            var luckLeft = Luck;
            foreach (var threshold in MagicFindThresholds.LevelThresholds)
            {
                if (luckLeft < threshold)
                {
                    return level;
                }

                level += 1;
                luckLeft -= threshold;
            }

            return level + (luckLeft / 30_000);
        }
    }
}
