using GuildWars2;

using Spectre.Console;

namespace MostVersatileMaterials;

internal sealed class ProgressTaskUpdater(ProgressTask progress) : IProgress<BulkProgress>
{
    public void Report(BulkProgress value)
    {
        ArgumentNullException.ThrowIfNull(value);

        progress.MaxValue = value.ResultTotal;
        progress.Value = value.ResultCount;
    }
}
