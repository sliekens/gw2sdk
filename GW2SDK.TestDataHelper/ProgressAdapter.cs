using Spectre.Console;

namespace GuildWars2.TestDataHelper;

internal sealed class ProgressAdapter(ProgressTask progress) : IProgress<BulkProgress>
{
    public void Report(BulkProgress value)
    {
        ArgumentNullException.ThrowIfNull(value);

        progress.MaxValue = value.ResultTotal;
        progress.Value = value.ResultCount;
    }
}
