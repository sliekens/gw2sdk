

using TUnit.Core.Interfaces;

[assembly: GuildWars2.Tests.TestInfrastructure.CustomParallelLimiter]

namespace GuildWars2.Tests.TestInfrastructure;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
public sealed class CustomParallelLimiterAttribute : Attribute, ITestRegisteredEventReceiver
{
    private sealed class CustomParallelLimit : IParallelLimit
    {
        public int Limit => 8;
    }

    public int Order => -1;

    public ValueTask OnTestRegistered(TestRegisteredContext context)
    {
#if NET
        ArgumentNullException.ThrowIfNull(context);
        context.SetParallelLimiter(new CustomParallelLimit());
        return ValueTask.CompletedTask;
#else
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.SetParallelLimiter(new CustomParallelLimit());
        return new ValueTask();
#endif
    }
}
