using System;
using GuildWars2;
using Spectre.Console;

namespace MostVersatileMaterials;

public class ProgressTaskUpdater(ProgressTask progress) : IProgress<BulkProgress>
{
    public void Report(BulkProgress value)
    {
        progress.MaxValue = value.ResultTotal;
        progress.Value = value.ResultCount;
    }
}
