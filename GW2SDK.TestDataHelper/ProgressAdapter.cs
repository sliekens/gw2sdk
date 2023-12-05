using Spectre.Console;

namespace GuildWars2.TestDataHelper;

public class ProgressAdapter(ProgressTask progress) : IProgress<ResultContext>
{
    public void Report(ResultContext value)
    {
        if (value.ResultTotal != progress.MaxValue)
        {
            progress.MaxValue = value.ResultTotal;
        }

        progress.Value = value.ResultCount;
    }
}
