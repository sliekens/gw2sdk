namespace GuildWars2.Hero.Accounts;

/// <summary>Information about magic find from luck on the account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AccountLuck
{
    /// <summary>The amount of luck consumed by the account.</summary>
    public required int Luck { get; init; }

    /// <summary>The percentage of Magic Find from Luck.</summary>
    public int MagicFind => Math.Min(300, MagicFindWithoutLevelCap);

    /// <summary>The hypothetical percentage of Magic Find from Luck if there was no level cap.</summary>
    public int MagicFindWithoutLevelCap
    {
        get
        {
            int level = 0;
            int luckLeft = Luck;
            foreach (int threshold in MagicFindThresholds.LevelThresholds)
            {
                if (luckLeft < threshold)
                {
                    return level;
                }

                level++;
                luckLeft -= threshold;
            }

            return level + (luckLeft / 30_000);
        }
    }
}
