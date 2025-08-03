using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

using GuildWars2.Tests.TestInfrastructure;

using static System.Reflection.BindingFlags;

namespace GuildWars2.Tests.PatternsAndPractices;

public class DataTransferJsonTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    // Could use a better name
    // The premise is that every JSON conversion should be an extension method for JsonElement
    // e.g. JsonDocument.RootElement.GetSomeRecord()
    [Fact]
    public void JsonElement_conversions_are_extensions()
    {
        List<MethodInfo> candidates = [.. fixture.Assembly.DefinedTypes
            .Where(candidate => candidate.Name.EndsWith("Json", StringComparison.Ordinal))
            .SelectMany(reader => reader.GetMethods(DeclaredOnly | Public | NonPublic | Static))];
        Assert.All(
            fixture.DataTransferObjects,
            dto =>
            {
                List<MethodInfo> matches = [.. candidates.Where(info => info.ReturnType == dto)];
                Assert.All(
                    matches,
                    info =>
                    {
                        Assert.Equal("Get" + dto.Name, info.Name);
                        Assert.True(info.IsPublic, $"{info.Name} must be public.");
                        Assert.Equal($"{dto.Name}Json", info.DeclaringType!.Name);
                        Assert.True(
                            info.DeclaringType!.IsNotPublic,
                            $"{info.Name} must be internal."
                        );
                        Assert.True(
                            info.IsDefined(typeof(ExtensionAttribute), false),
                            $"{info.Name} must be an extension method."
                        );

                        ParameterInfo[] parameters = info.GetParameters();
                        Assert.Equal(typeof(JsonElement).MakeByRefType(), parameters[0].ParameterType);
                        Assert.Equal(dto.Namespace, info.DeclaringType.Namespace);
                    }
                );
            }
        );
    }
}
