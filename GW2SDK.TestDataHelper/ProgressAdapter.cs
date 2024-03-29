﻿using Spectre.Console;

namespace GuildWars2.TestDataHelper;

public class ProgressAdapter(ProgressTask progress) : IProgress<BulkProgress>
{
    public void Report(BulkProgress value)
    {
        progress.MaxValue = value.ResultTotal;
        progress.Value = value.ResultCount;
    }
}
