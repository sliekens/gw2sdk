using System;
using GuildWars2;
using Spectre.Console;

namespace MostVersatileMaterials;

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
