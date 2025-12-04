namespace GuildWars2.Tests.TestInfrastructure.Composition;

#pragma warning disable CA2000 // Dispose objects before losing scope

public sealed class Composition : IDisposable
{
    private readonly CompositionRoot root;

    private readonly CompositeDisposable disposables = new();

    // Scoped services would be here, e.g.
    // private readonly ScopedService scopedService;

    internal Composition(CompositionRoot root)
    {
        this.root = root;
        // scopedService = new ScopedService();
    }

    public object GetService(Type serviceType)
    {
        // Example of resolving a scoped service
        // if (serviceType == typeof(ScopedService))
        // {
        //     return scopedService;
        // }

        // Fallback to root container for transient and singleton services
        return root.GetService(serviceType);
    }

    public void Dispose()
    {
        // Dispose disposable scoped services
        // scopedService.Dispose();
        disposables.Dispose();
    }
}
