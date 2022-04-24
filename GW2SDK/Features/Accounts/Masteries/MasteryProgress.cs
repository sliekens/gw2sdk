﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Masteries;

[PublicAPI]
[DataTransferObject]
public sealed record MasteryProgress
{
    public int Id { get; init; }

    public int Level { get; init; }
}