using System;
using GuildWars2;
using Spectre.Console;

namespace MostVersatileMaterials;

public class ConsoleProgressUpdater(ProgressTask progress) : IProgress<BulkProgress>
{
    public void Report(BulkProgress value)
    {
        progress.MaxValue = value.ResultTotal;
        progress.Value = value.ResultCount;
    }
}
