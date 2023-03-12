using System;

namespace GuildWars2;

/// <summary>A generic Subscription that delegates unsubscribe actions.</summary>
internal class Subscription : IDisposable
{
    private readonly Action unsubscribe;

    public Subscription(Action unsubscribe)
    {
        this.unsubscribe = unsubscribe;
    }

    public void Dispose() => unsubscribe();
}
