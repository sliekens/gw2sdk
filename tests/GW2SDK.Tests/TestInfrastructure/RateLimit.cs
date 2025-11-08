using GuildWars2.Tests.TestInfrastructure;

using TUnit.Core.Interfaces;

[assembly: ParallelLimiter<DefaultParallelLimit>]

namespace GuildWars2.Tests.TestInfrastructure;

public class DefaultParallelLimit : IParallelLimit
{
    public int Limit => 8;
}

public class Sequentually : IParallelLimit
{
    public int Limit => 1;
}
