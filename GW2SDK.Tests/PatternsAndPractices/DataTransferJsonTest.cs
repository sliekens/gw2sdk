using System.Runtime.CompilerServices;
using System.Text.Json;
using GuildWars2.Tests.TestInfrastructure;
using static System.Reflection.BindingFlags;

namespace GuildWars2.Tests.PatternsAndPractices;

public class DataTransferJsonTest : IClassFixture<AssemblyFixture>
{
    public DataTransferJsonTest(AssemblyFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AssemblyFixture fixture;

    // Could use a better name
    // The premise is that every JSON conversion should be an extension method for JsonElement
    // e.g. JsonDocument.RootElement.GetSomeRecord()
    [Fact]
    public void JsonElement_conversions_are_extensions()
    {
        var candidates = fixture.Assembly.DefinedTypes.SelectMany(
                reader => reader.GetMethods(DeclaredOnly | Public | NonPublic | Static)
            )
            .ToList();
        Assert.All(
            fixture.DataTransferObjects,
            dto =>
            {
                var matches = candidates.Where(info => info.ReturnType == dto).ToList();
                if (matches.Count == 0)
                {
                    throw new InvalidOperationException(
                        $"Could not find method 'Get{dto.Name}'. Typo or is it missing?"
                    );
                }

                Assert.All(
                    matches,
                    info =>
                    {
                        Assert.Equal("Get" + dto.Name, info.Name);
                        Assert.True(info.IsPublic, $"{info.Name} must be public.");
                        Assert.Equal($"{dto.Name}Json", info.DeclaringType!.Name);
                        Assert.True(info.DeclaringType!.IsNotPublic, $"{info.Name} must be internal.");
                        Assert.True(
                            info.IsDefined(typeof(ExtensionAttribute), false),
                            $"{info.Name} must be an extension method."
                        );
                        Assert.Equal(
                            typeof(JsonElement),
                            info.GetParameters().FirstOrDefault()?.ParameterType
                        );
                        Assert.Equal(
                            typeof(MissingMemberBehavior),
                            info.GetParameters().Skip(1).FirstOrDefault()?.ParameterType
                        );
                    }
                );
            }
        );
    }
}
