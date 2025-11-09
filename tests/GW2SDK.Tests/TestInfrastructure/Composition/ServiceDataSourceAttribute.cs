namespace GuildWars2.Tests.TestInfrastructure.Composition;

public sealed class ServiceDataSourceAttribute : DependencyInjectionDataSourceAttribute<Composition>
{
    private static readonly CompositionRoot CompositionRoot = new();

    public override Composition CreateScope(DataGeneratorMetadata dataGeneratorMetadata)
    {
        return CompositionRoot.CreateScope();
    }

    public override object? Create(Composition scope, Type type)
    {
        ArgumentNullException.ThrowIfNull(scope);
        ArgumentNullException.ThrowIfNull(type);
        return scope.GetService(type);
    }
}
