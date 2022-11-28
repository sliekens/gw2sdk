using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public interface IReplica<out T> : ITemporal
{
    T Value { get; }
}
