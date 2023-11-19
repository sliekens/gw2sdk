using Spectre.Console;

namespace GuildWars2.TestDataHelper;

public class ProgressAdapter : IProgress<ResultContext>
{
    private readonly ProgressTask progressTask;

    public ProgressAdapter(ProgressTask progress)
    {
        this.progressTask = progress;
    }

    public void Report(ResultContext value)
    {
        if (value.ResultTotal != progressTask.MaxValue)
        {
            progressTask.MaxValue = value.ResultTotal;
        }

        progressTask.Value = value.ResultCount;
    }
}
